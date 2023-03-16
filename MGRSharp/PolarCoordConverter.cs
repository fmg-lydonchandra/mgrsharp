/*
 * Copyright (C) 2011 United States Government as represented by the Administrator of the
 * National Aeronautics and Space Administration.
 * All Rights Reserved.
 */

/***************************************************************************/
/* RSC IDENTIFIER: POLAR STEREOGRAPHIC
 *
 *
 * ABSTRACT
 *
 *    This component provides conversions between geodetic (latitude and
 *    longitude) coordinates and Polar Stereographic (easting and northing)
 *    coordinates.
 *
 * ERROR HANDLING
 *
 *    This component checks parameters for valid values.  If an invalid
 *    value is found the error code is combined with the current error code
 *    using the bitwise or.  This combining allows multiple error codes to
 *    be returned. The possible error codes are:
 *
 *          POLAR_NO_ERROR           : No errors occurred in function
 *          POLAR_LAT_ERROR          : Latitude outside of valid range
 *                                      (-90 to 90 degrees)
 *          POLAR_LON_ERROR          : Longitude outside of valid range
 *                                      (-180 to 360 degrees)
 *          POLAR_ORIGIN_LAT_ERROR   : Latitude of true scale outside of valid
 *                                      range (-90 to 90 degrees)
 *          POLAR_ORIGIN_LON_ERROR   : Longitude down from pole outside of valid
 *                                      range (-180 to 360 degrees)
 *          POLAR_EASTING_ERROR      : Easting outside of valid range,
 *                                      depending on ellipsoid and
 *                                      projection parameters
 *          POLAR_NORTHING_ERROR     : Northing outside of valid range,
 *                                      depending on ellipsoid and
 *                                      projection parameters
 *          POLAR_RADIUS_ERROR       : Coordinates too far from pole,
 *                                      depending on ellipsoid and
 *                                      projection parameters
 *          POLAR_A_ERROR            : Semi-major axis less than or equal to zero
 *          POLAR_INV_F_ERROR        : Inverse flattening outside of valid range
 *								  	                  (250 to 350)
 *
 *
 * REUSE NOTES
 *
 *    POLAR STEREOGRAPHIC is intended for reuse by any application that
 *    performs a Polar Stereographic projection.
 *
 *
 * REFERENCES
 *
 *    Further information on POLAR STEREOGRAPHIC can be found in the
 *    Reuse Manual.
 *
 *
 *    POLAR STEREOGRAPHIC originated from :
 *                                U.S. Army Topographic Engineering Center
 *                                Geospatial Information Division
 *                                7701 Telegraph Road
 *                                Alexandria, VA  22310-3864
 *
 *
 * LICENSES
 *
 *    None apply to this component.
 *
 *
 * RESTRICTIONS
 *
 *    POLAR STEREOGRAPHIC has no restrictions.
 *
 *
 * ENVIRONMENT
 *
 *    POLAR STEREOGRAPHIC was tested and certified in the following
 *    environments:
 *
 *    1. Solaris 2.5 with GCC, version 2.8.1
 *    2. Window 95 with MS Visual C++, version 6
 *
 *
 * MODIFICATIONS
 *
 *    Date              Description
 *    ----              -----------
 *    06-11-95          Original Code
 *    03-01-97          Original Code
 *
 *
 */

/**
 * Ported to Java from the NGA GeoTrans polarst.c and polarst.h code.
 *
 * @author Garrett Headley - Feb 12, 2007 4:48:11 PM
 * @version $Id$
 */

/**
 * Ported to C# from the Java port.
 *
 * @author Jussi Lahdenniemi
 */

using System;

namespace MGRSharp;

public class PolarCoordConverter
{
    private const long POLAR_NO_ERROR = 0x0000;
    private const long POLAR_LAT_ERROR = 0x0001;
    private const long POLAR_LON_ERROR = 0x0002;
    private const long POLAR_ORIGIN_LAT_ERROR = 0x0004;
    private const long POLAR_ORIGIN_LON_ERROR = 0x0008;
    public const long POLAR_EASTING_ERROR = 0x0010;
    public const long POLAR_NORTHING_ERROR = 0x0020;
    private const long POLAR_A_ERROR = 0x0040;
    private const long POLAR_INV_F_ERROR = 0x0080;
    public const long POLAR_RADIUS_ERROR = 0x0100;

