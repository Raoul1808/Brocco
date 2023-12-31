using System;
using Brocco.ImGuiNet;
using Brocco.Input;
using Brocco.Menu;
using FontStashSharp;
using ImGuiNET;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Brocco.Example;

public class ExampleScene : Scene
{
    private Player _player;
    private Obstacle _obstacle;
    private FontSystem _font;
    private MenuObject _pauseMenu;
    
    public override void Load()
    {
        CanvasEffect = Assets.GetEffect("TestShader");

        var menuSettings = new MenuSettings
        {
            FontSize = 32f,
            SelectEffect = MenuSelectEffect.Underline,
            FontEffect = FontSystemEffect.Stroked,
            FontEffectStrength = 1,
        };
        
        _obstacle = AddToScene<Obstacle>();
        _player = AddToScene<Player>();
        _font = Assets.GetFontSystem("Noto Sans");
        _pauseMenu = MenuBuilder.CreateMenu(_font, new Vector2(300, 100), menuSettings)
            .AddButton("Resume", sender => { PauseUpdate = false; })
            .AddToggle("Yes")
            .AddArraySelect("I am", new [] {"Raoul1808", "Mew", "Both"})
            .AddTextInput("Please enter your name", action: (_, text) => Console.WriteLine("Hello " + text))
            .AddButton("Exit", sender => {ExitGame();})
            .Build();
    }

    public override void OnBecomeActive()
    {
        ImGuiManager.OnLayout += TestLayout;
    }

    public override void OnBecomeInactive()
    {
        ImGuiManager.OnLayout -= TestLayout;
    }

    public override void Update(float dt)
    {
        base.Update(dt);
        if (InputManager.GetKeyPress(Keys.Escape))
            PauseUpdate = !PauseUpdate;
        if (PauseUpdate)
        {
            ClearColor = Color.Black;
            _pauseMenu.Update();
        }
        else
            ClearColor = null;
        
        _player.CollidedWith(_obstacle);

        if (InputManager.GetClickDown(MouseButtons.Left))
        {
            Vector2 pos = InputManager.GetCanvasMousePosition();
            _player.Position = pos;
            // Console.WriteLine(pos);
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

    private void TestLayout()
    {
        // ImGui.Begin("Hello, Word!");
        // if (ImGui.Button("Hola"))
        // {
        //     Console.WriteLine("Hallo");
        // }
        // ImGui.End();
    }

    public override void ScreenRender(SpriteBatch spriteBatch)
    {
        if (PauseUpdate)
            _pauseMenu.Render(spriteBatch);
    }
}
