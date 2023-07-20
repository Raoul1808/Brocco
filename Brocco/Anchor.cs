using Microsoft.Xna.Framework;

namespace Brocco;

public enum Anchor
{
    TopLeft,
    TopCenter,
    TopRight,
    MiddleLeft,
    MiddleCenter,
    MiddleRight,
    BottomLeft,
    BottomCenter,
    BottomRight,
}

public static class AnchorExtensions
{
    public static Vector2 ToVector2(this Anchor anchor)
    {
        return anchor switch
        {
            Anchor.TopLeft => Vector2.Zero,
            Anchor.TopCenter => new Vector2(.5f, 0f),
            Anchor.TopRight => new Vector2(1f, 0f),
            Anchor.MiddleLeft => new Vector2(0f, .5f),
            Anchor.MiddleCenter => new Vector2(.5f, .5f),
            Anchor.MiddleRight => new Vector2(1f, .5f),
            Anchor.BottomLeft => new Vector2(0f, 1f),
            Anchor.BottomCenter => new Vector2(.5f, 1f),
            Anchor.BottomRight => Vector2.One,
            _ => Vector2.Zero,
        };
    }
}
