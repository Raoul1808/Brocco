using Microsoft.Xna.Framework;

namespace Brocco.Example;

public class Obstacle : Entity
{
    public Obstacle()
    {
        Color = Color.Black;
        Position = new Vector2(100, 100);
        Scale = new Vector2(100, 100);
    }
    
    public override void Update(float dt)
    {
    }
}
