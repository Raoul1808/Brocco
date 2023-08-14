using System.Collections.Generic;
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
            _currentSceneRef = scene;
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

    internal static Effect GetCanvasEffect() => _currentSceneRef?.CanvasEffect ?? null;
    
    internal static Effect GetScreenEffect() => _currentSceneRef?.ScreenEffect ?? null;

    internal static bool ReceivedStop() => _currentSceneRef?.StoppedGame ?? false;
}
