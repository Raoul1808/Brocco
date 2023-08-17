using System;
using System.Collections.Generic;
using Brocco.Basic;
using Brocco.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
    private Size _screenSize;
    private Vector2 _screenCenter;
    private float _canvasRenderScale;
    private readonly Vector2 _canvasDrawOffset;
    private readonly Color _clearColor;
    private Vector2 _canvasOffset;

    private List<BroccoAutoSystem> _systems;

    private bool _isRunning = false;

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
        _screenSize = settings.Resolution ?? settings.CanvasSize * 2;
        _clearColor = settings.ClearColor;
        Window.AllowUserResizing = settings.CanResize;
        Window.ClientSizeChanged += (sender, args) =>
        {
            SetResolution(Window.ClientBounds.Width, Window.ClientBounds.Height);
        };
        _systems = new List<BroccoAutoSystem>();
    }

    protected override void OnActivated(object sender, EventArgs args)
    {
        InputManager.GameActive = true;
        base.OnActivated(sender, args);
    }

    protected override void OnDeactivated(object sender, EventArgs args)
    {
        InputManager.GameActive = false;
        base.OnDeactivated(sender, args);
    }

    protected override void Initialize()
    {
        foreach (var system in _systems)
            system.PreInitialize(this);
        base.Initialize();
        _isRunning = true;
        foreach (var system in _systems)
            system.PostInitialize(this);

        SetResolution(_screenSize.Width, _screenSize.Height);
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
        foreach (var system in _systems)
            system.PreUpdate(gameTime);
        if (SceneManager.ReceivedStop())
            Exit();
        InputManager.Update();
        SceneManager.Update((float) gameTime.ElapsedGameTime.TotalSeconds);
        base.Update(gameTime);
        foreach (var system in _systems)
            system.PostUpdate(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.SetRenderTarget(_canvas);
        GraphicsDevice.Clear(SceneManager.GetClearColor() ?? _clearColor);
        _spriteBatch.Begin(SpriteSortMode.Deferred, SceneManager.GetCanvasBlendState() ?? BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
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
        foreach (var system in _systems)
            system.Render(_spriteBatch, gameTime);
        base.Draw(gameTime);
    }

    /// <summary>
    /// Changes the game's viewport/resolution.
    /// </summary>
    /// <param name="width">Width in pixels</param>
    /// <param name="height">Height in pixels</param>
    public void SetResolution(int width, int height)
    {
        var oldEvent = new GameResizeEvent
        {
            WindowSize = _screenSize,
            CanvasOffset = _canvasOffset,
            CanvasRenderScale = _canvasRenderScale,
        };
        
        _graphics.PreferredBackBufferWidth = width;
        _graphics.PreferredBackBufferHeight = height;
        _graphics.ApplyChanges();

        _screenCenter = new Vector2(width / 2f, height / 2f);
        _canvasRenderScale = Math.Min(width / (float)_canvasSize.Width, height / (float)_canvasSize.Height);
        InputManager.CanvasRenderScale = _canvasRenderScale;

        var csz = _canvasRenderScale * _canvasSize.ToVector2();
        _canvasOffset = new Vector2(_screenCenter.X - csz.X / 2f, _screenCenter.Y - csz.Y / 2f);
        InputManager.CanvasOffset = _canvasOffset;

        var newEvent = new GameResizeEvent
        {
            WindowSize = _screenSize,
            CanvasOffset = _canvasOffset,
            CanvasRenderScale = _canvasRenderScale,
        };
        
        foreach (var system in _systems)
        {
            system.OnGameResize(oldEvent, newEvent);
        }
    }

    /// <summary>
    /// Adds a new Brocco Auto System to the Brocco Game Loop.
    /// </summary>
    /// <typeparam name="T">The type of the system to add</typeparam>
    /// <exception cref="Exception">Thrown if the game is currently running</exception>
    public void AddSystem<T>() where T : BroccoAutoSystem
    {
        if (_isRunning)
            throw new Exception("Cannot add a new system after the game started.");
        if (_systems.FindIndex(system => system.GetType() == typeof(T)) != -1)
            return;
        var system = (BroccoAutoSystem)Activator.CreateInstance<T>();
        _systems.Add(system);
    }
}
