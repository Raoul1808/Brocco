using System;
using Microsoft.Xna.Framework;

namespace Brocco.Util;

public static class BroccoMath
{
    /// <summary>
    /// Restricts a value to be within a specified range.
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">
    /// The minimum value. If <c>value</c> is less than <c>min</c>, <c>min</c>
    /// will be returned.
    /// </param>
    /// <param name="max">
    /// The maximum value. If <c>value</c> is greater than <c>max</c>, <c>max</c>
    /// will be returned.
    /// </param>
    /// <returns>The clamped value.</returns>
    public static int Clamp(int value, int min, int max)
    {
        value = (value > max) ? max : value;
        value = (value < min) ? min : value;
        return value;
    }
    
    /// <summary>
    /// Calculates the distance between two points.
    /// </summary>
    /// <param name="p1">First point</param>
    /// <param name="p2">Second point</param>
    /// <returns>The distance between both points as a floating point number.</returns>
    public static float Distance(Point value1, Point value2)
    {
        float v1 = value1.X - value2.X, v2 = value1.Y - value2.Y;
        return (float) Math.Sqrt((v1 * v1) + (v2 * v2));
    }
}
