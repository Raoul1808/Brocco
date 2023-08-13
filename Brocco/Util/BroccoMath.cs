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
}
