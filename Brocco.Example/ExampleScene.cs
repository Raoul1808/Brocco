using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Brocco.Example;

public class ExampleScene : Scene
{
    public override void Render(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(BroccoGame.Pixel, new Rectangle(100, 100, 100, 100), Color.Red);
    }
}
