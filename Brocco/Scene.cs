using Microsoft.Xna.Framework.Graphics;

namespace Brocco;

public abstract class Scene
{
    public virtual void Update() {}
    public virtual void Render(SpriteBatch spriteBatch) {}
}
