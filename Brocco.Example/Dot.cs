using Microsoft.Xna.Framework;

namespace Brocco.Example;

public class Dot : Entity
{
    private float _lifeTimer = 5f;

    public Dot()
    {
        Color = Color.Red;
        Scale = Vector2.One * 5f;
    }

    public override void CollidedWith(Entity other)
    {
        if (other.GetType() == typeof(Player))
            Position.Y--;
    }

    public override void Update(float dt)
    {
        _lifeTimer -= dt;
        if (_lifeTimer <= 0)
            Dispose();
    }
}
