﻿using Brocco.Basic;
using Brocco.ImGuiNet;
using Microsoft.Xna.Framework;

namespace Brocco.Example;

internal class Program
{
    public static void Main(string[] args)
    {
        using BroccoGame game = new BroccoGame(new BroccoGameSettings
        {
            ShowMouse = true,
            CanvasSize = new Size(384, 216),
            CanResize = true,
            ClearColor = Color.CornflowerBlue,
        });
        game.Window.Title = "Example Game Made With Brocco";
        game.AddSystem<ImGuiManager>();

        Assets.EffectsRoot = "Effects";
        Assets.FontsRoot = "Fonts";
        Assets.SoundsRoot = "Sounds";
        Assets.TexturesRoot = "Textures";
        
        Assets.PreloadFont("Noto Sans", new[] {"NotoSans-Medium.ttf"});

        SceneManager.Add("Example Scene", new ExampleScene());
        
        game.Run();
    }
}
