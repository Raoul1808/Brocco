using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Brocco;

/// <summary>
/// A scene where entities are created and rendered every frame.
/// </summary>
public abstract class Scene
{
    private List<Entity> _entities = new();

    internal bool StoppedGame { get; private set; }

    /// <summary>
    /// Used internally, determines if the scene was loaded before or not.
    /// </summary>
    public bool Loaded { get; internal set; }
    
    /// <summary>
    /// Determines whether the game should run internal updates or not.
    /// </summary>
    public bool PauseUpdate { get; set; }

    /// <summary>
    /// The current canvas shader effect. Set to null to remove the shader.
    /// </summary>
    public Effect CanvasEffect { get; set; } = null;

    /// <summary>
    /// The current screen shader effect. Set to null to remove the shader.
    /// </summary>
    public Effect ScreenEffect { get; set; } = null;

    /// <summary>
    /// The scene's clear color. If this is set to null, the game's default clear color is used instead.
    /// </summary>
    public Color? ClearColor { get; set; } = null;

    /// <summary>
    /// Override this if you want to load certain assets for certain entities.
    /// </summary>
    public virtual void Load() {}

    /// <summary>
    /// This is called when the scene becomes active.
    /// </summary>
    public virtual void OnBecomeActive()
    {
    }

    /// <summary>
    /// This is called when the scene becomes inactive.
    /// </summary>
    public virtual void OnBecomeInactive()
    {
    }

    internal void InternalUpdate(float dt)
    {
        if (PauseUpdate)
        {
            Update(dt);
            return;
        }
        
        for (int i = 0; i < _entities.Count;)
        {
            var entity = _entities[i];
            if (entity.CanDispose)
            {
                RemoveFromScene(entity);
                continue;
            }

            entity.Update(dt);
            entity.Position += entity.Velocity;
            i++;
        }

        Update(dt);
    }

    /// <summary>
    /// This method is called every frame. Override this method to add custom scene update logic.
    /// </summary>
    public virtual void Update(float dt)
    {
    }

    /// <summary>
    /// This method renders to the canvas every registered entity and is called every frame. Override this to change the rendering code.
    /// </summary>
    /// <param name="spriteBatch">The <c>SpriteBatch</c> instance used in the Brocco Game Loop</param>
    public virtual void CanvasRender(SpriteBatch spriteBatch)
    {
        foreach (Entity entity in _entities)
            entity.Render(spriteBatch);
    }

    /// <summary>
    /// This method is called when the canvas is rendered to the screen. Override this to add things to render.
    /// </summary>
    /// <param name="spriteBatch">The <c>SpriteBatch</c> instance used in the Brocco Game Loop</param>
    public virtual void ScreenRender(SpriteBatch spriteBatch)
    {
    }

    /// <summary>
    /// Adds an entity to the scene's loop.
    /// </summary>
    /// <typeparam name="T">The entity to add</typeparam>
    /// <returns>The entity added</returns>
    public T AddToScene<T>() where T : Entity
    {
        var entity = (T)Activator.CreateInstance(typeof(T));
        return (T) AddToScene(entity);
    }

    /// <summary>
    /// Adds an entity to the scene's loop.
    /// </summary>
    /// <param name="entity">The entity to add</param>
    /// <returns>The entity added</returns>
    public Entity AddToScene(Entity entity)
    {
        entity.Scene = this;
        _entities.Add(entity);
        return entity;
    }

    /// <summary>
    /// Removes an entity from the scene's loop.
    /// </summary>
    /// <param name="entity">The entity to remove</param>
    public void RemoveFromScene(Entity entity)
    {
        _entities.Remove(entity);
    }

    /// <summary>
    /// Removes an entity from the scene's loop using its ID.
    /// </summary>
    /// <param name="id">The ID of the entity to remove</param>
    public void RemoveIdFromScene(int id)
    {
        _entities.Remove(_entities.Find(e => e.Id == id));
    }

    /// <summary>
    /// Clears all entities from the scene's loop.
    /// </summary>
    protected void ClearEntities()
    {
        _entities.Clear();
    }

    /// <summary>
    /// Call this to stop the game loop.
    /// </summary>
    public void ExitGame()
    {
        StoppedGame = true;
    }
}
