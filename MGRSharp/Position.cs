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
     * @author tag
     * @version $Id$
     */
public class Position : LatLon
{
    public new static readonly Position ZERO = new(Angle.ZERO, Angle.ZERO, 0d);

    public double elevation;

    public static Position FromRadians(double latitude, double longitude, double elevation)
    {
        return new Position(Angle.FromRadians(latitude), Angle.FromRadians(longitude), elevation);
    }

    public static Position FromDegrees(double latitude, double longitude, double elevation)
    {
        return new Position(Angle.FromDegrees(latitude), Angle.FromDegrees(longitude), elevation);
    }

    public new static Position FromDegrees(double latitude, double longitude)
    {
        return new Position(Angle.FromDegrees(latitude), Angle.FromDegrees(longitude), 0);
    }

    public Position(Angle latitude, Angle longitude, double elevation) : base(latitude, longitude)
    {
        this.elevation = elevation;
    }

    public Position(LatLon latLon, double elevation) : base(latLon)
    {
        this.elevation = elevation;
    }

    /**
         * Obtains the elevation of this position
         *
         * @return this position's elevation
         */
    public double Elevation => elevation;

    /**
         * Obtains the elevation of this position
         *
         * @return this position's elevation
         */
    public double Altitude => elevation;

    public new Position Add(Position that)
    {
        var lat = Angle.NormalizeLatitude(latitude.Add(that.latitude));
        var lon = Angle.NormalizeLongitude(longitude.Add(that.longitude));

        return new Position(lat, lon, elevation + that.elevation);
    }

    public new Position Subtract(Position that)
    {
        var lat = Angle.NormalizeLatitude(latitude.Subtract(that.latitude));
        var lon = Angle.NormalizeLongitude(longitude.Subtract(that.longitude));

        return new Position(lat, lon, elevation - that.elevation);
    }

    /**
         * Returns the linear interpolation of <code>value1</code> and <code>value2</code>, treating the geographic
         * locations as simple 2D coordinate pairs, and treating the elevation values as 1D scalars.
         *
         * @param amount the interpolation factor
         * @param value1 the first position.
         * @param value2 the second position.
         *
         * @return the linear interpolation of <code>value1</code> and <code>value2</code>.
         *
         * @throws IllegalArgumentException if either position is null.
         */
    public static Position interpolate(double amount, Position value1, Position value2)
    {
        throw new NotImplementedException();
        /*
        if (value1 == null || value2 == null)
        {
            throw new IllegalArgumentException("Position Is Null");
        }

        if (amount < 0)
            return value1;
        else if (amount > 1)
            return value2;

        LatLon latLon = LatLon.interpolate(amount, value1, value2);
        // Elevation is independent of geographic interpolation method (i.e. rhumb, great-circle, linear), so we
        // interpolate elevation linearly.
        double elevation = WWMath.Mix(amount, value1.Elevation, value2.Elevation);

        return new Position(latLon, elevation);
        */
    }

    /**
         * Returns the an interpolated location along the great-arc between <code>value1</code> and <code>value2</code>. The
         * position's elevation components are linearly interpolated as a simple 1D scalar value. The interpolation factor
         * <code>amount</code> defines the weight given to each value, and is clamped to the range [0, 1]. If <code>a</code>
         * is 0 or less, this returns <code>value1</code>. If <code>amount</code> is 1 or more, this returns
         * <code>value2</code>. Otherwise, this returns the position on the great-arc between <code>value1</code> and
         * <code>value2</code> with a linearly interpolated elevation component, and corresponding to the specified
         * interpolation factor.
         *
         * @param amount the interpolation factor
         * @param value1 the first position.
         * @param value2 the second position.
         *
         * @return an interpolated position along the great-arc between <code>value1</code> and <code>value2</code>, with a
         *         linearly interpolated elevation component.
         *
         * @throws IllegalArgumentException if either location is null.
         */
    public static Position interpolateGreatCircle(double amount, Position value1, Position value2)
    {
        throw new NotImplementedException();
        /*
        if (value1 == null || value2 == null)
        {
            throw new IllegalArgumentException("Position Is Null");
        }

        LatLon latLon = LatLon.interpolateGreatCircle(amount, value1, value2);
        // Elevation is independent of geographic interpolation method (i.e. rhumb, great-circle, linear), so we
        // interpolate elevation linearly.
        double elevation = WWMath.mix(amount, value1.getElevation(), value2.getElevation());

        return new Position(latLon, elevation);
        */
    }

