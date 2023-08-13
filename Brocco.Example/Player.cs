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
    
    public override void Update(float dt)
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

        Velocity = vel;
    }

    public void CollidedWith(Entity other)
    {
        if (other.GetType() != typeof(Obstacle))
            return;

        float halfHeight = BoundingBox.Height * 0.5f;
        float halfWidth = BoundingBox.Width * 0.5f;
        
        if (this.TouchedBottomOf(other))
        {
            Position.Y = other.BoundingBox.Bottom + halfHeight;
            Velocity.Y = 0;
        }

        if (this.TouchedTopOf(other))
        {
            Position.Y = other.BoundingBox.Top - halfHeight;
            Velocity.Y = 0;
        }

        if (this.TouchedLeftOf(other))
        {
            Position.X = other.BoundingBox.Left - halfWidth;
            Velocity.X = 0;
        }

        if (this.TouchedRightOf(other))
        {
            Position.X = other.BoundingBox.Right + halfWidth;
            Velocity.X = 0;
        }
    }
}
