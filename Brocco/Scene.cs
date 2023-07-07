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
    /// Override this if you want to load certain assets for certain entities.
    /// </summary>
    public virtual void Load() {}

    /// <summary>
    /// This method is called every frame.
    /// </summary>
    public virtual void Update()
    {
        foreach (Entity e in _entities)
            e.Update();
    }

    /// <summary>
    /// This method renders every registered entity and is called every frame. Override this to change the rendering code.
    /// </summary>
    /// <param name="spriteBatch">The <c>SpriteBatch</c> instance used in the Brocco Game Loop</param>
    public virtual void Render(SpriteBatch spriteBatch)
    {
        foreach (Entity entity in _entities)
            entity.Render(spriteBatch);
    }

    /// <summary>
    /// Adds an entity to the scene's loop.
    /// </summary>
    /// <param name="entity">The entity to add</param>
    /// <returns>The entity added</returns>
    protected Entity AddToScene(Entity entity)
    {
        _entities.Add(entity);
        return entity;
    }

    /// <summary>
    /// Removes an entity from the scene's loop.
    /// </summary>
    /// <param name="entity">The entity to remove</param>
    protected void RemoveFromScene(Entity entity)
    {
        _entities.Remove(entity);
    }

    /// <summary>
    /// Removes an entity from the scene's loop using its ID.
    /// </summary>
    /// <param name="id">The ID of the entity to remove</param>
    protected void RemoveIdFromScene(int id)
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
