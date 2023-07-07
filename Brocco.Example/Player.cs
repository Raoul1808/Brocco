using Brocco.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Brocco.Example;

public class Player : Entity
{
    public Player()
    {
        Color = Color.Blue;
        Scale = Vector2.One * 50f;
    }
    
    public override void Update()
    {
        if (InputManager.GetKeyDown(Keys.D))
            Position.X++;
        if (InputManager.GetKeyDown(Keys.Q))
            Position.X--;
        if (InputManager.GetKeyDown(Keys.Z))
            Position.Y--;
        if (InputManager.GetKeyDown(Keys.S))
            Position.Y++;
    }
}
