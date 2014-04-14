using System;

namespace Worldwind
{
    public static class Coordinates
    {
        public static string MGRSFromLatLon( double lat, double lon )
        {
            Angle latitude = Angle.FromDegrees(lat);
            Angle longitude = Angle.FromDegrees(lon);
            return MGRSCoord.FromLatLon(latitude, longitude).ToString();
        }

        public static double[] LatLonFromMGRS( string mgrs )
        {
            MGRSCoord coord = MGRSCoord.FromString(mgrs);
            return new double[]
            { 
                coord.Latitude.degrees,
                coord.Longitude.degrees 
            };
        }
    }
}
