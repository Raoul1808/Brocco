using Brocco.Input;
using Microsoft.Xna.Framework;
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
        Vector2 vel = Vector2.Zero;
        if (InputManager.GetKeyDown(Keys.D) || InputManager.GetButtonDown(Buttons.LeftThumbstickRight))
            vel.X++;
        if (InputManager.GetKeyDown(Keys.Q) || InputManager.GetButtonDown(Buttons.LeftThumbstickLeft))
            vel.X--;
        if (InputManager.GetKeyDown(Keys.Z) || InputManager.GetButtonDown(Buttons.LeftThumbstickUp))
            vel.Y--;
        if (InputManager.GetKeyDown(Keys.S) || InputManager.GetButtonDown(Buttons.LeftThumbstickDown))
            vel.Y++;

        if (vel != Vector2.Zero)
            vel.Normalize();

        vel *= 2f;

        Position += vel;

        if (InputManager.GetKeyPress(Keys.Space))
            _sound.Play();
    }
}
