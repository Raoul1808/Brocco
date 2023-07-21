using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework;

namespace Brocco.Basic;

/// <summary>
/// A class that represents a Size. Created to remove a dependency to System.Drawing
/// </summary>
public struct Size : IEquatable<Size>
{
    public int Width;
    public int Height;

    public Size()
    {
        Width = 0;
        Height = 0;
    }

    public Size(int size)
    {
        Width = Height = size;
    }

    public Size(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public Size(Point point)
    {
        Width = point.X;
        Height = point.Y;
    }
    
    public bool Equals(Size other)
    {
        return this.Width == other.Width &&
               this.Height == other.Height;
    }

    public override bool Equals(object obj)
    {
        return obj is Size other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Width * Height;  // TODO: find better hash code than this
    }

    public static Size operator +(Size a) => a;
    public static Size operator -(Size a) => new(-a.Width, -a.Height);

    public static Size operator +(Size a, Size b) => new(a.Width + b.Width, a.Height + b.Height);

    public static Size operator -(Size a, Size b) => a + (-b);

    public static Size operator *(Size a, int b) => a * new Size(b);
    public static Size operator *(Size a, Size b) => new(a.Width * b.Width, a.Height * b.Height);

    public static Size operator /(Size a, int b) => a / new Size(b);
    public static Size operator /(Size a, Size b) => new(a.Width / b.Width, a.Height / b.Height);

    public override string ToString() => $"{{Width: {Width}, Height: {Height}}}";
}