    private const double PI = 3.14159265358979323;
    private const double PI_OVER_2 = PI / 2.0;
    private const double PI_Over_4 = PI / 4.0;
    private const double TWO_PI = 2.0 * PI;

    /* Ellipsoid Parameters, default to WGS 84  */
    private double Polar_a = 6378137.0; /* Semi-major axis of ellipsoid in meters  */
    private double Polar_f = 1 / 298.257223563; /* Flattening of ellipsoid  */
    private double es = 0.08181919084262188000; /* Eccentricity of ellipsoid    */
    private double es_OVER_2 = .040909595421311; /* es / 2.0 */
    private double Southern_Hemisphere = 0; /* Flag variable */
    private double mc = 1.0;
    private double tc = 1.0;
    private double e4 = 1.0033565552493;
    private double Polar_a_mc = 6378137.0; /* Polar_a * mc */
    private double two_Polar_a = 12756274.0; /* 2.0 * Polar_a */

    /* Polar Stereographic projection Parameters */
    private double Polar_Origin_Lat = PI * 90 / 180; /* Latitude of origin in radians */
    private double Polar_Origin_Long = 0.0; /* Longitude of origin in radians */
    private double Polar_False_Easting = 0.0; /* False easting in meters */
    private double Polar_False_Northing = 0.0; /* False northing in meters */

    /* Maximum variance for easting and northing values for WGS 84. */
    private double Polar_Delta_Easting = 12713601.0;
    private double Polar_Delta_Northing = 12713601.0;

    private double _Easting;
    private double _Northing;
    private double _Latitude;
    private double _Longitude;

    public PolarCoordConverter()
    {
    }

    /**
         * The function setPolarStereographicParameters receives the ellipsoid parameters and Polar Stereograpic projection
         * parameters as inputs, and sets the corresponding state variables.  If any errors occur, error code(s) are
         * returned by the function, otherwise POLAR_NO_ERROR is returned.
         *
         * @param a                        Semi-major axis of ellipsoid, in meters
         * @param f                        Flattening of ellipsoid
         * @param Latitude_of_True_Scale   Latitude of true scale, in radians
         * @param Longitude_Down_from_Pole Longitude down from pole, in radians
         * @param False_Easting            Easting (X) at center of projection, in meters
         * @param False_Northing           Northing (Y) at center of projection, in meters
         * @return error code
         */
    public long SetPolarStereographicParameters(double a, double f, double Latitude_of_True_Scale,
        double Longitude_Down_from_Pole, double False_Easting, double False_Northing)

    {
        double es2;
        double slat, clat;
        double essin;
        double one_PLUS_es, one_MINUS_es;
        double pow_es;
        var inv_f = 1 / f;
        const double epsilon = 1.0e-2;
        var Error_Code = POLAR_NO_ERROR;

        if (a <= 0.0) /* Semi-major axis must be greater than zero */ Error_Code |= POLAR_A_ERROR;
        if (inv_f < 250 || inv_f > 350) /* Inverse flattening must be between 250 and 350 */
            Error_Code |= POLAR_INV_F_ERROR;
        if (Latitude_of_True_Scale < -PI_OVER_2 ||
            Latitude_of_True_Scale > PI_OVER_2) /* Origin Latitude out of range */ Error_Code |= POLAR_ORIGIN_LAT_ERROR;
        if (Longitude_Down_from_Pole < -PI || Longitude_Down_from_Pole > TWO_PI) /* Origin Longitude out of range */
            Error_Code |= POLAR_ORIGIN_LON_ERROR;

        if (Error_Code == POLAR_NO_ERROR)
        {
            /* no errors */

            Polar_a = a;
            two_Polar_a = 2.0 * Polar_a;
            Polar_f = f;

            if (Longitude_Down_from_Pole > PI)
                Longitude_Down_from_Pole -= TWO_PI;
            if (Latitude_of_True_Scale < 0)
            {
                Southern_Hemisphere = 1;
                Polar_Origin_Lat = -Latitude_of_True_Scale;
                Polar_Origin_Long = -Longitude_Down_from_Pole;
            }
            else
            {
                Southern_Hemisphere = 0;
                Polar_Origin_Lat = Latitude_of_True_Scale;
                Polar_Origin_Long = Longitude_Down_from_Pole;
            }

            Polar_False_Easting = False_Easting;
            Polar_False_Northing = False_Northing;

            es2 = 2 * Polar_f - Polar_f * Polar_f;
            es = Math.Sqrt(es2);
            es_OVER_2 = es / 2.0;

            if (Math.Abs(Math.Abs(Polar_Origin_Lat) - PI_OVER_2) > 1.0e-10)
            {
                slat = Math.Sin(Polar_Origin_Lat);
                essin = es * slat;
                pow_es = Math.Pow((1.0 - essin) / (1.0 + essin), es_OVER_2);
                clat = Math.Cos(Polar_Origin_Lat);
                mc = clat / Math.Sqrt(1.0 - essin * essin);
                Polar_a_mc = Polar_a * mc;
                tc = Math.Tan(PI_Over_4 - Polar_Origin_Lat / 2.0) / pow_es;
            }
            else
            {
                one_PLUS_es = 1.0 + es;
                one_MINUS_es = 1.0 - es;
                e4 = Math.Sqrt(Math.Pow(one_PLUS_es, one_PLUS_es) * Math.Pow(one_MINUS_es, one_MINUS_es));
            }
        }

        /* Calculate Radius */
        ConvertGeodeticToPolarStereographic(0, Polar_Origin_Long);

        Polar_Delta_Northing = Northing * 2; // Increased range for accepted easting and northing values
        Polar_Delta_Northing = Math.Abs(Polar_Delta_Northing) + epsilon;
        Polar_Delta_Easting = Polar_Delta_Northing;

        return Error_Code;
    }

