/*
 * Copyright (C) 2011 United States Government as represented by the Administrator of the
 * National Aeronautics and Space Administration.
 * All Rights Reserved.
 */

/**
 * Converter used to translate UTM coordinates to and from geodetic latitude and longitude.
 *
 * @author Patrick Murris
 * @version $Id$
 * @see UTMCoord, TMCoordConverter
 */

/**
 * Ported to Java from the NGA GeoTrans utm.c and utm.h
 *
 * @author Garrett Headley, Patrick Murris
 */

/**
 * Ported to C# from the Java port.
 *
 * @author Jussi Lahdenniemi
 */

using System;

namespace Worldwind
{
    class UTMCoordConverter
    {
        public const double CLARKE_A = 6378206.4;
        public const double CLARKE_B = 6356583.8;
        public const double CLARKE_F = 1 / 294.9786982;

        public const double WGS84_A = 6378137;
        public const double WGS84_F = 1 / 298.257223563;

        public const int UTM_NO_ERROR = 0x0000;
        public const int UTM_LAT_ERROR = 0x0001;
        public const int UTM_LON_ERROR = 0x0002;
        public const int UTM_EASTING_ERROR = 0x0004;
        public const int UTM_NORTHING_ERROR = 0x0008;
        public const int UTM_ZONE_ERROR = 0x0010;
        public const int UTM_HEMISPHERE_ERROR = 0x0020;
        public const int UTM_ZONE_OVERRIDE_ERROR = 0x0040;
        public const int UTM_A_ERROR = 0x0080;
        public const int UTM_INV_F_ERROR = 0x0100;
        public const int UTM_TM_ERROR = 0x0200;

        private const double PI = 3.14159265358979323;
        //private const double MIN_LAT = ((-80.5 * PI) / 180.0); /* -80.5 degrees in radians    */
        //private const double MAX_LAT = ((84.5 * PI) / 180.0);  /* 84.5 degrees in radians     */
        private const double MIN_LAT = ((-82 * PI) / 180.0); /* -82 degrees in radians    */
        private const double MAX_LAT = ((86 * PI) / 180.0);  /* 86 degrees in radians     */

        private const int MIN_EASTING = 100000;
        private const int MAX_EASTING = 900000;
        private const int MIN_NORTHING = 0;
        private const int MAX_NORTHING = 10000000;

        private double UTM_a = 6378137.0;         /* Semi-major axis of ellipsoid in meters  */
        private double UTM_f = 1 / 298.257223563; /* Flattening of ellipsoid                 */
        private long UTM_Override = 0;          /* Zone override flag                      */

        private double _Easting;
        private double _Northing;
        private String _Hemisphere;
        private int _Zone;
        private double _Latitude;
        private double _Longitude;
        private double _Central_Meridian;

        public UTMCoordConverter(){}

        public UTMCoordConverter(double a, double f)
        {
            SetUTMParameters(a, f, 0);
        }

        /**
         * The function Set_UTM_Parameters receives the ellipsoid parameters and UTM zone override parameter as inputs, and
         * sets the corresponding state variables.  If any errors occur, the error code(s) are returned by the function,
         * otherwise UTM_NO_ERROR is returned.
         *
         * @param a        Semi-major axis of ellipsoid, in meters
         * @param f        Flattening of ellipsoid
         * @param override UTM override zone, zero indicates no override
         *
         * @return error code
         */
        private long SetUTMParameters(double a, double f, long overrd)
        {
            double inv_f = 1 / f;
            long Error_Code = UTM_NO_ERROR;

            if (a <= 0.0)
            { /* Semi-major axis must be greater than zero */
                Error_Code |= UTM_A_ERROR;
            }
            if ((inv_f < 250) || (inv_f > 350))
            { /* Inverse flattening must be between 250 and 350 */
                Error_Code |= UTM_INV_F_ERROR;
            }
            if ((overrd < 0) || (overrd > 60))
            {
                Error_Code |= UTM_ZONE_OVERRIDE_ERROR;
            }
            if (Error_Code == UTM_NO_ERROR)
            { /* no errors */
                UTM_a = a;
                UTM_f = f;
                UTM_Override = overrd;
            }
            return (Error_Code);
        }

