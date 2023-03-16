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

namespace MGRSharp;

/**
     * This immutable class holds a set of UTM coordinates along with it's corresponding latitude and longitude.
     *
     * @author Patrick Murris
     * @version $Id$
     */
public class UTMCoord
{
    private Angle latitude;
    private Angle longitude;
    private string hemisphere;
    private int zone;
    private double easting;
    private double northing;
    private Angle centralMeridian;

    /**
         * Create a set of UTM coordinates from a pair of latitude and longitude for the given <code>Globe</code>.
         *
         * @param latitude  the latitude <code>Angle</code>.
         * @param longitude the longitude <code>Angle</code>.
         * @param globe     the <code>Globe</code> - can be null (will use WGS84).
         *
         * @return the corresponding <code>UTMCoord</code>.
         *
         * @throws IllegalArgumentException if <code>latitude</code> or <code>longitude</code> is null, or the conversion to
         *                                  UTM coordinates fails.
         */
    public static UTMCoord FromLatLon(Angle latitude, Angle longitude)
    {
        if (latitude == null || longitude == null) throw new ArgumentException("Latitude Or Longitude Is Null");

        var converter = new UTMCoordConverter();
        var err = converter.ConvertGeodeticToUTM(latitude.radians, longitude.radians);

        if (err != UTMCoordConverter.UTM_NO_ERROR) throw new ArgumentException("UTM Conversion Error");

        return new UTMCoord(latitude, longitude, converter.Zone, converter.Hemisphere,
            converter.Easting, converter.Northing, Angle.FromRadians(converter.CentralMeridian));
    }

    public static UTMCoord FromLatLon(Angle latitude, Angle longitude, string datum)
    {
        if (latitude == null || longitude == null) throw new ArgumentException("Latitude Or Longitude Is Null");

        UTMCoordConverter converter;
        if (datum != null && datum.Equals("NAD27"))
        {
            converter = new UTMCoordConverter(UTMCoordConverter.CLARKE_A, UTMCoordConverter.CLARKE_F);
            var llNAD27 = UTMCoordConverter.ConvertWGS84ToNAD27(latitude, longitude);
            latitude = llNAD27.Latitude;
            longitude = llNAD27.Longitude;
        }
        else
        {
            converter = new UTMCoordConverter(UTMCoordConverter.WGS84_A, UTMCoordConverter.WGS84_F);
        }

        var err = converter.ConvertGeodeticToUTM(latitude.radians, longitude.radians);

        if (err != UTMCoordConverter.UTM_NO_ERROR) throw new ArgumentException("UTM Conversion Error");

        return new UTMCoord(latitude, longitude, converter.Zone, converter.Hemisphere,
            converter.Easting, converter.Northing, Angle.FromRadians(converter.CentralMeridian));
    }

    /**
         * Create a set of UTM coordinates for the given <code>Globe</code>.
         *
         * @param zone       the UTM zone - 1 to 60.
         * @param hemisphere the hemisphere, either {@link gov.nasa.worldwind.avlist.AVKey#NORTH} or {@link
         *                   gov.nasa.worldwind.avlist.AVKey#SOUTH}.
         * @param easting    the easting distance in meters
         * @param northing   the northing distance in meters.
         * @param globe      the <code>Globe</code> - can be null (will use WGS84).
         *
         * @return the corresponding <code>UTMCoord</code>.
         *
         * @throws ArgumentException if the conversion to UTM coordinates fails.
         */
    public static UTMCoord FromUTM(int zone, string hemisphere, double easting, double northing)
    {
        var converter = new UTMCoordConverter();
        var err = converter.ConvertUTMToGeodetic(zone, hemisphere, easting, northing);

        if (err != UTMCoordConverter.UTM_NO_ERROR) throw new ArgumentException("UTM Conversion Error");

        return new UTMCoord(Angle.FromRadians(converter.Latitude),
            Angle.FromRadians(converter.Longitude),
            zone, hemisphere, easting, northing, Angle.FromRadians(converter.CentralMeridian));
    }

    /**
         * Convenience method for converting a UTM coordinate to a geographic location.
         *
         * @param zone       the UTM zone: 1 to 60.
         * @param hemisphere the hemisphere, either {@link gov.nasa.worldwind.avlist.AVKey#NORTH} or {@link
         *                   gov.nasa.worldwind.avlist.AVKey#SOUTH}.
         * @param easting    the easting distance in meters
         * @param northing   the northing distance in meters.
         * @param globe      the <code>Globe</code>. Can be null (will use WGS84).
         *
         * @return the geographic location corresponding to the specified UTM coordinate.
         */
    public static LatLon LocationFromUTMCoord(int zone, string hemisphere, double easting, double northing)
    {
        var coord = FromUTM(zone, hemisphere, easting, northing);
        return new LatLon(coord.Latitude, coord.Longitude);
    }

    /**
         * Create an arbitrary set of UTM coordinates with the given values.
         *
         * @param latitude   the latitude <code>Angle</code>.
         * @param longitude  the longitude <code>Angle</code>.
         * @param zone       the UTM zone - 1 to 60.
         * @param hemisphere the hemisphere, either {@link gov.nasa.worldwind.avlist.AVKey#NORTH} or {@link
         *                   gov.nasa.worldwind.avlist.AVKey#SOUTH}.
         * @param easting    the easting distance in meters
         * @param northing   the northing distance in meters.
         *
         * @throws ArgumentException if <code>latitude</code> or <code>longitude</code> is null.
         */
    public UTMCoord(Angle latitude, Angle longitude, int zone, string hemisphere, double easting, double northing) :
        this(latitude, longitude, zone, hemisphere, easting, northing, Angle.FromDegreesLongitude(0.0))
    {
    }

    /**
         * Create an arbitrary set of UTM coordinates with the given values.
         *
         * @param latitude        the latitude <code>Angle</code>.
         * @param longitude       the longitude <code>Angle</code>.
         * @param zone            the UTM zone - 1 to 60.
         * @param hemisphere      the hemisphere, either {@link gov.nasa.worldwind.avlist.AVKey#NORTH} or {@link
         *                        gov.nasa.worldwind.avlist.AVKey#SOUTH}.
         * @param easting         the easting distance in meters
         * @param northing        the northing distance in meters.
         * @param centralMeridian the cntral meridian <code>Angle</code>.
         *
         * @throws ArgumentException if <code>latitude</code> or <code>longitude</code> is null.
         */
    public UTMCoord(Angle latitude, Angle longitude, int zone, string hemisphere, double easting, double northing,
        Angle centralMeridian)
    {
        if (latitude == null || longitude == null) throw new ArgumentException("Latitude Or Longitude Is Null");

        this.latitude = latitude;
        this.longitude = longitude;
        this.hemisphere = hemisphere;
        this.zone = zone;
        this.easting = easting;
        this.northing = northing;
        this.centralMeridian = centralMeridian;
    }

    public Angle CentralMeridian => centralMeridian;

    public Angle Latitude => latitude;

    public Angle Longitude => longitude;

    public int Zone => zone;

    public string Hemisphere => hemisphere;

    public double Easting => easting;

    public double Northing => northing;

    public override string ToString()
    {
        return string.Format("{0} {1} {2}E {3}N",
            zone,
            AVKey.NORTH == hemisphere ? "N" : "S",
            easting, northing);
    }
}