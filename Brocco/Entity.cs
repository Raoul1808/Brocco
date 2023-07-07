using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Brocco;

public abstract class Entity
{
    private static uint _nextEntityId = 0;
    
    public uint Id = _nextEntityId++;
    public Vector2 Position { get; protected set; }
    public Color Color { get; protected set; }
    public float Rotation { get; protected set; }
    protected Texture2D CurrentTexture;

    public abstract void Update();

    public virtual void Render(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(CurrentTexture, Position, null, Color, Rotation, Vector2.One * 0.5f, 1f, SpriteEffects.None, 0f);
    }
}
