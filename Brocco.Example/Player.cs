using Brocco.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;

namespace Brocco.Example;

public class Player : Entity
{
    private SoundEffect _sound;
    
    public Player()
    {
        CurrentTexture = Assets.GetTexture("PlayerShip");
        _sound = Assets.GetSound("coin");
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

        if (InputManager.GetKeyPress(Keys.Space))
            _sound.Play();
    }
}
