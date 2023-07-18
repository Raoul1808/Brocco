using Microsoft.Xna.Framework.Graphics;

namespace Brocco.Example;

public class ExampleScene : Scene
{
    private Player _player;
    
    public override void Load()
    {
        AddToScene(_player = new Player());
    }

    public override void ScreenRender(SpriteBatch spriteBatch)
    {
        _player.Render(spriteBatch);
    }
}
