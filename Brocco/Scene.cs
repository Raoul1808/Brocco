using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Brocco;

/// <summary>
/// A scene where entities are created and rendered every frame.
/// </summary>
public abstract class Scene
{
    private List<Entity> _entities = new();

    /// <summary>
    /// Used internally, determines if the scene was loaded before or not.
    /// </summary>
    public bool Loaded { get; internal set; }
    
    /// <summary>
    /// Determines whether the game should run internal updates or not.
    /// </summary>
    public bool PauseUpdate { get; set; }

    /// <summary>
    /// Override this if you want to load certain assets for certain entities.
    /// </summary>
    public virtual void Load() {}

    internal void InternalUpdate(float dt)
    {
        if (PauseUpdate)
        {
            Update(dt);
            return;
        }
        
        Entity firstEntity = null;
        for (int i = 0; i < _entities.Count;)
        {
            var entity = _entities[i];
            if (entity.CanDispose)
            {
                RemoveFromScene(entity);
                continue;
            }

            entity.Update(dt);
            i++;
            if (firstEntity == null)
            {
                firstEntity = entity;
                continue;
            }

            if (firstEntity.BoundingBox.Intersects(entity.BoundingBox))
            {
                firstEntity.CollidedWith(entity);
                entity.CollidedWith(firstEntity);
            }
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
}
