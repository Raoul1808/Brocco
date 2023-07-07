using Brocco.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Brocco.Example;

public class ExampleScene : Scene
{
    private Vector2 _rectPos = Vector2.Zero * 100f;

    public override void Update()
    {
        if (InputManager.GetKeyDown(Keys.D))
            _rectPos.X++;
        if (InputManager.GetKeyDown(Keys.Q))
            _rectPos.X--;
        if (InputManager.GetKeyDown(Keys.Z))
            _rectPos.Y--;
        if (InputManager.GetKeyDown(Keys.S))
            _rectPos.Y++;
    }

    public override void Render(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(BroccoGame.Pixel, new Rectangle((int)_rectPos.X, (int)_rectPos.Y, 50, 50), Color.Red);
    }
}