        /**
         * The function Convert_Geodetic_To_UTM converts geodetic (latitude and longitude) coordinates to UTM projection
         * (zone, hemisphere, easting and northing) coordinates according to the current ellipsoid and UTM zone override
         * parameters.  If any errors occur, the error code(s) are returned by the function, otherwise UTM_NO_ERROR is
         * returned.
         *
         * @param Latitude  Latitude in radians
         * @param Longitude Longitude in radians
         *
         * @return error code
         */
        public long ConvertGeodeticToUTM(double Latitude, double Longitude)
        {
            long Lat_Degrees;
            long Long_Degrees;
            long temp_zone;
            long Error_Code = UTM_NO_ERROR;
            double Origin_Latitude = 0;
            double False_Easting = 500000;
            double False_Northing = 0;
            double Scale = 0.9996;

            if ((Latitude < MIN_LAT) || (Latitude > MAX_LAT))
            { /* Latitude out of range */
                Error_Code |= UTM_LAT_ERROR;
            }
            if ((Longitude < -PI) || (Longitude > (2 * PI)))
            { /* Longitude out of range */
                Error_Code |= UTM_LON_ERROR;
            }
            if (Error_Code == UTM_NO_ERROR)
            { /* no errors */
                if (Longitude < 0)
                    Longitude += (2 * PI) + 1.0e-10;
                Lat_Degrees = (long) (Latitude * 180.0 / PI);
                Long_Degrees = (long) (Longitude * 180.0 / PI);

                if (Longitude < PI)
                    temp_zone = (long) (31 + ((Longitude * 180.0 / PI) / 6.0));
                else
                    temp_zone = (long) (((Longitude * 180.0 / PI) / 6.0) - 29);
                if (temp_zone > 60)
                    temp_zone = 1;
                /* UTM special cases */
                if ((Lat_Degrees > 55) && (Lat_Degrees < 64) && (Long_Degrees > -1) && (Long_Degrees < 3))
                    temp_zone = 31;
                if ((Lat_Degrees > 55) && (Lat_Degrees < 64) && (Long_Degrees > 2) && (Long_Degrees < 12))
                    temp_zone = 32;
                if ((Lat_Degrees > 71) && (Long_Degrees > -1) && (Long_Degrees < 9))
                    temp_zone = 31;
                if ((Lat_Degrees > 71) && (Long_Degrees > 8) && (Long_Degrees < 21))
                    temp_zone = 33;
                if ((Lat_Degrees > 71) && (Long_Degrees > 20) && (Long_Degrees < 33))
                    temp_zone = 35;
                if ((Lat_Degrees > 71) && (Long_Degrees > 32) && (Long_Degrees < 42))
                    temp_zone = 37;

                if (UTM_Override != 0)
                {
                    if ((temp_zone == 1) && (UTM_Override == 60))
                        temp_zone = UTM_Override;
                    else if ((temp_zone == 60) && (UTM_Override == 1))
                        temp_zone = UTM_Override;
                    else if (((temp_zone - 1) <= UTM_Override) && (UTM_Override <= (temp_zone + 1)))
                        temp_zone = UTM_Override;
                    else
                        Error_Code = UTM_ZONE_OVERRIDE_ERROR;
                }
                if (Error_Code == UTM_NO_ERROR)
                {
                    if (temp_zone >= 31)
                        _Central_Meridian = (6 * temp_zone - 183) * PI / 180.0;
                    else
                        _Central_Meridian = (6 * temp_zone + 177) * PI / 180.0;
                    _Zone = (int) temp_zone;
                    if (Latitude < 0)
                    {
                        False_Northing = 10000000;
                        _Hemisphere = AVKey.SOUTH;
                    }
                    else
                        _Hemisphere = AVKey.NORTH;

                    try
                    {
                        TMCoord TM = TMCoord.FromLatLon(Angle.FromRadians(Latitude), Angle.FromRadians(Longitude),
                                                        this.UTM_a, this.UTM_f, Angle.FromRadians(Origin_Latitude),
                                                        Angle.FromRadians(_Central_Meridian), False_Easting, False_Northing, Scale);
                        _Easting = TM.Easting;
                        _Northing = TM.Northing;

                        if ((_Easting < MIN_EASTING) || (_Easting > MAX_EASTING))
                            Error_Code = UTM_EASTING_ERROR;
                        if ((_Northing < MIN_NORTHING) || (_Northing > MAX_NORTHING))
                            Error_Code |= UTM_NORTHING_ERROR;
                    }
                    catch (Exception)
                    {
                        Error_Code = UTM_TM_ERROR;
                    }
                }
            }
            return (Error_Code);
        }

        /** @return Easting (X) in meters */
        public double Easting
        {
            get { return _Easting; }
        }

        /** @return Northing (Y) in meters */
        public double Northing
        {
            get { return _Northing; }
        }

        /**
         * @return The coordinate hemisphere, either {@link gov.nasa.worldwind.avlist.AVKey#NORTH} or {@link
         *         gov.nasa.worldwind.avlist.AVKey#SOUTH}.
         */
        public String Hemisphere
        {
            get { return _Hemisphere; }
        }

        /** @return UTM zone */
        public int Zone
        {
            get { return _Zone; }
        }

