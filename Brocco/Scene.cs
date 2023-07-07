using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Brocco;

public abstract class Scene
{
    private List<Entity> _entities = new();

    public bool Loaded { get; internal set; }

    public virtual void Load() {}
    
    public virtual void Update() {}

    public virtual void Render(SpriteBatch spriteBatch)
    {
        foreach (Entity entity in _entities)
            entity.Render(spriteBatch);
    }

    protected Entity AddToScene(Entity entity)
    {
        _entities.Add(entity);
        return entity;
    }

    protected void RemoveFromScene(Entity entity)
    {
        _entities.Remove(entity);
    }

    protected void RemoveIdFromScene(int id)
    {
        _entities.Remove(_entities.Find(e => e.Id == id));
    }

    protected void ClearEntities()
    {
        _entities.Clear();
    }
}
