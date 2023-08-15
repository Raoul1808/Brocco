using System;
using Brocco.Basic;
using Brocco.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SDL2;

namespace Brocco;

/// <summary>
/// A Brocco Game Loop class.
/// </summary>
public sealed class BroccoGame : Game
{
    private readonly GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private RenderTarget2D _canvas;
    private readonly Size _canvasSize;
    private Vector2 _screenCenter;
    private float _canvasRenderScale;
    private readonly Vector2 _canvasDrawOffset;
    private readonly Color _clearColor;

    /// <summary>
    /// Creates a Brocco Game Loop with default settings.
    /// </summary>
    public BroccoGame()
        : this(new BroccoGameSettings())
    {
    }
    
    /// <summary>
    /// Creates a Brocco Game Loop using the passed settings.
    /// </summary>
    /// <param name="settings">The settings for the Brocco Game Loop</param>
    public BroccoGame(BroccoGameSettings settings)
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = settings.AssetsDirectory;
        IsMouseVisible = settings.ShowMouse;
        _canvasSize = settings.CanvasSize;
        _canvasDrawOffset = new Vector2(_canvasSize.Width / 2f, _canvasSize.Height / 2f);
        var sz = settings.Resolution ?? settings.CanvasSize * 2;
        SetResolution(sz.Width, sz.Height);
        _clearColor = settings.ClearColor;
        Window.AllowUserResizing = settings.CanResize;
        Window.ClientSizeChanged += (sender, args) =>
        {
            SetResolution(Window.ClientBounds.Width, Window.ClientBounds.Height);
        };
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _canvas = new RenderTarget2D(GraphicsDevice, _canvasSize.Width, _canvasSize.Height);
        Assets.Prepare(Content, GraphicsDevice);
        Assets.LoadFonts();
        SceneManager.LoadScenes();
    }

    protected override void Update(GameTime gameTime)
    {
        if (SceneManager.ReceivedStop())
            Exit();
        InputManager.Update();
        SceneManager.Update((float) gameTime.ElapsedGameTime.TotalSeconds);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.SetRenderTarget(_canvas);
        GraphicsDevice.Clear(SceneManager.GetClearColor() ?? _clearColor);
        _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
        SceneManager.CanvasRender(_spriteBatch);
        _spriteBatch.End();
        GraphicsDevice.SetRenderTarget(null);
        GraphicsDevice.Clear(Color.Black);
        _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, SceneManager.GetCanvasEffect());
        _spriteBatch.Draw(_canvas, _screenCenter, null, Color.White, 0f, _canvasDrawOffset, _canvasRenderScale, SpriteEffects.None, 1f);
        _spriteBatch.End();
        _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, SceneManager.GetScreenEffect());
        SceneManager.ScreenRender(_spriteBatch);
        _spriteBatch.End();
        base.Draw(gameTime);
    }

    /// <summary>
    /// Changes the game's viewport/resolution.
    /// </summary>
    /// <param name="width">Width in pixels</param>
    /// <param name="height">Height in pixels</param>
    public void SetResolution(int width, int height)
    {
        _graphics.PreferredBackBufferWidth = width;
        _graphics.PreferredBackBufferHeight = height;
        _graphics.ApplyChanges();

        _screenCenter = new Vector2(width / 2f, height / 2f);
        _canvasRenderScale = Math.Min(width / (float)_canvasSize.Width, height / (float)_canvasSize.Height);
        InputManager.CanvasRenderScale = _canvasRenderScale;
    }
}