        /**
         * The function Convert_UTM_To_Geodetic converts UTM projection (zone, hemisphere, easting and northing) coordinates
         * to geodetic(latitude and  longitude) coordinates, according to the current ellipsoid parameters.  If any errors
         * occur, the error code(s) are returned by the function, otherwise UTM_NO_ERROR is returned.
         *
         * @param Zone       UTM zone.
         * @param Hemisphere The coordinate hemisphere, either {@link gov.nasa.worldwind.avlist.AVKey#NORTH} or {@link
         *                   gov.nasa.worldwind.avlist.AVKey#SOUTH}.
         * @param Easting    Easting (X) in meters.
         * @param Northing   Northing (Y) in meters.
         *
         * @return error code.
         */
        public long ConvertUTMToGeodetic(long Zone, String Hemisphere, double Easting, double Northing)
        {
            // TODO: arg checking
            long Error_Code = UTM_NO_ERROR;
            double Origin_Latitude = 0;
            double False_Easting = 500000;
            double False_Northing = 0;
            double Scale = 0.9996;

            if ((Zone < 1) || (Zone > 60))
                Error_Code |= UTM_ZONE_ERROR;
            if (!Hemisphere.Equals(AVKey.SOUTH) && !Hemisphere.Equals(AVKey.NORTH))
                Error_Code |= UTM_HEMISPHERE_ERROR;
//        if ((Easting < MIN_EASTING) || (Easting > MAX_EASTING))    //removed check to enable reprojecting images
//            Error_Code |= UTM_EASTING_ERROR;                       //that extend into another zone
            if ((Northing < MIN_NORTHING) || (Northing > MAX_NORTHING))
                Error_Code |= UTM_NORTHING_ERROR;

            if (Error_Code == UTM_NO_ERROR)
            { /* no errors */
                if (Zone >= 31)
                    _Central_Meridian = ((6 * Zone - 183) * PI / 180.0 /*+ 0.00000005*/);
                else
                    _Central_Meridian = ((6 * Zone + 177) * PI / 180.0 /*+ 0.00000005*/);
                if (Hemisphere.Equals(AVKey.SOUTH))
                    False_Northing = 10000000;
                try
                {
                    TMCoord TM = TMCoord.FromTM(Easting, Northing,
                                                Angle.FromRadians(Origin_Latitude), Angle.FromRadians(_Central_Meridian),
                                                False_Easting, False_Northing, Scale);
                    _Latitude = TM.Latitude.radians;
                    _Longitude = TM.Longitude.radians;

                    if ((Latitude < MIN_LAT) || (Latitude > MAX_LAT))
                    { /* Latitude out of range */
                        Error_Code |= UTM_NORTHING_ERROR;
                    }
                }
                catch (Exception)
                {
                    Error_Code = UTM_TM_ERROR;
                }
            }
            return (Error_Code);
        }

        /** @return Latitude in radians. */
        public double Latitude
        {
            get { return _Latitude; }
        }

        /** @return Longitude in radians. */
        public double Longitude
        {
            get { return _Longitude; }
        }

        /** @return Central_Meridian in radians. */
        public double CentralMeridian
        {
            get { return _Central_Meridian; }
        }

        public static LatLon ConvertWGS84ToNAD27(Angle latWGS, Angle lonWGS)
        {
            double deltaX = -12.0;
            double deltaY = 130.0;
            double deltaZ = 190.0;
            double difA = WGS84_A - CLARKE_A;
            double difF = WGS84_F - CLARKE_F;

            double lat = latWGS.radians;
            double lon = lonWGS.radians;

            double f = 1 - CLARKE_B / CLARKE_A;
            double e2 = 2 * f - Math.Pow(f, 2);
            double Rn = CLARKE_A / Math.Sqrt(1 - e2 * Math.Pow(Math.Sin(lat), 2.0));
            double Rm = (CLARKE_A * (1 - e2)) / Math.Pow(1 - e2 * Math.Pow(Math.Sin(lat), 2.0), 1.5);
            double errLon = (-1 * deltaX * Math.Sin(lon) + deltaY * Math.Cos(lon)) / (Rn * Math.Cos(lat));
            double errLat = (-1 * deltaX * Math.Sin(lat) * Math.Cos(lon) - deltaY * Math.Sin(lat) * Math.Sin(lon)
                             + deltaZ * Math.Cos(lat)
                             + difA * (Rn * e2 * Math.Sin(lat) * Math.Cos(lat)) / CLARKE_A
                             + difF * (Rm * CLARKE_A / CLARKE_B + Rn * CLARKE_B / CLARKE_A) * Math.Sin(lat) * Math.Cos(lat)) / Rm;

            return LatLon.FromRadians(lat - errLat, lon - errLon);
        }
    }
}
