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
     * This immutable class holds a set of UPS coordinates along with it's corresponding latitude and longitude.
     *
     * @author Patrick Murris
     * @version $Id$
     */
public class UPSCoord
{
    private Angle latitude;
    private Angle longitude;
    private string hemisphere;
    private double easting;
    private double northing;

    /**
         * Create a set of UPS coordinates from a pair of latitude and longitude for the given <code>Globe</code>.
         *
         * @param latitude  the latitude <code>Angle</code>.
         * @param longitude the longitude <code>Angle</code>.
         * @param globe     the <code>Globe</code> - can be null (will use WGS84).
         *
         * @return the corresponding <code>UPSCoord</code>.
         *
         * @throws IllegalArgumentException if <code>latitude</code> or <code>longitude</code> is null, or the conversion to
         *                                  UPS coordinates fails.
         */
    public static UPSCoord FromLatLon(Angle latitude, Angle longitude)
    {
        if (latitude == null || longitude == null) throw new ArgumentException("Latitude Or Longitude Is Null");

        var converter = new UPSCoordConverter();
        var err = converter.ConvertGeodeticToUPS(latitude.radians, longitude.radians);

        if (err != UPSCoordConverter.UPS_NO_ERROR) throw new ArgumentException("UPS Conversion Error");

        return new UPSCoord(latitude, longitude, converter.Hemisphere,
            converter.Easting, converter.Northing);
    }

    /**
         * Create a set of UPS coordinates for the given <code>Globe</code>.
         *
         * @param hemisphere the hemisphere, either {@link gov.nasa.worldwind.avlist.AVKey#NORTH} or {@link
         *                   gov.nasa.worldwind.avlist.AVKey#SOUTH}.
         * @param easting    the easting distance in meters
         * @param northing   the northing distance in meters.
         * @param globe      the <code>Globe</code> - can be null (will use WGS84).
         *
         * @return the corresponding <code>UPSCoord</code>.
         *
         * @throws ArgumentException if the conversion to UPS coordinates fails.
         */
    public static UPSCoord FromUPS(string hemisphere, double easting, double northing)
    {
        var converter = new UPSCoordConverter();
        var err = converter.ConvertUPSToGeodetic(hemisphere, easting, northing);

        if (err != UTMCoordConverter.UTM_NO_ERROR) throw new ArgumentException("UTM Conversion Error");

        return new UPSCoord(Angle.FromRadians(converter.Latitude),
            Angle.FromRadians(converter.Longitude),
            hemisphere, easting, northing);
    }

    /**
         * Create an arbitrary set of UPS coordinates with the given values.
         *
         * @param latitude   the latitude <code>Angle</code>.
         * @param longitude  the longitude <code>Angle</code>.
         * @param hemisphere the hemisphere, either {@link gov.nasa.worldwind.avlist.AVKey#NORTH} or {@link
         *                   gov.nasa.worldwind.avlist.AVKey#SOUTH}.
         * @param easting    the easting distance in meters
         * @param northing   the northing distance in meters.
         *
         * @throws ArgumentException if <code>latitude</code>, <code>longitude</code>, or <code>hemisphere</code> is
         *                                  null.
         */
    public UPSCoord(Angle latitude, Angle longitude, string hemisphere, double easting, double northing)
    {
        if (latitude == null || longitude == null) throw new ArgumentException("Latitude Or Longitude Is Null");

        this.latitude = latitude;
        this.longitude = longitude;
        this.hemisphere = hemisphere;
        this.easting = easting;
        this.northing = northing;
    }

    public Angle Latitude => latitude;

    public Angle Longitude => longitude;

    public string Hemisphere => hemisphere;

    public double Easting => easting;

    public double Northing => northing;

    public override string ToString()
    {
        return string.Format("{0} {1}E {2}N",
            AVKey.NORTH == hemisphere ? "N" : "S",
            easting, northing);
    }
}