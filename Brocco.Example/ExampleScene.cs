using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Brocco.Example;

public class ExampleScene : Scene
{
    private Player _player;
    private FontSystem _font;
    
    public override void Load()
    {
        AddToScene(_player = new Player());
        _font = Assets.GetFontSystem("Noto Sans");
    }

    public override void ScreenRender(SpriteBatch spriteBatch)
    {
        var font = _font.GetFont(32);
        spriteBatch.DrawString(font, "Hello, World!", Vector2.One * 100f, Color.Red);
    }
}
