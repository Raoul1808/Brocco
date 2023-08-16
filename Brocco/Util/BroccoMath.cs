using System;
using Microsoft.Xna.Framework;

namespace Brocco.Util;

public static class BroccoMath
{
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
