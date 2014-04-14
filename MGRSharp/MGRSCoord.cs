/*
 * Copyright (C) 2011 United States Government as represented by the Administrator of the
 * National Aeronautics and Space Administration.
 * All Rights Reserved.
 */

/**
 * This class holds an immutable MGRS coordinate string along with
 * the corresponding latitude and longitude.
 *
 * @author Patrick Murris
 * @version $Id$
 */

/**
 * Ported to C# from the Java port.
 *
 * @author Jussi Lahdenniemi
 */

using System;

namespace Worldwind
{
    public class MGRSCoord
    {
        private string MGRSString;
        private Angle latitude;
        private Angle longitude;

        /**
         * Create a WGS84 MGRS coordinate from a pair of latitude and longitude <code>Angle</code>
         * with the maximum precision of five digits (one meter).
         *
         * @param latitude the latitude <code>Angle</code>.
         * @param longitude the longitude <code>Angle</code>.
         * @return the corresponding <code>MGRSCoord</code>.
         * @throws IllegalArgumentException if <code>latitude</code> or <code>longitude</code> is null,
         * or the conversion to MGRS coordinates fails.
         */
        public static MGRSCoord FromLatLon(Angle latitude, Angle longitude)
        {
            return FromLatLon(latitude, longitude, 5);
        }

        /**
         * Create a MGRS coordinate from a pair of latitude and longitude <code>Angle</code>
         * with the given precision or number of digits (1 to 5).
         *
         * @param latitude the latitude <code>Angle</code>.
         * @param longitude the longitude <code>Angle</code>.
         * @param globe the <code>Globe</code> - can be null (will use WGS84).
         * @param precision the number of digits used for easting and northing (1 to 5).
         * @return the corresponding <code>MGRSCoord</code>.
         * @throws IllegalArgumentException if <code>latitude</code> or <code>longitude</code> is null,
         * or the conversion to MGRS coordinates fails.
         */
        public static MGRSCoord FromLatLon(Angle latitude, Angle longitude, int precision)
        {
            if (latitude == null || longitude == null)
            {
                throw new ArgumentException("Latitude Or Longitude Is Null");
            }

            MGRSCoordConverter converter = new MGRSCoordConverter();
            long err = converter.convertGeodeticToMGRS(latitude.radians, longitude.radians, precision);

            if (err != MGRSCoordConverter.MGRS_NO_ERROR)
            {
                throw new ArgumentException("MGRS Conversion Error");
            }

            return new MGRSCoord(latitude, longitude, converter.MGRSString);
        }

        /**
         * Create a MGRS coordinate from a standard MGRS coordinate text string.
         * <p>
         * The string will be converted to uppercase and stripped of all spaces before being evaluated.
         * </p>
         * <p>Valid examples:<br />
         * 32TLP5626635418<br />
         * 32 T LP 56266 35418<br />
         * 11S KU 528 111<br />
         * </p>
         * @param MGRSString the MGRS coordinate text string.
         * @param globe the <code>Globe</code> - can be null (will use WGS84).
         * @return the corresponding <code>MGRSCoord</code>.
         * @throws ArgumentException if the <code>MGRSString</code> is null or empty,
         * the <code>globe</code> is null, or the conversion to geodetic coordinates fails (invalid coordinate string).
         */
        public static MGRSCoord FromString(string MGRSString)
        {
            if (MGRSString == null || MGRSString.Length == 0)
            {
                throw new ArgumentException("String Is Null");
            }

            MGRSString = MGRSString.ToUpper().Replace(" ", "");

            MGRSCoordConverter converter = new MGRSCoordConverter();
            long err = converter.ConvertMGRSToGeodetic(MGRSString);

            if (err != MGRSCoordConverter.MGRS_NO_ERROR)
            {
                throw new ArgumentException("MGRS Conversion Error");
            }

            return new MGRSCoord(Angle.FromRadians(converter.Latitude),
                                 Angle.FromRadians(converter.Longitude),
                                 MGRSString);
        }

        /**
         * Create an arbitrary MGRS coordinate from a pair of latitude-longitude <code>Angle</code>
         * and the corresponding MGRS coordinate string.
         *
         * @param latitude the latitude <code>Angle</code>.
         * @param longitude the longitude <code>Angle</code>.
         * @param MGRSString the corresponding MGRS coordinate string.
         * @throws ArgumentException if <code>latitude</code> or <code>longitude</code> is null,
         * or the MGRSString is null or empty.
         */
        public MGRSCoord(Angle latitude, Angle longitude, String MGRSString)
        {
            if (latitude == null || longitude == null)
            {
                throw new ArgumentException("Latitude Or Longitude Is Null");
            }
            if (MGRSString == null)
            {
                throw new ArgumentException("String Is Null");
            }
            if (MGRSString.Length == 0)
            {
                throw new ArgumentException("String Is Empty");
            }
            this.latitude = latitude;
            this.longitude = longitude;
            this.MGRSString = MGRSString;
        }

        public Angle Latitude
        {
            get { return this.latitude; }
        }

        public Angle Longitude
        {
            get { return this.longitude; }
        }

        override public string ToString()
        {
            return this.MGRSString;
        }
    }
}
