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
     * Represents a geometric angle. Instances of <code>Angle</code> are immutable. An angle can be obtained through the
     * factory methods {@link #fromDegrees} and {@link #fromRadians}.
     *
     * @author Tom Gaskins
     * @version $Id$
     */
public class Angle
{
    // Angle format
    public static readonly string ANGLE_FORMAT_DD = "gov.nasa.worldwind.Geom.AngleDD";
    public static readonly string ANGLE_FORMAT_DMS = "gov.nasa.worldwind.Geom.AngleDMS";

    /** Represents an angle of zero degrees */
    public static readonly Angle ZERO = FromDegrees(0);

    /** Represents a right angle of positive 90 degrees */
    public static readonly Angle POS90 = FromDegrees(90);

    /** Represents a right angle of negative 90 degrees */
    public static readonly Angle NEG90 = FromDegrees(-90);

    /** Represents an angle of positive 180 degrees */
    public static readonly Angle POS180 = FromDegrees(180);

    /** Represents an angle of negative 180 degrees */
    public static readonly Angle NEG180 = FromDegrees(-180);

    /** Represents an angle of positive 360 degrees */
    public static readonly Angle POS360 = FromDegrees(360);

    /** Represents an angle of negative 360 degrees */
    public static readonly Angle NEG360 = FromDegrees(-360);

    /** Represents an angle of 1 minute */
    public static readonly Angle MINUTE = FromDegrees(1.0 / 60.0);

    /** Represents an angle of 1 second */
    public static readonly Angle SECOND = FromDegrees(1.0 / 3600.0);

    private static readonly double DEGREES_TO_RADIANS = Math.PI / 180.0;
    private static readonly double RADIANS_TO_DEGREES = 180.0 / Math.PI;

    /**
         * Obtains an angle from a specified number of degrees.
         *
         * @param degrees the size in degrees of the angle to be obtained
         *
         * @return a new angle, whose size in degrees is given by <code>degrees</code>
         */
    public static Angle FromDegrees(double degrees)
    {
        return new Angle(degrees, DEGREES_TO_RADIANS * degrees);
    }

    /**
         * Obtains an angle from a specified number of radians.
         *
         * @param radians the size in radians of the angle to be obtained.
         *
         * @return a new angle, whose size in radians is given by <code>radians</code>.
         */
    public static Angle FromRadians(double radians)
    {
        return new Angle(RADIANS_TO_DEGREES * radians, radians);
    }

    private const double PIOver2 = Math.PI / 2;

    public static Angle FromDegreesLatitude(double degrees)
    {
        degrees = degrees < -90 ? -90 : degrees > 90 ? 90 : degrees;
        var radians = DEGREES_TO_RADIANS * degrees;
        radians = radians < -PIOver2 ? -PIOver2 : radians > PIOver2 ? PIOver2 : radians;

        return new Angle(degrees, radians);
    }

    public static Angle FromRadiansLatitude(double radians)
    {
        radians = radians < -PIOver2 ? -PIOver2 : radians > PIOver2 ? PIOver2 : radians;
        var degrees = RADIANS_TO_DEGREES * radians;
        degrees = degrees < -90 ? -90 : degrees > 90 ? 90 : degrees;

        return new Angle(degrees, radians);
    }

    public static Angle FromDegreesLongitude(double degrees)
    {
        degrees = degrees < -180 ? -180 : degrees > 180 ? 180 : degrees;
        var radians = DEGREES_TO_RADIANS * degrees;
        radians = radians < -Math.PI ? -Math.PI : radians > Math.PI ? Math.PI : radians;

        return new Angle(degrees, radians);
    }

    public static Angle FromRadiansLongitude(double radians)
    {
        radians = radians < -Math.PI ? -Math.PI : radians > Math.PI ? Math.PI : radians;
        var degrees = RADIANS_TO_DEGREES * radians;
        degrees = degrees < -180 ? -180 : degrees > 180 ? 180 : degrees;

        return new Angle(degrees, radians);
    }

    /**
         * Obtains an angle from rectangular coordinates.
         *
         * @param x the abscissa coordinate.
         * @param y the ordinate coordinate.
         *
         * @return a new angle, whose size is determined from <code>x</code> and <code>y</code>.
         */
    public static Angle FromXY(double x, double y)
    {
        var radians = Math.Atan2(y, x);
        return new Angle(RADIANS_TO_DEGREES * radians, radians);
    }

    /**
         * Obtain an angle from a given number of degrees, minutes and seconds.
         *
         * @param degrees integer number of degrees, positive.
         * @param minutes integer number of minutes, positive only between 0 and 60.
         * @param seconds integer number of seconds, positive only between 0 and 60.
         *
         * @return a new angle whose size in degrees is given by <code>degrees</code>, <code>minutes</code> and
         *         <code>seconds</code>.
         *
         * @throws IllegalArgumentException if minutes or seconds are outside the 0-60 range.
         */
    public static Angle FromDMS(int degrees, int minutes, int seconds)
    {
        if (minutes < 0 || minutes >= 60) throw new ArgumentException("Argument Out Of Range");
        if (seconds < 0 || seconds >= 60) throw new ArgumentException("Argument Out Of Range");

        return FromDegrees(Math.Sign(degrees) * (Math.Abs(degrees) + minutes / 60.0 + seconds / 3600.0));
    }

    public static Angle FromDMdS(int degrees, double minutes)
    {
        if (minutes < 0 || minutes >= 60) throw new ArgumentException("Argument Out Of Range");

        return FromDegrees(Math.Sign(degrees) * (Math.Abs(degrees) + minutes / 60.0));
    }

    /**
         * Obtain an angle from a degrees, minute and seconds character string.
         * <p>eg:<pre>
         * 123 34 42
         * -123* 34' 42" (where * stands for the degree symbol)
         * +45* 12' 30" (where * stands for the degree symbol)
         * 45 12 30 S
         * 45 12 30 N
         * </p>
         *
         * @param dmsString the degrees, minute and second character string.
         *
         * @return the corresponding angle.
         *
         * @throws IllegalArgumentException if dmsString is null or not properly formated.
         */
    public static Angle FromDMS(string dmsString)
    {
        throw new NotImplementedException();
    }

    public double degrees;
    public double radians;

    public Angle(Angle angle)
    {
        degrees = angle.degrees;
        radians = angle.radians;
    }

    private Angle(double degrees, double radians)
    {
        this.degrees = degrees;
        this.radians = radians;
    }

    /**
         * Retrieves the size of this angle in degrees. This method may be faster than first obtaining the radians and then
         * converting to degrees.
         *
         * @return the size of this angle in degrees.
         */
    public double Degrees => degrees;

    /**
         * Retrieves the size of this angle in radians. This may be useful for <code>java.lang.Math</code> functions, which
         * generally take radians as trigonometric arguments. This method may be faster that first obtaining the degrees and
         * then converting to radians.
         *
         * @return the size of this angle in radians.
         */
    public double Radians => radians;

    /**
         * Obtains the sum of these two angles. Does not accept a null argument. This method is commutative, so
         * <code>a.add(b)</code> and <code>b.add(a)</code> are equivalent. Neither this angle nor angle is changed, instead
         * the result is returned as a new angle.
         *
         * @param angle the angle to add to this one.
         *
         * @return an angle whose size is the total of this angles and angles size.
         *
         * @throws IllegalArgumentException if angle is null.
         */
    public Angle Add(Angle angle)
    {
        if (angle == null) throw new ArgumentException("Angle Is Null");

        return FromDegrees(degrees + angle.degrees);
    }

    /**
         * Obtains the difference of these two angles. Does not accept a null argument. This method is not commutative.
         * Neither this angle nor angle is changed, instead the result is returned as a new angle.
         *
         * @param angle the angle to subtract from this angle.
         *
         * @return a new angle correpsonding to this angle's size minus angle's size.
         *
         * @throws IllegalArgumentException if angle is null.
         */
    public Angle Subtract(Angle angle)
    {
        if (angle == null) throw new ArgumentException("Angle Is Null");

        return FromDegrees(degrees - angle.degrees);
    }

    /**
         * Multiplies this angle by <code>multiplier</code>. This angle remains unchanged. The result is returned as a new
         * angle.
         *
         * @param multiplier a scalar by which this angle is multiplied.
         *
         * @return a new angle whose size equals this angle's size multiplied by <code>multiplier</code>.
         */
    public Angle Multiply(double multiplier)
    {
        return FromDegrees(degrees * multiplier);
    }

    /**
         * Divides this angle by another angle. This angle remains unchanged, instead the resulting value in degrees is
         * returned.
         *
         * @param angle the angle by which to divide.
         *
         * @return this angle's degrees divided by angle's degrees.
         *
         * @throws IllegalArgumentException if angle is null.
         */
    public double Divide(Angle angle)
    {
        if (angle == null) throw new ArgumentException("Angle Is Null");
        if (angle.Degrees == 0.0) throw new ArgumentException("Divide By Zero");

        return degrees / angle.degrees;
    }

    public Angle AddDegrees(double degrees)
    {
        return FromDegrees(this.degrees + degrees);
    }

    public Angle SubtractDegrees(double degrees)
    {
        return FromDegrees(this.degrees - degrees);
    }

    /**
         * Divides this angle by <code>divisor</code>. This angle remains unchanged. The result is returned as a new angle.
         * Behaviour is undefined if <code>divisor</code> equals zero.
         *
         * @param divisor the number to be divided by.
         *
         * @return a new angle equivalent to this angle divided by <code>divisor</code>.
         */
    public Angle Divide(double divisor)
    {
        return FromDegrees(degrees / divisor);
    }

    public Angle AddRadians(double radians)
    {
        return FromRadians(this.radians + radians);
    }

    public Angle SubtractRadians(double radians)
    {
        return FromRadians(this.radians - radians);
    }

    /**
         * Computes the shortest distance between this and angle, as an angle.
         *
         * @param angle the angle to measure angular distance to.
         *
         * @return the angular distance between this and <code>value</code>.
         */
    public Angle AngularDistanceTo(Angle angle)
    {
        if (angle == null) throw new ArgumentException("Angle Is Null");

        var differenceDegrees = angle.Subtract(this).degrees;
        if (differenceDegrees < -180)
            differenceDegrees += 360;
        else if (differenceDegrees > 180)
            differenceDegrees -= 360;

        var absAngle = Math.Abs(differenceDegrees);
        return FromDegrees(absAngle);
    }

    /**
         * Obtains the sine of this angle.
         *
         * @return the trigonometric sine of this angle.
         */
    public double Sin()
    {
        return Math.Sin(radians);
    }

    public double SinHalfAngle()
    {
        return Math.Sin(0.5 * radians);
    }

    public static Angle Asin(double sine)
    {
        return FromRadians(Math.Asin(sine));
    }

    /**
         * Obtains the cosine of this angle.
         *
         * @return the trigonometric cosine of this angle.
         */
    public double Cos()
    {
        return Math.Cos(radians);
    }

    public double CosHalfAngle()
    {
        return Math.Cos(0.5 * radians);
    }

    public static Angle Acos(double cosine)
    {
        //Tom: this method is not used, should we delete it? (13th Dec 06)
        return FromRadians(Math.Acos(cosine));
    }

    /**
         * Obtains the tangent of half of this angle.
         *
         * @return the trigonometric tangent of half of this angle.
         */
    public double TanHalfAngle()
    {
        return Math.Tan(0.5 * radians);
    }

    public static Angle Atan(double tan)
    {
        //Tom: this method is not used, should we delete it? (13th Dec 06)
        return FromRadians(Math.Atan(tan));
    }

    /**
         * Obtains the average of two angles. This method is commutative, so <code>midAngle(m, n)</code> and
         * <code>midAngle(n, m)</code> are equivalent.
         *
         * @param a1 the first angle.
         * @param a2 the second angle.
         *
         * @return the average of <code>a1</code> and <code>a2</code> throws IllegalArgumentException if either angle is
         *         null.
         */
    public static Angle MidAngle(Angle a1, Angle a2)
    {
        if (a1 == null || a2 == null) throw new ArgumentException("Angle Is Null");

        return FromDegrees(0.5 * (a1.degrees + a2.degrees));
    }

    /**
         * Obtains the average of three angles. The order of parameters does not matter.
         *
         * @param a the first angle.
         * @param b the second angle.
         *
         * @return the average of <code>a1</code>, <code>a2</code> and <code>a3</code>
         *
         * @throws IllegalArgumentException if <code>a</code> or <code>b</code> is null
         */
    public static Angle Average(Angle a, Angle b)
    {
        if (a == null || b == null) throw new ArgumentException("Angle Is Null");

        return FromDegrees(0.5 * (a.degrees + b.degrees));
    }

    /**
         * Obtains the average of three angles. The order of parameters does not matter.
         *
         * @param a the first angle.
         * @param b the second angle.
         * @param c the third angle.
         *
         * @return the average of <code>a1</code>, <code>a2</code> and <code>a3</code>.
         *
         * @throws IllegalArgumentException if <code>a</code>, <code>b</code> or <code>c</code> is null.
         */
    public static Angle Average(Angle a, Angle b, Angle c)
    {
        if (a == null || b == null || c == null) throw new ArgumentException("Angle Is Null");

        return FromDegrees((a.degrees + b.degrees + c.degrees) / 3);
    }

    /**
         * Linearly interpolates between two angles.
         *
         * @param amount the interpolant.
         * @param value1 the first angle.
         * @param value2 the second angle.
         *
         * @return a new angle between <code>value1</code> and <code>value2</code>.
         */
    public static Angle Mix(double amount, Angle value1, Angle value2)
    {
        throw new NotImplementedException();
/*
            if (value1 == null || value2 == null)
            {
                throw new ArgumentException("Angle Is Null");
            }

            if (amount < 0)
                return value1;
            else if (amount > 1)
                return value2;

            Quaternion quat = Quaternion.slerp(
                amount,
                Quaternion.FromAxisAngle(value1, Vec4.UNIT_X),
                Quaternion.FromAxisAngle(value2, Vec4.UNIT_X));

            Angle angle = quat.getRotationX();
            if (Double.isNaN(angle.degrees))
                return null;

            return angle;
*/
    }

    /**
         * Compares this {@link Angle} with another. Returns a negative integer if this is the smaller angle, a positive
         * integer if this is the larger, and zero if both angles are equal.
         *
         * @param angle the angle to compare against.
         *
         * @return -1 if this angle is smaller, 0 if both are equal and +1 if this angle is larger.
         *
         * @throws IllegalArgumentException if angle is null.
         */
    public int CompareTo(Angle angle)
    {
        if (angle == null) throw new ArgumentException("Angle Is Null");

        if (degrees < angle.degrees)
            return -1;

        if (degrees > angle.degrees)
            return 1;

        return 0;
    }

    private static double NormalizedDegreesLatitude(double degrees)
    {
        var lat = degrees % 180;
        return lat > 90 ? 180 - lat : lat < -90 ? -180 - lat : lat;
    }

    private static double NormalizedDegreesLongitude(double degrees)
    {
        var lon = degrees % 360;
        return lon > 180 ? lon - 360 : lon < -180 ? 360 + lon : lon;
    }

    public static Angle NormalizeLatitude(Angle unnormalizedAngle)
    {
        if (unnormalizedAngle == null) throw new ArgumentException("Angle Is Null");

        return FromDegrees(NormalizedDegreesLatitude(unnormalizedAngle.degrees));
    }

    public static Angle NormalizeLongitude(Angle unnormalizedAngle)
    {
        if (unnormalizedAngle == null) throw new ArgumentException("Angle Is Null");

        return FromDegrees(NormalizedDegreesLongitude(unnormalizedAngle.degrees));
    }

    public Angle NormalizedLatitude => NormalizeLatitude(this);

    public Angle NormalizedLongitude => NormalizeLongitude(this);

    public static bool CrossesLongitudeBoundary(Angle angleA, Angle angleB)
    {
        if (angleA == null || angleB == null) throw new ArgumentException("Angle Is Null");

        // A segment cross the line if end pos have different longitude signs
        // and are more than 180 degress longitude apart
        return Math.Sign(angleA.degrees) != Math.Sign(angleB.degrees)
               && Math.Abs(angleA.degrees - angleB.degrees) > 180;
    }

    public static bool IsValidLatitude(double value)
    {
        return value >= -90 && value <= 90;
    }

    public static bool IsValidLongitude(double value)
    {
        return value >= -180 && value <= 180;
    }

    public static Angle Max(Angle a, Angle b)
    {
        return a.degrees >= b.degrees ? a : b;
    }

    public static Angle Min(Angle a, Angle b)
    {
        return a.degrees <= b.degrees ? a : b;
    }

    /**
         * Obtains a <code>String</code> representation of this angle.
         *
         * @return the value of this angle in degrees and as a <code>String</code>.
         */
    public override string ToString()
    {
        return degrees.ToString() + '\u00B0';
    }

    /**
         * Forms a decimal degrees {@link String} representation of this {@link Angle}.
         *
         * @param digits the number of digits past the decimal point to include in the string.
         *
         * @return the value of this angle in decimal degrees as a string with the specified number of digits beyond the
         *         decimal point. The string is padded with trailing zeros to fill the number of post-decimal point
         *         positions requested.
         */
    public string ToDecimalDegreesString(int digits)
    {
        if (digits < 0 || digits > 15) throw new ArgumentException("Argument Out Of Range");

        return string.Format("{0:F" + digits + "}", degrees);
    }

    /**
         * Obtains a {@link String} representation of this {@link Angle} formated as degrees, minutes and seconds
         * integer values.
         *
         * @return the value of this angle in degrees, minutes, seconds as a string.
         */
    public string ToDMSString()
    {
        var temp = degrees;
        var sign = (int)Math.Sign(temp);
        temp *= sign;
        var d = (int)Math.Floor(temp);
        temp = (temp - d) * 60d;
        var m = (int)Math.Floor(temp);
        temp = (temp - m) * 60d;
        var s = (int)Math.Round(temp);

        if (s == 60)
        {
            m++;
            s = 0;
        } // Fix rounding errors

        if (m == 60)
        {
            d++;
            m = 0;
        }

        return (sign == -1 ? "-" : "") + d + '\u00B0' + ' ' + m + '\u2019' + ' ' + s + '\u201d';
    }

    public string ToFormattedDMSString()
    {
        var temp = degrees;
        var sign = (int)Math.Sign(temp);

        temp *= sign;
        var d = (int)Math.Floor(temp);
        temp = (temp - d) * 60d;
        var m = (int)Math.Floor(temp);
        temp = (temp - m) * 60d;
        var s = Math.Round(temp * 100, MidpointRounding.ToEven) / 100; // keep two decimals for seconds

        if (s == 60)
        {
            m++;
            s = 0;
        } // Fix rounding errors

        if (m == 60)
        {
            d++;
            m = 0;
        }

        return string.Format("{0:D4}\u00B0 {1:D2}\u2019 {2,5:F2}\u201d", sign * d, m, s);
    }

    public double[] ToDMS()
    {
        var temp = degrees;
        var sign = (int)Math.Sign(temp);

        temp *= sign;
        var d = (int)Math.Floor(temp);
        temp = (temp - d) * 60d;
        var m = (int)Math.Floor(temp);
        temp = (temp - m) * 60d;
        var s = Math.Round(temp * 100, MidpointRounding.ToEven) / 100; // keep two decimals for seconds

        if (s == 60)
        {
            m++;
            s = 0;
        } // Fix rounding errors

        if (m == 60)
        {
            d++;
            m = 0;
        }

        return new double[] { sign * d, m, s };
    }
}