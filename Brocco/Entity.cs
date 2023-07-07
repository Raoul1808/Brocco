using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Brocco;

/// <summary>
/// A class that represents a rendered object with a special behaviour.
/// </summary>
public abstract class Entity
{
    private static uint _nextEntityId = 0;
    
    /// <summary>
    /// This entity's unique ID.
    /// </summary>
    public readonly uint Id = _nextEntityId++;

    /// <summary>
    /// The current position of this entity.
    /// </summary>
    protected Vector2 Position = Vector2.Zero;

    /// <summary>
    /// This entity's current color. Mainly used for rendering.
    /// </summary>
    protected Color Color = Color.White;

    /// <summary>
    /// This entity's current transparency. Mainly used for rendering.
    /// </summary>
    protected float Alpha = 1f;

    /// <summary>
    /// This entity's rotation. Mainly used for rendering.
    /// </summary>
    /// <remarks>Rotation is measured in radians.</remarks>
    protected float Rotation = 0f;

    /// <summary>
    /// This entity's scale. Mainly used for rendering.
    /// </summary>
    protected Vector2 Scale = Vector2.One;

    /// <summary>
    /// This entity's current texture. If the texture is not set, it will default to a white pixel.
    /// </summary>
    protected Texture2D CurrentTexture = null;

    /// <summary>
    /// This entity's depth on screen. Mainly used for rendering.
    /// </summary>
    protected float LayerDepth = 1f;

    /// <summary>
    /// This method is called every frame.
    /// </summary>
    public abstract void Update();

    /// <summary>
    /// This method renders your entity and is called every frame. Override this to change the rendering code.
    /// </summary>
    /// <param name="spriteBatch">The <c>SpriteBatch</c> instance used in the Brocco Game Loop</param>
    public virtual void Render(SpriteBatch spriteBatch)
    {
        var tex = CurrentTexture ?? BroccoGame.Pixel;
        spriteBatch.Draw(tex, Position, null, Color * Alpha, Rotation, new Vector2(CurrentTexture.Width / 2f, CurrentTexture.Height / 2f), Scale, SpriteEffects.None, LayerDepth);
    }
}
