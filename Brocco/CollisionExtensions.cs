using Microsoft.Xna.Framework;

namespace Brocco;

public static class CollisionExtensions
{
    /// <summary>
    /// Determine if this entity is touching the top of the other entity.
    /// </summary>
    /// <param name="entity">This entity</param>
    /// <param name="other">The other entity</param>
    /// <returns>true if the top of the other entity has been touched; false otherwise</returns>
    public static bool TouchedTopOf(this Entity entity, Entity other) => entity.TouchedTopOf(other.BoundingBox);
    
    /// <summary>
    /// Determine if this entity is touching the top of the provided rectangle.
    /// </summary>
    /// <param name="entity">This entity</param>
    /// <param name="r2">The other rectangle</param>
    /// <returns>true if the top of the other rectangle has been touched; false otherwise</returns>
    public static bool TouchedTopOf(this Entity entity, Rectangle r2)
    {
        var r1 = entity.BoundingBox;
        var vel = entity.Velocity;
        return r1.Bottom + vel.Y > r2.Top &&
               r1.Top < r2.Top &&
               r1.Right > r2.Left &&
               r1.Left < r2.Right;
    }

    /// <summary>
    /// Determine if this entity is touching the bottom of the other entity.
    /// </summary>
    /// <param name="entity">This entity</param>
    /// <param name="other">The other entity</param>
    /// <returns>true if the bottom of the other entity has been touched; false otherwise</returns>
    public static bool TouchedBottomOf(this Entity entity, Entity other) => entity.TouchedBottomOf(other.BoundingBox);

    /// <summary>
    /// Determine if this entity is touching the bottom of the provided rectangle.
    /// </summary>
    /// <param name="entity">This entity</param>
    /// <param name="r2">The other rectangle</param>
    /// <returns>true if the bottom of the other rectangle has been touched; false otherwise</returns>
    public static bool TouchedBottomOf(this Entity entity, Rectangle r2)
    {
        var r1 = entity.BoundingBox;
        var vel = entity.Velocity;
        return r1.Top + vel.Y < r2.Bottom &&
               r1.Bottom > r2.Bottom &&
               r1.Right > r2.Left &&
               r1.Left < r2.Right;
    }

    /// <summary>
    /// Determine if this entity is touching the left of the other entity.
    /// </summary>
    /// <param name="entity">This entity</param>
    /// <param name="other">The other entity</param>
    /// <returns>true if the left of the other entity has been touched; false otherwise</returns>
    public static bool TouchedLeftOf(this Entity entity, Entity other) => entity.TouchedLeftOf(other.BoundingBox);
    
    /// <summary>
    /// Determine if this entity is touching the left of the provided rectangle.
    /// </summary>
    /// <param name="entity">This entity</param>
    /// <param name="r2">The other rectangle</param>
    /// <returns>true if the left of the other rectangle has been touched; false otherwise</returns>
    public static bool TouchedLeftOf(this Entity entity, Rectangle r2)
    {
        var r1 = entity.BoundingBox;
        var vel = entity.Velocity;
        return r1.Right + vel.X > r2.Left &&
               r1.Left < r2.Left &&
               r1.Bottom > r2.Top &&
               r1.Top < r2.Bottom;
    }

    /// <summary>
    /// Determine if this entity is touching the right of the other entity.
    /// </summary>
    /// <param name="entity">This entity</param>
    /// <param name="other">The other entity</param>
    /// <returns>true if the right of the other entity has been touched; false otherwise</returns>
    public static bool TouchedRightOf(this Entity entity, Entity other) => entity.TouchedRightOf(other.BoundingBox);

    /// <summary>
    /// Determine if this entity is touching the right of the provided rectangle.
    /// </summary>
    /// <param name="entity">This entity</param>
    /// <param name="r2">The other rectangle</param>
    /// <returns>true if the right of the other rectangle has been touched; false otherwise</returns>
    public static bool TouchedRightOf(this Entity entity, Rectangle r2)
    {
        var r1 = entity.BoundingBox;
        var vel = entity.Velocity;
        return r1.Left + vel.X < r2.Right &&
               r1.Right > r2.Right &&
               r1.Bottom > r2.Top &&
               r1.Top < r2.Bottom;
    }
}
