using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Brocco;

/// <summary>
/// A class that represents a rendered object with a special behaviour.
/// </summary>
public abstract class Entity
{
    private static uint _nextEntityId = 0;
    
    internal bool CanDispose { get; private set; }
    
    /// <summary>
    /// This entity's unique ID.
    /// </summary>
    public readonly uint Id = _nextEntityId++;

    /// <summary>
    /// The current position of this entity.
    /// </summary>
    public Vector2 Position = Vector2.Zero;

    /// <summary>
    /// This entity's current color. Mainly used for rendering.
    /// </summary>
    public Color Color = Color.White;

    /// <summary>
    /// This entity's current transparency. Mainly used for rendering.
    /// </summary>
    public float Alpha = 1f;

    /// <summary>
    /// This entity's rotation. Mainly used for rendering.
    /// </summary>
    /// <remarks>Rotation is measured in radians.</remarks>
    public float Rotation = 0f;

    /// <summary>
    /// This entity's scale. Mainly used for rendering.
    /// </summary>
    public Vector2 Scale = Vector2.One;

    /// <summary>
    /// This entity's current texture. If the texture is not set, it will default to a white pixel.
    /// </summary>
    public Texture2D CurrentTexture = null;

    /// <summary>
    /// This entity's depth on screen. Mainly used for rendering.
    /// </summary>
    public float LayerDepth = 1f;

    /// <summary>
    /// This entity's current scene.
    /// </summary>
    public Scene Scene { get; internal set; }

    /// <summary>
    /// This method is called every frame.
    /// </summary>
    public abstract void Update(float dt);

    /// <summary>
    /// This method renders your entity and is called every frame. Override this to change the rendering code.
    /// </summary>
    /// <param name="spriteBatch">The <c>SpriteBatch</c> instance used in the Brocco Game Loop</param>
    public virtual void Render(SpriteBatch spriteBatch)
    {
        var tex = CurrentTexture ?? Assets.Pixel;
        spriteBatch.Draw(tex, Position, null, Color * Alpha, Rotation, new Vector2((Scale.X * tex.Width) / 2f, (Scale.Y * tex.Height) / 2f), Scale, SpriteEffects.None, LayerDepth);
    }

    /// <summary>
    /// Queue this entity for removal on next frame
    /// </summary>
    public void Dispose()
    {
        CanDispose = true;
    }
}
