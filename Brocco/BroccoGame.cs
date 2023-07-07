using System;
using Brocco.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Size = System.Drawing.Size;

namespace Brocco;

/// <summary>
/// A Brocco Game Loop class.
/// </summary>
public sealed class BroccoGame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private RenderTarget2D _canvas;
    private Size _canvasSize;
    private Vector2 _screenCenter;
    private float _canvasRenderScale;
    private Vector2 _canvasDrawOffset;

    public static Texture2D Pixel { get; private set; }

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
        Content.RootDirectory = "Content";
        IsMouseVisible = settings.ShowMouse;
        // TODO: user resizing logic and auto scaling?
        _canvasSize = settings.CanvasSize;
        _canvasDrawOffset = new Vector2(_canvasSize.Width / 2f, _canvasSize.Height / 2f);
        var sz = settings.Resolution;
        SetResolution(sz.Width, sz.Height);
    }

    protected override void LoadContent()
    {
        // TODO: make sprites and background sprite rendering system
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        // TODO: add canvas size option in constructor
        _canvas = new RenderTarget2D(GraphicsDevice, _canvasSize.Width, _canvasSize.Height);
        Pixel = new Texture2D(GraphicsDevice, 1, 1);
        Pixel.SetData(new[] {Color.White});

        // TODO: content loading?
        SceneManager.LoadScenes();
    }

    protected override void Update(GameTime gameTime)
    {
        InputManager.Update();
        SceneManager.Update();
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.SetRenderTarget(_canvas);
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
        SceneManager.Render(_spriteBatch);
        _spriteBatch.End();
        GraphicsDevice.SetRenderTarget(null);
        GraphicsDevice.Clear(Color.Black);
        _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
        _spriteBatch.Draw(_canvas, _screenCenter, null, Color.White, 0f, _canvasDrawOffset, _canvasRenderScale, SpriteEffects.None, 1f);
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
    }
}