    /**
         * The function Convert_Geodetic_To_Polar_Stereographic converts geodetic coordinates (latitude and longitude) to
         * Polar Stereographic coordinates (easting and northing), according to the current ellipsoid and Polar
         * Stereographic projection parameters. If any errors occur, error code(s) are returned by the function, otherwise
         * POLAR_NO_ERROR is returned.
         *
         * @param Latitude  latitude, in radians
         * @param Longitude Longitude, in radians
         * @return error code
         */
    public long ConvertGeodeticToPolarStereographic(double Latitude, double Longitude)
    {
        double dlam;
        double slat;
        double essin;
        double t;
        double rho;
        double pow_es;
        var Error_Code = POLAR_NO_ERROR;

        if (Latitude < -PI_OVER_2 || Latitude > PI_OVER_2) /* Latitude out of range */ Error_Code |= POLAR_LAT_ERROR;
        if (Latitude < 0 && Southern_Hemisphere == 0) /* Latitude and Origin Latitude in different hemispheres */
            Error_Code |= POLAR_LAT_ERROR;
        if (Latitude > 0 && Southern_Hemisphere == 1) /* Latitude and Origin Latitude in different hemispheres */
            Error_Code |= POLAR_LAT_ERROR;
        if (Longitude < -PI || Longitude > TWO_PI) /* Longitude out of range */ Error_Code |= POLAR_LON_ERROR;

        if (Error_Code == POLAR_NO_ERROR)
        {
            /* no errors */

            if (Math.Abs(Math.Abs(Latitude) - PI_OVER_2) < 1.0e-10)
            {
                _Easting = 0.0;
                _Northing = 0.0;
            }
            else
            {
                if (Southern_Hemisphere != 0)
                {
                    Longitude *= -1.0;
                    Latitude *= -1.0;
                }

                dlam = Longitude - Polar_Origin_Long;
                if (dlam > PI) dlam -= TWO_PI;
                if (dlam < -PI) dlam += TWO_PI;
                slat = Math.Sin(Latitude);
                essin = es * slat;
                pow_es = Math.Pow((1.0 - essin) / (1.0 + essin), es_OVER_2);
                t = Math.Tan(PI_Over_4 - Latitude / 2.0) / pow_es;

                if (Math.Abs(Math.Abs(Polar_Origin_Lat) - PI_OVER_2) > 1.0e-10)
                    rho = Polar_a_mc * t / tc;
                else
                    rho = two_Polar_a * t / e4;


                if (Southern_Hemisphere != 0)
                {
                    _Easting = -(rho * Math.Sin(dlam) - Polar_False_Easting);
                    //Easting *= -1.0;
                    _Northing = rho * Math.Cos(dlam) + Polar_False_Northing;
                }
                else
                {
                    _Easting = rho * Math.Sin(dlam) + Polar_False_Easting;
                    _Northing = -rho * Math.Cos(dlam) + Polar_False_Northing;
                }
            }
        }

        return Error_Code;
    }

