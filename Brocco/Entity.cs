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
    public Vector2 Position { get; protected set; }
    
    /// <summary>
    /// This entity's current color. Mainly used for rendering.
    /// </summary>
    public Color Color { get; protected set; }
    
    /// <summary>
    /// This entity's rotation. Mainly used for rendering.
    /// </summary>
    /// <remarks>Rotation is measured in radians.</remarks>
    public float Rotation { get; protected set; }

    /// <summary>
    /// This entity's scale. Mainly used for rendering.
    /// </summary>
    public Vector2 Scale { get; protected set; }

    /// <summary>
    /// This entity's current texture. If the texture is not set, it will default to a white pixel.
    /// </summary>
    protected Texture2D CurrentTexture;

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
        spriteBatch.Draw(tex, Position, null, Color, Rotation, Vector2.One * 0.5f, Scale, SpriteEffects.None, 0f);
    }
}
