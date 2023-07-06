using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Brocco;

public sealed class BroccoGame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private RenderTarget2D _canvas;
    private Point _canvasSize;
    private Vector2 _screenCenter;
    private float _canvasRenderScale;

    public static Texture2D Pixel { get; private set; }

    public BroccoGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        // TODO: user resizing logic and auto scaling?
        _canvasSize = new Point(640, 360);
        SetResolution(1280, 720);
    }

    protected override void LoadContent()
    {
        // TODO: make sprites and background sprite rendering system
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        // TODO: add canvas size option in constructor
        _canvas = new RenderTarget2D(GraphicsDevice, _canvasSize.X, _canvasSize.Y);
        // TODO: content loading?

        Pixel = new Texture2D(GraphicsDevice, 1, 1);
        Pixel.SetData(new[] {Color.White});
    }

    protected override void Update(GameTime gameTime)
    {
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
        _spriteBatch.Draw(_canvas, _screenCenter, null, Color.White, 0f, _screenCenter / 2f, _canvasRenderScale, SpriteEffects.None, 1f);
        _spriteBatch.End();
        base.Draw(gameTime);
    }

    public void SetResolution(int width, int height)
    {
        _graphics.PreferredBackBufferWidth = width;
        _graphics.PreferredBackBufferHeight = height;
        _graphics.ApplyChanges();

        _screenCenter = new Vector2(width / 2f, height / 2f);
        _canvasRenderScale = Math.Min(width / (float)_canvasSize.X, height / (float)_canvasSize.Y);
    }
}
