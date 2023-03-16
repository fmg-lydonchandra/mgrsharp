/**
 * Exmple/test code for the C# version of Worldwind's MGRS conversion
 * routines.
 *
 * @author Jussi Lahdenniemi
 */

using MGRSharp;

namespace Example;

public static class Test
{
    private static double[,] Testpt =
    {
        { 63.554403, 23.716971 },
        { 61.188325, -149.862498 },
        { -31.399455, -64.175971 },
        { -42.869627, 147.359083 },
        { 75.470308, 112.865295 },
        { 0.0, 0.0 },
        { 89.5, 12.3 },
        { -84.195396, 160.872803 },
        { -31.92157, 116.010921 }, //hazelmere
        { -31.995770, 116.123526 }, // perth
        { -22.3894, 119.70442 }, // flinders
        { -22.51937, 117.02329 } // eliwana 
    };

    public static void Main(string[] args)
    {
        for (var i = 0; i < Testpt.GetLength(0); i++)
        {
            Console.Write("WGS-84 {0,11:F6} {1,11:F6} => ", Testpt[i, 0], Testpt[i, 1]);
            var mgrs = Coordinates.MGRSFromLatLon(Testpt[i, 0], Testpt[i, 1]);
            Console.Write("MGRS {0} => ", mgrs);

            var wgs84 = Coordinates.LatLonFromMGRS(mgrs);
            Console.WriteLine("WGS-84 {0,11:F6} {1,11:F6}", wgs84[0], wgs84[1]);
        }
    }
}