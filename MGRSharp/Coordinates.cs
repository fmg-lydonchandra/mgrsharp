namespace MGRSharp;

public static class Coordinates
{
    public static string MGRSFromLatLon(double lat, double lon)
    {
        var latitude = Angle.FromDegrees(lat);
        var longitude = Angle.FromDegrees(lon);
        return MGRSCoord.FromLatLon(latitude, longitude).ToString();
    }

    public static double[] LatLonFromMGRS(string mgrs)
    {
        var coord = MGRSCoord.FromString(mgrs);
        return new double[]
        {
            coord.Latitude.degrees,
            coord.Longitude.degrees
        };
    }
}