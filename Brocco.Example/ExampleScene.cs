using System;
using Brocco.Input;
using Brocco.Menu;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Brocco.Example;

public class ExampleScene : Scene
{
    private Player _player;
    private FontSystem _font;
    private MenuObject _pauseMenu;
    
    public override void Load()
    {
        _player = AddToScene<Player>();
        _font = Assets.GetFontSystem("Noto Sans");
        _pauseMenu = MenuBuilder.CreateMenu(_font, new Vector2(300, 100))
            .AddButton("Resume", sender => { PauseUpdate = false; })
            .AddToggle("Yes")
            .AddArraySelect("I am", new [] {"Raoul1808", "Mew", "Both"})
            .AddButton("Exit", sender => {ExitGame();})
            .Build();
    }

    public override void Update(float dt)
    {
        base.Update(dt);
        if (InputManager.GetKeyPress(Keys.Escape))
            PauseUpdate = !PauseUpdate;
        if (PauseUpdate)
        {
            _pauseMenu.Update();
        }

        if (InputManager.GetClickDown(MouseButtons.Left))
        {
            Vector2 pos = InputManager.GetCanvasMousePosition();
            _player.Position = pos;
            Console.WriteLine(pos);
        }

        var keys = InputManager.GetNewKeyPresses();
        if (keys.Length > 0)
        {
            Console.Write("New presses this frame: ");
            foreach (var key in keys)
                Console.Write(key + " ");
            Console.WriteLine();
        }
    }

    public override void ScreenRender(SpriteBatch spriteBatch)
    {
        if (PauseUpdate)
            _pauseMenu.Render(spriteBatch);
    }
}
