using Brocco.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Brocco.Example;

public class Player : Entity
{
    public Player()
    {
        CurrentTexture = BroccoGame.Pixel;
        Color = Color.Blue;
    }
    
    public override void Update()
    {
        var pos = Position;
        
        if (InputManager.GetKeyDown(Keys.D))
            pos.X++;
        if (InputManager.GetKeyDown(Keys.Q))
            pos.X--;
        if (InputManager.GetKeyDown(Keys.Z))
            pos.Y--;
        if (InputManager.GetKeyDown(Keys.S))
            pos.Y++;

        Position = pos;
    }
}