    public double Easting => _Easting;

    public double Northing => _Northing;

    /**
         *  The function Convert_Polar_Stereographic_To_Geodetic converts Polar
         *  Stereographic coordinates (easting and northing) to geodetic
         *  coordinates (latitude and longitude) according to the current ellipsoid
         *  and Polar Stereographic projection Parameters. If any errors occur, the
         *  code(s) are returned by the function, otherwise POLAR_NO_ERROR
         *  is returned.
         *
         *  @param Easting Easting (X), in meters
         *  @param Northing Northing (Y), in meters
         *  @return error code
         */
    public long ConvertPolarStereographicToGeodetic(double Easting, double Northing)
    {
        double dy = 0, dx = 0;
        double rho = 0;
        double t;
        double PHI, sin_PHI;
        var tempPHI = 0.0;
        double essin;
        double pow_es;
        double delta_radius;
        var Error_Code = POLAR_NO_ERROR;
        var min_easting = Polar_False_Easting - Polar_Delta_Easting;
        var max_easting = Polar_False_Easting + Polar_Delta_Easting;
        var min_northing = Polar_False_Northing - Polar_Delta_Northing;
        var max_northing = Polar_False_Northing + Polar_Delta_Northing;

        if (Easting > max_easting || Easting < min_easting) /* Easting out of range */
            Error_Code |= POLAR_EASTING_ERROR;
        if (Northing > max_northing || Northing < min_northing) /* Northing out of range */
            Error_Code |= POLAR_NORTHING_ERROR;

        if (Error_Code == POLAR_NO_ERROR)
        {
            dy = Northing - Polar_False_Northing;
            dx = Easting - Polar_False_Easting;

            /* Radius of point with origin of false easting, false northing */
            rho = Math.Sqrt(dx * dx + dy * dy);

            delta_radius = Math.Sqrt(Polar_Delta_Easting * Polar_Delta_Easting +
                                     Polar_Delta_Northing * Polar_Delta_Northing);

            if (rho > delta_radius) /* Point is outside of projection area */ Error_Code |= POLAR_RADIUS_ERROR;
        }

        if (Error_Code == POLAR_NO_ERROR)
        {
            /* no errors */
            if (dy == 0.0 && dx == 0.0)
            {
                _Latitude = PI_OVER_2;
                _Longitude = Polar_Origin_Long;
            }
            else
            {
                if (Southern_Hemisphere != 0)
                {
                    dy *= -1.0;
                    dx *= -1.0;
                }

                if (Math.Abs(Math.Abs(Polar_Origin_Lat) - PI_OVER_2) > 1.0e-10)
                    t = rho * tc / Polar_a_mc;
                else
                    t = rho * e4 / two_Polar_a;
                PHI = PI_OVER_2 - 2.0 * Math.Atan(t);
                while (Math.Abs(PHI - tempPHI) > 1.0e-10)
                {
                    tempPHI = PHI;
                    sin_PHI = Math.Sin(PHI);
                    essin = es * sin_PHI;
                    pow_es = Math.Pow((1.0 - essin) / (1.0 + essin), es_OVER_2);
                    PHI = PI_OVER_2 - 2.0 * Math.Atan(t * pow_es);
                }

                _Latitude = PHI;
                _Longitude = Polar_Origin_Long + Math.Atan2(dx, -dy);

                if (_Longitude > PI)
                    _Longitude -= TWO_PI;
                else if (_Longitude < -PI)
                    _Longitude += TWO_PI;


                if (_Latitude > PI_OVER_2) /* force distorted values to 90, -90 degrees */
                    _Latitude = PI_OVER_2;
                else if (_Latitude < -PI_OVER_2)
                    _Latitude = -PI_OVER_2;

                if (_Longitude > PI) /* force distorted values to 180, -180 degrees */
                    _Longitude = PI;
                else if (_Longitude < -PI)
                    _Longitude = -PI;
            }

            if (Southern_Hemisphere != 0)
            {
                _Latitude *= -1.0;
                _Longitude *= -1.0;
            }
        }

        return Error_Code;
    }

    /**
         * @return Latitude in radians.
         */
    public double Latitude => _Latitude;

    /**
         * @return Longitude in radians.
         */
    public double Longitude => _Longitude;
}