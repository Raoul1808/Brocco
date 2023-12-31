using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Brocco;

/// <summary>
/// This class manages the current scene on screen.
/// </summary>
public static class SceneManager
{
    private static Dictionary<string, Scene> _scenes = new();
    private static Scene _currentSceneRef = null;

    /// <summary>
    /// Register a scene to the <c>SceneManager</c>
    /// </summary>
    /// <param name="name">The name of the scene</param>
    /// <param name="scene">The scene instance</param>
    public static void Add(string name, Scene scene)
    {
        _scenes.TryAdd(name, scene);
        _currentSceneRef ??= scene;
    }

    internal static void LoadScenes()
    {
        foreach (var scene in _scenes.Values)
            if (!scene.Loaded)
            {
                scene.Load();
                scene.Loaded = true;
            }
    }

    internal static void ActivateScene()
    {
        _currentSceneRef?.OnBecomeActive();
    }

    /// <summary>
    /// Removes a scene from the <c>SceneManager</c>
    /// </summary>
    /// <param name="name">The name of the scene to remove</param>
    public static void Remove(string name)
    {
        _scenes.Remove(name);
    }

    /// <summary>
    /// Changes the current scene
    /// </summary>
    /// <param name="name">The name of the scene to change to</param>
    public static void Change(string name)
    {
        if (_scenes.TryGetValue(name, out var scene))
        {
            _currentSceneRef?.OnBecomeInactive();
            _currentSceneRef = scene;
            _currentSceneRef?.OnBecomeActive();
        }
    }

    /// <summary>
    /// Reloads a scene. The Scene.Load() method will be called again.
    /// </summary>
    /// <param name="name"></param>
    public static void Reload(string name)
    {
        if (_scenes.TryGetValue(name, out var scene))
        {
            if (_currentSceneRef == scene)
                scene.OnBecomeInactive();
            scene.Load();
            scene.Loaded = true;
            if (_currentSceneRef == scene)
                scene.OnBecomeActive();
        }
    }

    internal static void Update(float dt)
    {
        _currentSceneRef?.InternalUpdate(dt);
    }

    internal static void CanvasRender(SpriteBatch spriteBatch)
    {
        _currentSceneRef?.CanvasRender(spriteBatch);
    }

    internal static void ScreenRender(SpriteBatch spriteBatch)
    {
        _currentSceneRef?.ScreenRender(spriteBatch);
    }

    internal static BlendState GetCanvasBlendState() => _currentSceneRef?.BlendState;

    internal static Effect GetCanvasEffect() => _currentSceneRef?.CanvasEffect;
    
    internal static Effect GetScreenEffect() => _currentSceneRef?.ScreenEffect;

    internal static Color? GetClearColor() => _currentSceneRef?.ClearColor;

    internal static bool ReceivedStop() => _currentSceneRef?.StoppedGame ?? false;
}
