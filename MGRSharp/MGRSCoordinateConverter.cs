/*
 * Copyright (C) 2011 United States Government as represented by the Administrator of the
 * National Aeronautics and Space Administration.
 * All Rights Reserved.
 */

/**
 * Converter used to translate MGRS coordinate strings to and from geodetic latitude and longitude.
 *
 * @author Patrick Murris
 * @version $Id$
 * @see MGRSCoord
 */

/**
 * Ported to Java from the NGA GeoTrans mgrs.c and mgrs.h code. Contains routines to convert from Geodetic to MGRS and
 * the other direction.
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
    class MGRSCoordConverter
    {
        public const int MGRS_NO_ERROR = 0;
        private const int MGRS_LAT_ERROR = 0x0001;
        private const int MGRS_LON_ERROR = 0x0002;
        public const int MGRS_STRING_ERROR = 0x0004;
        private const int MGRS_PRECISION_ERROR = 0x0008;
        private const int MGRS_A_ERROR = 0x0010;
        private const int MGRS_INV_F_ERROR = 0x0020;
        private const int MGRS_EASTING_ERROR = 0x0040;
        private const int MGRS_NORTHING_ERROR = 0x0080;
        private const int MGRS_ZONE_ERROR = 0x0100;
        private const int MGRS_HEMISPHERE_ERROR = 0x0200;
        private const int MGRS_LAT_WARNING = 0x0400;
        private const int MGRS_NOZONE_WARNING = 0x0800;
        private const int MGRS_UTM_ERROR = 0x1000;
        private const int MGRS_UPS_ERROR = 0x2000;

        private const double PI = Math.PI;
        private const double PI_OVER_2 = (PI / 2.0e0);
        private const int MAX_PRECISION = 5;
        private const double MIN_UTM_LAT = (-80 * PI) / 180.0;    // -80 degrees in radians
        private const double MAX_UTM_LAT = (84 * PI) / 180.0;     // 84 degrees in radians
        public const double DEG_TO_RAD = 0.017453292519943295;   // PI/180
        private const double RAD_TO_DEG = 57.29577951308232087;   // 180/PI

        private const double MIN_EAST_NORTH = 0;
        private const double MAX_EAST_NORTH = 4000000;
        private const double TWOMIL = 2000000;
        private const double ONEHT = 100000;

        private static readonly string CLARKE_1866 = "CC";
        private static readonly string CLARKE_1880 = "CD";
        private static readonly string BESSEL_1841 = "BR";
        private static readonly string BESSEL_1841_NAMIBIA = "BN";

        // Ellipsoid parameters, default to WGS 84
        private double _MGRS_a = 6378137.0;          // Semi-major axis of ellipsoid in meters
        private double _MGRS_f = 1 / 298.257223563;  // Flattening of ellipsoid
        private string _MGRS_Ellipsoid_Code = "WE";

        private string _MGRSString = "";
        private long ltr2_low_value;
        private long ltr2_high_value;       // this is only used for doing MGRS to xxx conversions.
        private double false_northing;
        private long lastLetter;
        private long last_error = MGRS_NO_ERROR;
        private double north, south, min_northing, northing_offset;  //smithjl added north_offset
        private double latitude;
        private double longitude;

        private const int LETTER_A = 0;   /* ARRAY INDEX FOR LETTER A               */
        private const int LETTER_B = 1;   /* ARRAY INDEX FOR LETTER B               */
        private const int LETTER_C = 2;   /* ARRAY INDEX FOR LETTER C               */
        private const int LETTER_D = 3;   /* ARRAY INDEX FOR LETTER D               */
        private const int LETTER_E = 4;   /* ARRAY INDEX FOR LETTER E               */
        private const int LETTER_F = 5;   /* ARRAY INDEX FOR LETTER E               */
        private const int LETTER_G = 6;   /* ARRAY INDEX FOR LETTER H               */
        private const int LETTER_H = 7;   /* ARRAY INDEX FOR LETTER H               */
        private const int LETTER_I = 8;   /* ARRAY INDEX FOR LETTER I               */
        private const int LETTER_J = 9;   /* ARRAY INDEX FOR LETTER J               */
        private const int LETTER_K = 10;   /* ARRAY INDEX FOR LETTER J               */
        private const int LETTER_L = 11;   /* ARRAY INDEX FOR LETTER L               */
        private const int LETTER_M = 12;   /* ARRAY INDEX FOR LETTER M               */
        private const int LETTER_N = 13;   /* ARRAY INDEX FOR LETTER N               */
        private const int LETTER_O = 14;   /* ARRAY INDEX FOR LETTER O               */
        private const int LETTER_P = 15;   /* ARRAY INDEX FOR LETTER P               */
        private const int LETTER_Q = 16;   /* ARRAY INDEX FOR LETTER Q               */
        private const int LETTER_R = 17;   /* ARRAY INDEX FOR LETTER R               */
        private const int LETTER_S = 18;   /* ARRAY INDEX FOR LETTER S               */
        private const int LETTER_T = 19;   /* ARRAY INDEX FOR LETTER S               */
        private const int LETTER_U = 20;   /* ARRAY INDEX FOR LETTER U               */
        private const int LETTER_V = 21;   /* ARRAY INDEX FOR LETTER V               */
        private const int LETTER_W = 22;   /* ARRAY INDEX FOR LETTER W               */
        private const int LETTER_X = 23;   /* ARRAY INDEX FOR LETTER X               */
        private const int LETTER_Y = 24;   /* ARRAY INDEX FOR LETTER Y               */
        private const int LETTER_Z = 25;   /* ARRAY INDEX FOR LETTER Z               */
        private const int MGRS_LETTERS = 3;  /* NUMBER OF LETTERS IN MGRS              */

        private static readonly string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        // UPS Constants are in the following order:
        //    long letter;            /* letter representing latitude band      */
        //    long ltr2_low_value;    /* 2nd letter range - high number         */
        //    long ltr2_high_value;   /* 2nd letter range - low number          */
        //    long ltr3_high_value;   /* 3rd letter range - high number (UPS)   */
        //    double false_easting;   /* False easting based on 2nd letter      */
        //    double false_northing;  /* False northing based on 3rd letter     */
        private static readonly long[,] upsConstants = new long[,]
        {
            {LETTER_A, LETTER_J, LETTER_Z, LETTER_Z, 800000, 800000},
            {LETTER_B, LETTER_A, LETTER_R, LETTER_Z, 2000000, 800000},
            {LETTER_Y, LETTER_J, LETTER_Z, LETTER_P, 800000, 1300000},
            {LETTER_Z, LETTER_A, LETTER_J, LETTER_P, 2000000, 1300000}
        };

        // Latitude Band Constants are in the following order:
        //        long letter;            /* letter representing latitude band  */
        //        double min_northing;    /* minimum northing for latitude band */
        //        double north;           /* upper latitude for latitude band   */
        //        double south;           /* lower latitude for latitude band   */
        private static readonly double[,] latitudeBandConstants = new double[,]
        {
            {LETTER_C, 1100000.0, -72.0, -80.5, 0.0},
            {LETTER_D, 2000000.0, -64.0, -72.0, 2000000.0},
            {LETTER_E, 2800000.0, -56.0, -64.0, 2000000.0},
            {LETTER_F, 3700000.0, -48.0, -56.0, 2000000.0},
            {LETTER_G, 4600000.0, -40.0, -48.0, 4000000.0},
            {LETTER_H, 5500000.0, -32.0, -40.0, 4000000.0},   //smithjl last column to table
            {LETTER_J, 6400000.0, -24.0, -32.0, 6000000.0},
            {LETTER_K, 7300000.0, -16.0, -24.0, 6000000.0},
            {LETTER_L, 8200000.0, -8.0, -16.0, 8000000.0},
            {LETTER_M, 9100000.0, 0.0, -8.0, 8000000.0},
            {LETTER_N, 0.0, 8.0, 0.0, 0.0},
            {LETTER_P, 800000.0, 16.0, 8.0, 0.0},
            {LETTER_Q, 1700000.0, 24.0, 16.0, 0.0},
            {LETTER_R, 2600000.0, 32.0, 24.0, 2000000.0},
            {LETTER_S, 3500000.0, 40.0, 32.0, 2000000.0},
            {LETTER_T, 4400000.0, 48.0, 40.0, 4000000.0},
            {LETTER_U, 5300000.0, 56.0, 48.0, 4000000.0},
            {LETTER_V, 6200000.0, 64.0, 56.0, 6000000.0},
            {LETTER_W, 7000000.0, 72.0, 64.0, 6000000.0},
            {LETTER_X, 7900000.0, 84.5, 72.0, 6000000.0}
        };

        private class MGRSComponents
        {
            public int zone;
            public int latitudeBand;
            public int squareLetter1;
            public int squareLetter2;
            public double easting;
            public double northing;
            public int precision;

            public MGRSComponents(int zone, int latitudeBand, int squareLetter1, int squareLetter2,
                                  double easting, double northing, int precision)
            {
                this.zone = zone;
                this.latitudeBand = latitudeBand;
                this.squareLetter1 = squareLetter1;
                this.squareLetter2 = squareLetter2;
                this.easting = easting;
                this.northing = northing;
                this.precision = precision;
            }

            override public string ToString()
            {
                return "MGRS: " + zone + " " +
                    alphabet[latitudeBand] + " " +
                    alphabet[squareLetter1] + alphabet[squareLetter2] + " " +
                    easting + " " +
                    northing + " " +
                    "(" + precision + ")";
            }
        }

        public MGRSCoordConverter()
        {
        }

        /**
         * The function setMGRSParameters receives the ellipsoid parameters and sets the corresponding state variables. If
         * any errors occur, the error code(s) are returned by the function, otherwise MGRS_NO_ERROR is returned.
         *
         * @param mgrs_a        Semi-major axis of ellipsoid in meters
         * @param mgrs_f        Flattening of ellipsoid
         * @param ellipsoidCode 2-letter code for ellipsoid
         *
         * @return error code
         */
        public long SetMGRSParameters(double mgrs_a, double mgrs_f, string ellipsoidCode)
        {
            if (mgrs_a <= 0.0)
                return MGRS_A_ERROR;

            if (mgrs_f == 0.0)
                return MGRS_INV_F_ERROR;
            double inv_f = 1 / mgrs_f;
            if (inv_f < 250 || inv_f > 350)
                return MGRS_INV_F_ERROR;

            _MGRS_a = mgrs_a;
            _MGRS_f = mgrs_f;
            _MGRS_Ellipsoid_Code = ellipsoidCode;

            return MGRS_NO_ERROR;
        }

        /** @return Flattening of ellipsoid */
        public double MGRS_f
        {
            get { return _MGRS_f; }
        }

        /** @return Semi-major axis of ellipsoid in meters */
        public double MGRS_a
        {
            get { return _MGRS_a; }
        }

        /** @return Latitude band letter */
        private long LastLetter
        {
            get { return lastLetter; }
        }

        /** @return 2-letter code for ellipsoid */
        public string MGRS_Ellipsoid_Code
        {
            get { return _MGRS_Ellipsoid_Code; }
        }

        /**
         * The function ConvertMGRSToGeodetic converts an MGRS coordinate string to Geodetic (latitude and longitude)
         * coordinates according to the current ellipsoid parameters.  If any errors occur, the error code(s) are returned
         * by the function, otherwise UTM_NO_ERROR is returned.
         *
         * @param MGRSString MGRS coordinate string.
         *
         * @return the error code.
         */
        public long ConvertMGRSToGeodetic(string MGRSString)
        {
            latitude = 0;
            longitude = 0;
            long error_code = CheckZone(MGRSString);
            if (error_code == MGRS_NO_ERROR)
            {
                UTMCoord UTM = ConvertMGRSToUTM(MGRSString);
                if (UTM != null)
                {
                    latitude = UTM.Latitude.radians;
                    longitude = UTM.Longitude.radians;
                }
                else
                    error_code = MGRS_UTM_ERROR;
            }
            else if (error_code == MGRS_NOZONE_WARNING)
            {
                UPSCoord UPS = ConvertMGRSToUPS(MGRSString);
                if (UPS != null)
                {
                    latitude = UPS.Latitude.radians;
                    longitude = UPS.Longitude.radians;
                    error_code = MGRS_NO_ERROR;
                }
                else
                    error_code = MGRS_UPS_ERROR;
            }
            return (error_code);
        }

        public double Latitude
        {
            get { return latitude; }
        }

        public double Longitude
        {
            get { return longitude; }
        }

        /**
         * The function Break_MGRS_String breaks down an MGRS coordinate string into its component parts. Updates
         * last_error.
         *
         * @param MGRSString the MGRS coordinate string
         *
         * @return the corresponding <code>MGRSComponents</code> or <code>null</code>.
         */
        private MGRSComponents BreakMGRSString(string MGRSString)
        {
            int num_digits;
            int num_letters;
            int i = 0;
            int j = 0;
            long error_code = MGRS_NO_ERROR;

            int zone = 0;
            int[] letters = new int[3];
            long easting = 0;
            long northing = 0;
            int precision = 0;

            while (i < MGRSString.Length && MGRSString[i] == ' ')
            {
                i++;  /* skip any leading blanks */
            }
            j = i;
            while (i < MGRSString.Length && char.IsDigit(MGRSString[i]))
            {
                i++;
            }
            num_digits = i - j;
            if (num_digits <= 2)
            {
                if (num_digits > 0)
                {
                    /* get zone */
                    zone = int.Parse(MGRSString.Substring(j, i));
                    if ((zone < 1) || (zone > 60))
                        error_code |= MGRS_STRING_ERROR;
                }
            }
            else
            {
                error_code |= MGRS_STRING_ERROR;
            }
            j = i;

            while (i < MGRSString.Length && char.IsLetter(MGRSString[i]))
            {
                i++;
            }
            num_letters = i - j;
            if (num_letters == 3)
            {
                /* get letters */
                letters[0] = alphabet.IndexOf(char.ToUpper(MGRSString[j]));
                if ((letters[0] == LETTER_I) || (letters[0] == LETTER_O))
                    error_code |= MGRS_STRING_ERROR;
                letters[1] = alphabet.IndexOf(char.ToUpper(MGRSString[j + 1]));
                if ((letters[1] == LETTER_I) || (letters[1] == LETTER_O))
                    error_code |= MGRS_STRING_ERROR;
                letters[2] = alphabet.IndexOf(char.ToUpper(MGRSString[j + 2]));
                if ((letters[2] == LETTER_I) || (letters[2] == LETTER_O))
                    error_code |= MGRS_STRING_ERROR;
            }
            else
                error_code |= MGRS_STRING_ERROR;
            j = i;
            while (i < MGRSString.Length && char.IsDigit(MGRSString[i]))
            {
                i++;
            }
            num_digits = i - j;
            if ((num_digits <= 10) && (num_digits % 2 == 0))
            {
                /* get easting, northing and precision */
                int n;
                double multiplier;
                /* get easting & northing */
                n = num_digits / 2;
                precision = n;
                if (n > 0)
                {
                    if( !long.TryParse( MGRSString.Substring(j, n), out easting ) ||
                        !long.TryParse( MGRSString.Substring(j+n, n), out northing ))
                    {
                        error_code |= MGRS_STRING_ERROR;
                    }
                    multiplier = Math.Pow(10.0, 5 - n);
                    easting *= (long)multiplier;
                    northing *= (long)multiplier;
                }
                else
                {
                    easting = 0;
                    northing = 0;
                }
            }
            else
                error_code |= MGRS_STRING_ERROR;

            last_error = error_code;
            if (error_code == MGRS_NO_ERROR)
                return new MGRSComponents(zone, letters[0], letters[1], letters[2], easting, northing, precision);

            return null;
        }

        /**
         * The function Check_Zone receives an MGRS coordinate string. If a zone is given, MGRS_NO_ERROR is returned.
         * Otherwise, MGRS_NOZONE_WARNING. is returned.
         *
         * @param MGRSString the MGRS coordinate string.
         *
         * @return the error code.
         */
        private long CheckZone(string MGRSString)
        {
            int i = 0;
            int j = 0;
            int num_digits = 0;
            long error_code = MGRS_NO_ERROR;

            /* skip any leading blanks */
            while (i < MGRSString.Length && MGRSString[i] == ' ')
            {
                i++;
            }
            j = i;
            while (i < MGRSString.Length && char.IsDigit(MGRSString[i]))
            {
                i++;
            }
            num_digits = i - j;
            if (num_digits > 2)
                error_code |= MGRS_STRING_ERROR;
            else if (num_digits <= 0)
                error_code |= MGRS_NOZONE_WARNING;

            return error_code;
        }

        /**
         * The function Get_Latitude_Band_Min_Northing receives a latitude band letter and uses the Latitude_Band_Table to
         * determine the minimum northing for that latitude band letter. Updates min_northing.
         *
         * @param letter Latitude band letter.
         *
         * @return the error code.
         */
        private long GetLatitudeBandMinNorthing(int letter)
        {
            long error_code = MGRS_NO_ERROR;

            if ((letter >= LETTER_C) && (letter <= LETTER_H))
            {
                min_northing = latitudeBandConstants[letter - 2,1];
                northing_offset = latitudeBandConstants[letter - 2,4];        //smithjl
            }
            else if ((letter >= LETTER_J) && (letter <= LETTER_N))
            {
                min_northing = latitudeBandConstants[letter - 3,1];
                northing_offset = latitudeBandConstants[letter - 3,4];        //smithjl
            }
            else if ((letter >= LETTER_P) && (letter <= LETTER_X))
            {
                min_northing = latitudeBandConstants[letter - 4,1];
                northing_offset = latitudeBandConstants[letter - 4,4];        //smithjl
            }
            else
                error_code |= MGRS_STRING_ERROR;
            return error_code;
        }

        /**
         * The function Get_Latitude_Range receives a latitude band letter and uses the Latitude_Band_Table to determine the
         * latitude band boundaries for that latitude band letter. Updates north and south.
         *
         * @param letter the Latitude band letter
         *
         * @return the error code.
         */
        private long GetLatitudeRange(int letter)
        {
            long error_code = MGRS_NO_ERROR;

            if ((letter >= LETTER_C) && (letter <= LETTER_H))
            {
                north = latitudeBandConstants[letter - 2,2] * DEG_TO_RAD;
                south = latitudeBandConstants[letter - 2,3] * DEG_TO_RAD;
            }
            else if ((letter >= LETTER_J) && (letter <= LETTER_N))
            {
                north = latitudeBandConstants[letter - 3,2] * DEG_TO_RAD;
                south = latitudeBandConstants[letter - 3,3] * DEG_TO_RAD;
            }
            else if ((letter >= LETTER_P) && (letter <= LETTER_X))
            {
                north = latitudeBandConstants[letter - 4,2] * DEG_TO_RAD;
                south = latitudeBandConstants[letter - 4,3] * DEG_TO_RAD;
            }
            else
                error_code |= MGRS_STRING_ERROR;

            return error_code;
        }

        /**
         * The function convertMGRSToUTM converts an MGRS coordinate string to UTM projection (zone, hemisphere, easting and
         * northing) coordinates according to the current ellipsoid parameters.  Updates last_error if any errors occured.
         *
         * @param MGRSString the MGRS coordinate string
         *
         * @return the corresponding <code>UTMComponents</code> or <code>null</code>.
         */
        private UTMCoord ConvertMGRSToUTM(String MGRSString)
        {
            double grid_easting;        /* Easting for 100,000 meter grid square      */
            double grid_northing;       /* Northing for 100,000 meter grid square     */
            double latitude = 0.0;
            double divisor = 1.0;
            long error_code = MGRS_NO_ERROR;

            string hemisphere = AVKey.NORTH;
            double easting = 0;
            double northing = 0;
            UTMCoord UTM = null;

            MGRSComponents MGRS = BreakMGRSString(MGRSString);
            if (MGRS == null)
                error_code |= MGRS_STRING_ERROR;
            else
            {
                if (error_code == MGRS_NO_ERROR)
                {
                    if ((MGRS.latitudeBand == LETTER_X) && ((MGRS.zone == 32) || (MGRS.zone == 34) || (MGRS.zone == 36)))
                        error_code |= MGRS_STRING_ERROR;
                    else
                    {
                        if (MGRS.latitudeBand < LETTER_N)
                            hemisphere = AVKey.SOUTH;
                        else
                            hemisphere = AVKey.NORTH;

                        GetGridValues(MGRS.zone);

                        // Check that the second letter of the MGRS string is within
                        // the range of valid second letter values
                        // Also check that the third letter is valid
                        if ((MGRS.squareLetter1 < ltr2_low_value) || (MGRS.squareLetter1 > ltr2_high_value) ||
                            (MGRS.squareLetter2 > LETTER_V))
                            error_code |= MGRS_STRING_ERROR;

                        if (error_code == MGRS_NO_ERROR)
                        {
                            grid_northing =
                                (double) (MGRS.squareLetter2) * ONEHT;  //   smithjl  commented out + false_northing;
                            grid_easting = (double) ((MGRS.squareLetter1) - ltr2_low_value + 1) * ONEHT;
                            if ((ltr2_low_value == LETTER_J) && (MGRS.squareLetter1 > LETTER_O))
                                grid_easting = grid_easting - ONEHT;

                            if (MGRS.squareLetter2 > LETTER_O)
                                grid_northing = grid_northing - ONEHT;

                            if (MGRS.squareLetter2 > LETTER_I)
                                grid_northing = grid_northing - ONEHT;

                            if (grid_northing >= TWOMIL)
                                grid_northing = grid_northing - TWOMIL;

                            error_code = GetLatitudeBandMinNorthing(MGRS.latitudeBand);
                            if (error_code == MGRS_NO_ERROR)
                            {
                                /*smithjl Deleted code here and added this*/
                                grid_northing = grid_northing - false_northing;

                                if (grid_northing < 0.0)
                                    grid_northing += TWOMIL;

                                grid_northing += northing_offset;

                                if (grid_northing < min_northing)
                                    grid_northing += TWOMIL;

                                /* smithjl End of added code */

                                easting = grid_easting + MGRS.easting;
                                northing = grid_northing + MGRS.northing;

                                try
                                {
                                    UTM = UTMCoord.FromUTM(MGRS.zone, hemisphere, easting, northing);
                                    latitude = UTM.Latitude.radians;
                                    divisor = Math.Pow(10.0, MGRS.precision);
                                    error_code = GetLatitudeRange(MGRS.latitudeBand);
                                    if (error_code == MGRS_NO_ERROR)
                                    {
                                        if (!(((south - DEG_TO_RAD / divisor) <= latitude)
                                              && (latitude <= (north + DEG_TO_RAD / divisor))))
                                            error_code |= MGRS_LAT_WARNING;
                                    }
                                }
                                catch (Exception)
                                {
                                    error_code = MGRS_UTM_ERROR;
                                }
                            }
                        }
                    }
                }
            }

            last_error = error_code;
            if (error_code == MGRS_NO_ERROR || error_code == MGRS_LAT_WARNING)
                return UTM;

            return null;
        } /* Convert_MGRS_To_UTM */

        /**
         * The function convertGeodeticToMGRS converts Geodetic (latitude and longitude) coordinates to an MGRS coordinate
         * string, according to the current ellipsoid parameters.  If any errors occur, the error code(s) are returned by
         * the function, otherwise MGRS_NO_ERROR is returned.
         *
         * @param latitude  Latitude in radians
         * @param longitude Longitude in radian
         * @param precision Precision level of MGRS string
         *
         * @return error code
         */
        public long convertGeodeticToMGRS(double latitude, double longitude, int precision)
        {
            _MGRSString = "";

            long error_code = MGRS_NO_ERROR;
            if ((latitude < -PI_OVER_2) || (latitude > PI_OVER_2))
            { /* Latitude out of range */
                error_code = MGRS_LAT_ERROR;
            }

            if ((longitude < -PI) || (longitude > (2 * PI)))
            { /* Longitude out of range */
                error_code = MGRS_LON_ERROR;
            }

            if ((precision < 0) || (precision > MAX_PRECISION))
                error_code = MGRS_PRECISION_ERROR;

            if (error_code == MGRS_NO_ERROR)
            {
                if ((latitude < MIN_UTM_LAT) || (latitude > MAX_UTM_LAT))
                {
                    try
                    {
                        UPSCoord UPS =
                            UPSCoord.FromLatLon(Angle.FromRadians(latitude), Angle.FromRadians(longitude));
                        error_code |= ConvertUPSToMGRS(UPS.Hemisphere, UPS.Easting,
                                                       UPS.Northing, precision);
                    }
                    catch (Exception)
                    {
                        error_code = MGRS_UPS_ERROR;
                    }
                }
                else
                {
                    try
                    {
                        UTMCoord UTM =
                            UTMCoord.FromLatLon(Angle.FromRadians(latitude), Angle.FromRadians(longitude));
                        error_code |= ConvertUTMToMGRS(UTM.Zone, latitude, UTM.Easting,
                                                       UTM.Northing, precision);
                    }
                    catch (Exception)
                    {
                        error_code = MGRS_UTM_ERROR;
                    }
                }
            }

            return error_code;
        }

        /** @return converted MGRS string */
        public string MGRSString
        {
            get { return _MGRSString; }
        }

        /**
         * The function Convert_UPS_To_MGRS converts UPS (hemisphere, easting, and northing) coordinates to an MGRS
         * coordinate string according to the current ellipsoid parameters.  If any errors occur, the error code(s) are
         * returned by the function, otherwise MGRS_NO_ERROR is returned.
         *
         * @param Hemisphere Hemisphere either, {@link gov.nasa.worldwind.avlist.AVKey#NORTH} or {@link
         *                   gov.nasa.worldwind.avlist.AVKey#SOUTH}.
         * @param Easting    Easting/X in meters
         * @param Northing   Northing/Y in meters
         * @param Precision  Precision level of MGRS string
         *
         * @return error value
         */
        private long ConvertUPSToMGRS(string Hemisphere, Double Easting, Double Northing, long Precision)
        {
            double false_easting;       /* False easting for 2nd letter                 */
            double false_northing;      /* False northing for 3rd letter                */
            double grid_easting;        /* Easting used to derive 2nd letter of MGRS    */
            double grid_northing;       /* Northing used to derive 3rd letter of MGRS   */
            int ltr2_low_value;        /* 2nd letter range - low number                */
            long[] letters = new long[MGRS_LETTERS];  /* Number location of 3 letters in alphabet     */
            double divisor;
            int index;
            long error_code = MGRS_NO_ERROR;

            if (!AVKey.NORTH.Equals(Hemisphere) && !AVKey.SOUTH.Equals(Hemisphere))
                error_code |= MGRS_HEMISPHERE_ERROR;
            if ((Easting < MIN_EAST_NORTH) || (Easting > MAX_EAST_NORTH))
                error_code |= MGRS_EASTING_ERROR;
            if ((Northing < MIN_EAST_NORTH) || (Northing > MAX_EAST_NORTH))
                error_code |= MGRS_NORTHING_ERROR;
            if ((Precision < 0) || (Precision > MAX_PRECISION))
                error_code |= MGRS_PRECISION_ERROR;

            if (error_code == MGRS_NO_ERROR)
            {
                divisor = Math.Pow(10.0, (5 - Precision));
                Easting = RoundMGRS(Easting / divisor) * divisor;
                Northing = RoundMGRS(Northing / divisor) * divisor;

                if (AVKey.NORTH.Equals(Hemisphere))
                {
                    if (Easting >= TWOMIL)
                        letters[0] = LETTER_Z;
                    else
                        letters[0] = LETTER_Y;

                    index = (int) letters[0] - 22;
//                ltr2_low_value = UPS_Constant_Table.get(index).ltr2_low_value;
//                false_easting = UPS_Constant_Table.get(index).false_easting;
//                false_northing = UPS_Constant_Table.get(index).false_northing;
                    ltr2_low_value = (int) upsConstants[index,1];
                    false_easting = (double) upsConstants[index,4];
                    false_northing = (double) upsConstants[index,5];
                }
                else // AVKey.SOUTH.equals(Hemisphere)
                {
                    if (Easting >= TWOMIL)
                        letters[0] = LETTER_B;
                    else
                        letters[0] = LETTER_A;

//                ltr2_low_value = UPS_Constant_Table.get((int) letters[0]).ltr2_low_value;
//                false_easting = UPS_Constant_Table.get((int) letters[0]).false_easting;
//                false_northing = UPS_Constant_Table.get((int) letters[0]).false_northing;
                    ltr2_low_value = (int) upsConstants[(int) letters[0],1];
                    false_easting = (double) upsConstants[(int) letters[0],4];
                    false_northing = (double) upsConstants[(int) letters[0],5];
                }

                grid_northing = Northing;
                grid_northing = grid_northing - false_northing;
                letters[2] = (int) (grid_northing / ONEHT);

                if (letters[2] > LETTER_H)
                    letters[2] = letters[2] + 1;

                if (letters[2] > LETTER_N)
                    letters[2] = letters[2] + 1;

                grid_easting = Easting;
                grid_easting = grid_easting - false_easting;
                letters[1] = (int) ltr2_low_value + ((int) (grid_easting / ONEHT));

                if (Easting < TWOMIL)
                {
                    if (letters[1] > LETTER_L)
                        letters[1] = letters[1] + 3;

                    if (letters[1] > LETTER_U)
                        letters[1] = letters[1] + 2;
                }
                else
                {
                    if (letters[1] > LETTER_C)
                        letters[1] = letters[1] + 2;

                    if (letters[1] > LETTER_H)
                        letters[1] = letters[1] + 1;

                    if (letters[1] > LETTER_L)
                        letters[1] = letters[1] + 3;
                }

                MakeMGRSString(0, letters, Easting, Northing, Precision);
            }
            return (error_code);
        }

        /**
         * The function UTM_To_MGRS calculates an MGRS coordinate string based on the zone, latitude, easting and northing.
         *
         * @param Zone      Zone number
         * @param Latitude  Latitude in radians
         * @param Easting   Easting
         * @param Northing  Northing
         * @param Precision Precision
         *
         * @return error code
         */
        private long ConvertUTMToMGRS(long Zone, double Latitude, double Easting, double Northing, long Precision)
        {
            double grid_easting;        /* Easting used to derive 2nd letter of MGRS   */
            double grid_northing;       /* Northing used to derive 3rd letter of MGRS  */
            long[] letters = new long[MGRS_LETTERS];  /* Number location of 3 letters in alphabet    */
            double divisor;
            long error_code;

            /* Round easting and northing values */
            divisor = Math.Pow(10.0, (5 - Precision));
            Easting = RoundMGRS(Easting / divisor) * divisor;
            Northing = RoundMGRS(Northing / divisor) * divisor;

            GetGridValues(Zone);

            error_code = GetLatitudeLetter(Latitude);
            letters[0] = LastLetter;

            if (error_code == MGRS_NO_ERROR)
            {
                grid_northing = Northing;
                if (grid_northing == 1.0e7)
                    grid_northing = grid_northing - 1.0;

                while (grid_northing >= TWOMIL)
                {
                    grid_northing = grid_northing - TWOMIL;
                }
                grid_northing = grid_northing + false_northing;   //smithjl

                if (grid_northing >= TWOMIL)                     //smithjl
                    grid_northing = grid_northing - TWOMIL;        //smithjl

                letters[2] = (long) (grid_northing / ONEHT);
                if (letters[2] > LETTER_H)
                    letters[2] = letters[2] + 1;

                if (letters[2] > LETTER_N)
                    letters[2] = letters[2] + 1;

                grid_easting = Easting;
                if (((letters[0] == LETTER_V) && (Zone == 31)) && (grid_easting == 500000.0))
                    grid_easting = grid_easting - 1.0; /* SUBTRACT 1 METER */

                letters[1] = ltr2_low_value + ((long) (grid_easting / ONEHT) - 1);
                if ((ltr2_low_value == LETTER_J) && (letters[1] > LETTER_N))
                    letters[1] = letters[1] + 1;

                MakeMGRSString(Zone, letters, Easting, Northing, Precision);
            }
            return error_code;
        }

        /**
         * The function Get_Grid_Values sets the letter range used for the 2nd letter in the MGRS coordinate string, based
         * on the set number of the utm zone. It also sets the false northing using a value of A for the second letter of
         * the grid square, based on the grid pattern and set number of the utm zone.
         * <p/>
         * Key values that are set in this function include:  ltr2_low_value, ltr2_high_value, and false_northing.
         *
         * @param zone Zone number
         */
        private void GetGridValues(long zone)
        {
            long set_number;    /* Set number (1-6) based on UTM zone number */
            long aa_pattern;    /* Pattern based on ellipsoid code */

            set_number = zone % 6;

            if (set_number == 0)
                set_number = 6;

            if (MGRS_Ellipsoid_Code.CompareTo(CLARKE_1866) == 0 || MGRS_Ellipsoid_Code.CompareTo(CLARKE_1880) == 0 ||
                MGRS_Ellipsoid_Code.CompareTo(BESSEL_1841) == 0 || MGRS_Ellipsoid_Code.CompareTo(BESSEL_1841_NAMIBIA) == 0)
                aa_pattern = 0L;
            else
                aa_pattern = 1L;

            if ((set_number == 1) || (set_number == 4))
            {
                ltr2_low_value = (long) LETTER_A;
                ltr2_high_value = (long) LETTER_H;
            }
            else if ((set_number == 2) || (set_number == 5))
            {
                ltr2_low_value = (long) LETTER_J;
                ltr2_high_value = (long) LETTER_R;
            }
            else if ((set_number == 3) || (set_number == 6))
            {
                ltr2_low_value = (long) LETTER_S;
                ltr2_high_value = (long) LETTER_Z;
            }

            /* False northing at A for second letter of grid square */
            if (aa_pattern == 1L)
            {
                if ((set_number % 2) == 0)
                    false_northing = 500000.0;             //smithjl was 1500000
                else
                    false_northing = 0.0;
            }
            else
            {
                if ((set_number % 2) == 0)
                    false_northing = 1500000.0;            //smithjl was 500000
                else
                    false_northing = 1000000.00;
            }
        }

        /**
         * The function Get_Latitude_Letter receives a latitude value and uses the Latitude_Band_Table to determine the
         * latitude band letter for that latitude.
         *
         * @param latitude latitude to turn into code
         *
         * @return error code
         */
        private long GetLatitudeLetter(double latitude)
        {
            double temp;
            long error_code = MGRS_NO_ERROR;
            double lat_deg = latitude * RAD_TO_DEG;

            if (lat_deg >= 72 && lat_deg < 84.5)
                lastLetter = (long) LETTER_X;
            else if (lat_deg > -80.5 && lat_deg < 72)
            {
                temp = ((latitude + (80.0 * DEG_TO_RAD)) / (8.0 * DEG_TO_RAD)) + 1.0e-12;
                // lastLetter = Latitude_Band_Table.get((int) temp).letter;
                lastLetter = (long) latitudeBandConstants[(int) temp,0];
            }
            else
                error_code |= MGRS_LAT_ERROR;

            return error_code;
        }

        /**
         * The function Round_MGRS rounds the input value to the nearest integer, using the standard engineering rule. The
         * rounded integer value is then returned.
         *
         * @param value Value to be rounded
         *
         * @return rounded double value
         */
        private double RoundMGRS(double value)
        {
            double ivalue = Math.Floor(value);
            long ival;
            double fraction = value - ivalue;
            // double fraction = modf (value, &ivalue);

            ival = (long) (ivalue);
            if ((fraction > 0.5) || ((fraction == 0.5) && (ival % 2 == 1)))
                ival++;
            return (double) ival;
        }

        /**
         * The function Make_MGRS_String constructs an MGRS string from its component parts.
         *
         * @param Zone      UTM Zone
         * @param Letters   MGRS coordinate string letters
         * @param Easting   Easting value
         * @param Northing  Northing value
         * @param Precision Precision level of MGRS string
         *
         * @return error code
         */
        private long MakeMGRSString(long Zone, long[] Letters, double Easting, double Northing, long Precision)
        {
            int j;
            double divisor;
            long east;
            long north;
            long error_code = MGRS_NO_ERROR;

            if (Zone != 0)
                _MGRSString = string.Format("{0:D2}", Zone);
            else
                _MGRSString = "  ";

            for (j = 0; j < 3; j++)
            {
                if (Letters[j] < 0 || Letters[j] > 26)
                    return MGRS_ZONE_ERROR; 
                _MGRSString = _MGRSString + alphabet[(int)Letters[j]];
            }

            divisor = Math.Pow(10.0, (5 - Precision));
            Easting = Easting % 100000.0;
            if (Easting >= 99999.5)
                Easting = 99999.0;
            east = (long) (Easting / divisor);

            // Here we need to only use the number requesting in the precision
            int iEast = (int) east;
            string sEast = iEast.ToString();
            if (sEast.Length > Precision)
                sEast = sEast.Substring(0, (int) Precision - 1);
            else
            {
                int i;
                int length = sEast.Length;
                for (i = 0; i < Precision - length; i++)
                {
                    sEast = "0" + sEast;
                }
            }
            _MGRSString = _MGRSString + " " + sEast;

            Northing = Northing % 100000.0;
            if (Northing >= 99999.5)
                Northing = 99999.0;
            north = (long) (Northing / divisor);

            int iNorth = (int) north;
            string sNorth = iNorth.ToString();
            if (sNorth.Length > Precision)
                sNorth = sNorth.Substring(0, (int) Precision - 1);
            else
            {
                int i;
                int length = sNorth.Length;
                for (i = 0; i < Precision - length; i++)
                {
                    sNorth = "0" + sNorth;
                }
            }
            _MGRSString = _MGRSString + " " + sNorth;

            return (error_code);
        }

        /**
         * Get the last error code.
         *
         * @return the last error code.
         */
        public long Error
        {
            get { return last_error; }
        }

        /**
         * The function Convert_MGRS_To_UPS converts an MGRS coordinate string to UPS (hemisphere, easting, and northing)
         * coordinates, according to the current ellipsoid parameters. If any errors occur, the error code(s) are returned
         * by the function, otherwide UPS_NO_ERROR is returned.
         *
         * @param MGRS the MGRS coordinate string.
         *
         * @return a corresponding {@link UPSCoord} instance.
         */
        private UPSCoord ConvertMGRSToUPS(String MGRS)
        {
            long ltr2_high_value;       /* 2nd letter range - high number             */
            long ltr3_high_value;       /* 3rd letter range - high number (UPS)       */
            long ltr2_low_value;        /* 2nd letter range - low number              */
            double false_easting;       /* False easting for 2nd letter               */
            double false_northing;      /* False northing for 3rd letter              */
            double grid_easting;        /* easting for 100,000 meter grid square      */
            double grid_northing;       /* northing for 100,000 meter grid square     */
            int index = 0;
            long error_code = MGRS_NO_ERROR;

            string hemisphere;
            double easting, northing;

            MGRSComponents mgrs = BreakMGRSString(MGRS);
            if (mgrs == null)
                error_code = this.last_error;

            if (mgrs != null && mgrs.zone > 0)
                error_code |= MGRS_STRING_ERROR;

            if (error_code == MGRS_NO_ERROR)
            {
                easting = mgrs.easting;
                northing = mgrs.northing;

                if (mgrs.latitudeBand >= LETTER_Y)
                {
                    hemisphere = AVKey.NORTH;

                    index = mgrs.latitudeBand - 22;
                    ltr2_low_value = upsConstants[index,1]; //.ltr2_low_value;
                    ltr2_high_value = upsConstants[index,2]; //.ltr2_high_value;
                    ltr3_high_value = upsConstants[index,3]; //.ltr3_high_value;
                    false_easting = upsConstants[index,4]; //.false_easting;
                    false_northing = upsConstants[index,5]; //.false_northing;
                }
                else
                {
                    hemisphere = AVKey.SOUTH;

                    ltr2_low_value = upsConstants[mgrs.latitudeBand,1]; //.ltr2_low_value;
                    ltr2_high_value = upsConstants[mgrs.latitudeBand,2]; //.ltr2_high_value;
                    ltr3_high_value = upsConstants[mgrs.latitudeBand,3]; //.ltr3_high_value;
                    false_easting = upsConstants[mgrs.latitudeBand,4]; //.false_easting;
                    false_northing = upsConstants[mgrs.latitudeBand,5]; //.false_northing;
                }

                // Check that the second letter of the MGRS string is within
                // the range of valid second letter values
                // Also check that the third letter is valid
                if ((mgrs.squareLetter1 < ltr2_low_value) || (mgrs.squareLetter1 > ltr2_high_value) ||
                    ((mgrs.squareLetter1 == LETTER_D) || (mgrs.squareLetter1 == LETTER_E) ||
                     (mgrs.squareLetter1 == LETTER_M) || (mgrs.squareLetter1 == LETTER_N) ||
                     (mgrs.squareLetter1 == LETTER_V) || (mgrs.squareLetter1 == LETTER_W)) ||
                    (mgrs.squareLetter2 > ltr3_high_value))
                    error_code = MGRS_STRING_ERROR;

                if (error_code == MGRS_NO_ERROR)
                {
                    grid_northing = (double) mgrs.squareLetter2 * ONEHT + false_northing;
                    if (mgrs.squareLetter2 > LETTER_I)
                        grid_northing = grid_northing - ONEHT;

                    if (mgrs.squareLetter2 > LETTER_O)
                        grid_northing = grid_northing - ONEHT;

                    grid_easting = (double) ((mgrs.squareLetter1) - ltr2_low_value) * ONEHT + false_easting;
                    if (ltr2_low_value != LETTER_A)
                    {
                        if (mgrs.squareLetter1 > LETTER_L)
                            grid_easting = grid_easting - 300000.0;

                        if (mgrs.squareLetter1 > LETTER_U)
                            grid_easting = grid_easting - 200000.0;
                    }
                    else
                    {
                        if (mgrs.squareLetter1 > LETTER_C)
                            grid_easting = grid_easting - 200000.0;

                        if (mgrs.squareLetter1 > LETTER_I)
                            grid_easting = grid_easting - ONEHT;

                        if (mgrs.squareLetter1 > LETTER_L)
                            grid_easting = grid_easting - 300000.0;
                    }

                    easting = grid_easting + easting;
                    northing = grid_northing + northing;
                    return UPSCoord.FromUPS(hemisphere, easting, northing);
                }
            }

            return null;
        }
    }
}
