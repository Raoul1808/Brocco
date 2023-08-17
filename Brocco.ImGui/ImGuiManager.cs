using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Brocco.ImGuiNet;

public class ImGuiManager : BroccoAutoSystem
{
    private static ImGuiRenderer _imGuiRenderer;
    public static event Action OnLayout;

    public override void PostInitialize(BroccoGame game)
    {
        _imGuiRenderer = new ImGuiRenderer(game);
        _imGuiRenderer.RebuildFontAtlas();
    }

    internal static void Layout(GameTime gameTime)
    {
        _imGuiRenderer.BeforeLayout(gameTime);
        OnLayout?.Invoke();
        _imGuiRenderer.AfterLayout();
    }

    public override void Render(SpriteBatch spriteBatch, GameTime gameTime)
    {
        Layout(gameTime);
    }
}
