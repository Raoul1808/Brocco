using System;
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
        _menu = MenuBuilder.CreateMenu(_font, new Vector2(300, 100))
            .AddButton("New Game")
            .AddToggle("Is This a Toggle?")
            .AddArraySelect("Select between these meme numbers", new int[] { 69, 420, 727, 1116 })
            .AddButton("Exit", _ => {Console.WriteLine("Sorry but there is no escape");})
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
