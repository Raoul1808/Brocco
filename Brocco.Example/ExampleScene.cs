using Brocco.Menu;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Brocco.Example;

public class ExampleScene : Scene
{
    private Player _player;
    private FontSystem _font;
    private MenuObject _menu;
    
    public override void Load()
    {
        AddToScene(_player = new Player());
        _font = Assets.GetFontSystem("Noto Sans");
        _menu = MenuBuilder.CreateMenu(_font, Vector2.One * 100)
            .AddEntry("New Game")
            .AddEntry("Options")
            .AddEntry("Credits")
            .AddEntry("Exit")
            .Build();
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
