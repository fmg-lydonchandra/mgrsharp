/*
 * Copyright (C) 2011 United States Government as represented by the Administrator of the
 * National Aeronautics and Space Administration.
 * All Rights Reserved.
 */

/**
 * Ported to C# from the Java port.
 *
 * @author Jussi Lahdenniemi
 */

using System;

namespace Worldwind
{
    /**
     * This class holds a set of Transverse Mercator coordinates along with the
     * corresponding latitude and longitude.
     *
     * @author Patrick Murris
     * @version $Id$
     * @see TMCoordConverter
     */
    public class TMCoord
    {
        private Angle latitude;
        private Angle longitude;
        private Angle originLatitude;
        private Angle centralMeridian;
        private double falseEasting;
        private double falseNorthing;
        private double scale;
        private double easting;
        private double northing;

        /**
         * Create a set of Transverse Mercator coordinates from a pair of latitude and longitude,
         * for the given <code>Globe</code> and projection parameters.
         *
         * @param latitude the latitude <code>Angle</code>.
         * @param longitude the longitude <code>Angle</code>.
         * @param globe the <code>Globe</code> - can be null (will use WGS84).
         * @param a semi-major ellipsoid radius. If this and argument f are non-null and globe is null, will use the specfied a and f.
         * @param f ellipsoid flattening. If this and argument a are non-null and globe is null, will use the specfied a and f.
         * @param originLatitude the origin latitude <code>Angle</code>.
         * @param centralMeridian the central meridian longitude <code>Angle</code>.
         * @param falseEasting easting value at the center of the projection in meters.
         * @param falseNorthing northing value at the center of the projection in meters.
         * @param scale scaling factor.
         * @return the corresponding <code>TMCoord</code>.
         * @throws IllegalArgumentException if <code>latitude</code> or <code>longitude</code> is null,
         * or the conversion to TM coordinates fails. If the globe is null conversion will default
         * to using WGS84.
         */
        public static TMCoord FromLatLon(Angle latitude, Angle longitude, double? a, double? f,
                                         Angle originLatitude, Angle centralMeridian,
                                         double falseEasting, double falseNorthing,
                                         double scale)
        {
            if (latitude == null || longitude == null)
            {
                throw new ArgumentException("Latitude Or Longitude Is Null");
            }
            if (originLatitude == null || centralMeridian == null)
            {
                throw new ArgumentException("Angle Is Null");
            }

            TMCoordConverter converter = new TMCoordConverter();
            if (!a.HasValue || !f.HasValue)
            {
                a = converter.A;
                f = converter.F;
            }
            long err = converter.SetTransverseMercatorParameters(a.Value, f.Value, originLatitude.radians, centralMeridian.radians,
                                                                 falseEasting, falseNorthing, scale);
            if (err == TMCoordConverter.TRANMERC_NO_ERROR)
                err = converter.ConvertGeodeticToTransverseMercator(latitude.radians, longitude.radians);

            if (err != TMCoordConverter.TRANMERC_NO_ERROR && err != TMCoordConverter.TRANMERC_LON_WARNING)
            {
                throw new ArgumentException("TM Conversion Error");
            }

            return new TMCoord(latitude, longitude, converter.Easting, converter.Northing,
                               originLatitude, centralMeridian, falseEasting, falseNorthing, scale);
        }

        /**
         * Create a set of Transverse Mercator coordinates for the given <code>Globe</code>,
         * easting, northing and projection parameters.
         *
         * @param easting the easting distance value in meters.
         * @param northing the northing distance value in meters.
         * @param globe the <code>Globe</code> - can be null (will use WGS84).
         * @param originLatitude the origin latitude <code>Angle</code>.
         * @param centralMeridian the central meridian longitude <code>Angle</code>.
         * @param falseEasting easting value at the center of the projection in meters.
         * @param falseNorthing northing value at the center of the projection in meters.
         * @param scale scaling factor.
         * @return the corresponding <code>TMCoord</code>.
         * @throws ArgumentException if <code>originLatitude</code> or <code>centralMeridian</code>
         * is null, or the conversion to geodetic coordinates fails. If the globe is null conversion will default
         * to using WGS84.
         */
        public static TMCoord FromTM(double easting, double northing,
                                     Angle originLatitude, Angle centralMeridian,
                                     double falseEasting, double falseNorthing,
                                     double scale)
        {
            if (originLatitude == null || centralMeridian == null)
            {
                throw new ArgumentException("Angle Is Null");
            }

            TMCoordConverter converter = new TMCoordConverter();

            double a = converter.A;
            double f = converter.F;
            long err = converter.SetTransverseMercatorParameters(a, f, originLatitude.radians, centralMeridian.radians,
                                                                 falseEasting, falseNorthing, scale);
            if (err == TMCoordConverter.TRANMERC_NO_ERROR)
                err = converter.ConvertTransverseMercatorToGeodetic(easting, northing);

            if (err != TMCoordConverter.TRANMERC_NO_ERROR && err != TMCoordConverter.TRANMERC_LON_WARNING)
            {
                throw new ArgumentException("TM Conversion Error");
            }

            return new TMCoord(Angle.FromRadians(converter.Latitude), Angle.FromRadians(converter.Longitude),
                               easting, northing, originLatitude, centralMeridian, falseEasting, falseNorthing, scale);
        }

        /**
         * Create an arbitrary set of Transverse Mercator coordinates with the given values.
         *
         * @param latitude the latitude <code>Angle</code>.
         * @param longitude the longitude <code>Angle</code>.
         * @param easting the easting distance value in meters.
         * @param northing the northing distance value in meters.
         * @param originLatitude the origin latitude <code>Angle</code>.
         * @param centralMeridian the central meridian longitude <code>Angle</code>.
         * @param falseEasting easting value at the center of the projection in meters.
         * @param falseNorthing northing value at the center of the projection in meters.
         * @param scale scaling factor.
         * @throws ArgumentException if <code>latitude</code>, <code>longitude</code>, <code>originLatitude</code>
         * or <code>centralMeridian</code> is null.
         */
        public TMCoord(Angle latitude, Angle longitude, double easting, double northing,
                       Angle originLatitude, Angle centralMeridian,
                       double falseEasting, double falseNorthing,
                       double scale)
        {
            if (latitude == null || longitude == null)
            {
                throw new ArgumentException("Latitude Or Longitude Is Null");
            }
            if (originLatitude == null || centralMeridian == null)
            {
                throw new ArgumentException("Angle Is Null");
            }

            this.latitude = latitude;
            this.longitude = longitude;
            this.easting = easting;
            this.northing = northing;
            this.originLatitude = originLatitude;
            this.centralMeridian = centralMeridian;
            this.falseEasting = falseEasting;
            this.falseNorthing = falseNorthing;
            this.scale = scale;
        }

        public Angle Latitude
        {
            get { return this.latitude; }
        }

        public Angle Longitude
        {
            get { return this.longitude; }
        }

        public Angle OriginLatitude
        {
            get { return this.originLatitude; }
        }

        public Angle CentralMeridian
        {
            get { return this.centralMeridian; }
        }

        public double FalseEasting
        {
            get { return this.falseEasting; }
        }

        public double FalseNorthing
        {
            get { return this.falseNorthing; }
        }

        public double Scale
        {
            get { return this.scale; }
        }

        public double Easting
        {
            get { return this.easting; }
        }

        public double Northing
        {
            get { return this.northing; }
        }
    }
}