    /**
         * Returns the an interpolated location along the rhumb line between <code>value1</code> and <code>value2</code>.
         * The position's elevation components are linearly interpolated as a simple 1D scalar value. The interpolation
         * factor <code>amount</code> defines the weight given to each value, and is clamped to the range [0, 1]. If
         * <code>a</code> is 0 or less, this returns <code>value1</code>. If <code>amount</code> is 1 or more, this returns
         * <code>value2</code>. Otherwise, this returns the position on the rhumb line between <code>value1</code> and
         * <code>value2</code> with a linearly interpolated elevation component, and corresponding to the specified
         * interpolation factor.
         *
         * @param amount the interpolation factor
         * @param value1 the first position.
         * @param value2 the second position.
         *
         * @return an interpolated position along the great-arc between <code>value1</code> and <code>value2</code>, with a
         *         linearly interpolated elevation component.
         *
         * @throws IllegalArgumentException if either location is null.
         */
    public static Position interpolateRhumb(double amount, Position value1, Position value2)
    {
        throw new NotImplementedException();
        /*
        if (value1 == null || value2 == null)
        {
            throw new IllegalArgumentException("Position Is Null");
        }

        LatLon latLon = LatLon.interpolateRhumb(amount, value1, value2);
        // Elevation is independent of geographic interpolation method (i.e. rhumb, great-circle, linear), so we
        // interpolate elevation linearly.
        double elevation = WWMath.mix(amount, value1.getElevation(), value2.getElevation());

        return new Position(latLon, elevation);
        */
    }

    /*
    public static boolean positionsCrossDateLine(Iterable<? extends Position> positions)
    {
        if (positions == null)
        {
            throw new IllegalArgumentException("Positions List Is Null");
        }

        Position pos = null;
        for (Position posNext : positions)
        {
            if (pos != null)
            {
                // A segment cross the line if end pos have different longitude signs
                // and are more than 180 degress longitude apart
                if (Math.signum(pos.getLongitude().degrees) != Math.signum(posNext.getLongitude().degrees))
                {
                    double delta = Math.abs(pos.getLongitude().degrees - posNext.getLongitude().degrees);
                    if (delta > 180 && delta < 360)
                        return true;
                }
            }
            pos = posNext;
        }

        return false;
    }
    */

    /**
         * Computes a new set of positions translated from a specified reference position to a new reference position.
         *
         * @param oldPosition the original reference position.
         * @param newPosition the new reference position.
         * @param positions   the positions to translate.
         *
         * @return the translated positions, or null if the positions could not be translated.
         *
         * @throws IllegalArgumentException if any argument is null.
         */
    /*
    public static List<Position> computeShiftedPositions(Position oldPosition, Position newPosition,
                                                         Iterable<? extends Position> positions)
    {
        // TODO: Account for dateline spanning
        if (oldPosition == null || newPosition == null)
        {
            throw new IllegalArgumentException("Position Is Null");
        }

        if (positions == null)
        {
            throw new IllegalArgumentException("Positions List Is Null");
        }

        ArrayList<Position> newPositions = new ArrayList<Position>();

        double elevDelta = newPosition.getElevation() - oldPosition.getElevation();

        for (Position pos : positions)
        {
            Angle distance = LatLon.greatCircleDistance(oldPosition, pos);
            Angle azimuth = LatLon.greatCircleAzimuth(oldPosition, pos);
            LatLon newLocation = LatLon.greatCircleEndPosition(newPosition, azimuth, distance);
            double newElev = pos.getElevation() + elevDelta;

            newPositions.add(new Position(newLocation, newElev));
        }

        return newPositions;
    }
    */
    public override bool Equals(object o)
    {
        if (this == o)
            return true;

        var position = o as Position;
        if (o == null)
            return false;

        if (!base.Equals(position))
            return false;
        if (position.elevation != elevation)
            return false;

        return true;
    }

    public override int GetHashCode()
    {
        var result = base.GetHashCode();
        long temp;
        temp = elevation != 0.0 ? BitConverter.DoubleToInt64Bits(elevation) : 0L;
        result = 31 * result + (int)(temp ^ (temp >> 32));
        return result;
    }

    public override string ToString()
    {
        return string.Format("({0}, {1}, {2})", latitude.ToString(), longitude.ToString(), elevation);
    }
}