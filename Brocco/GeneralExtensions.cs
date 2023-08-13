using System;
using Microsoft.Xna.Framework;

namespace Brocco;

public static class GeneralExtensions
{
    public static Vector2 ToVector2(this Point point) => new(point.X, point.Y);
    public static Point ToPoint(this Vector2 vector) => new((int)Math.Round(vector.X), (int)Math.Round(vector.Y));
}
