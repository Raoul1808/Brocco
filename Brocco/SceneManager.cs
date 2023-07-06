using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Brocco;

public static class SceneManager
{
    private static Dictionary<string, Scene> _scenes = new();
    private static Scene _currentSceneRef = null;

    public static void Add(string name, Scene scene)
    {
        _scenes.TryAdd(name, scene);
        _currentSceneRef ??= scene;
    }

    public static void Remove(string name)
    {
        _scenes.Remove(name);
    }

    public static void Change(string name)
    {
        if (_scenes.TryGetValue(name, out var scene))
            _currentSceneRef = scene;
    }

    public static void Update()
    {
        _currentSceneRef?.Update();
    }

    public static void Render(SpriteBatch spriteBatch)
    {
        _currentSceneRef?.Render(spriteBatch);
    }
}
