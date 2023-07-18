using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Brocco.Example;

public class ExampleScene : Scene
{
    private Player _player;
    private FontSystem _font;
    private Menu _menu;
    
    public override void Load()
    {
        AddToScene(_player = new Player());
        _font = Assets.GetFontSystem("Noto Sans");
        _menu = new Menu(_font, Vector2.One * 100, 32);
        _menu.AddOption("New Game");
        _menu.AddOption("Options");
        _menu.AddOption("Credits");
        _menu.AddOption("Exit");
    }

    public override void Update()
    {
        base.Update();
        _menu.Update();
    }

    public override void ScreenRender(SpriteBatch spriteBatch)
    {
        _menu.Render(spriteBatch);
    }
}
