using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Brocco;

/// <summary>
/// A class that allows adding a system to the game that will continuously update in the background.
/// </summary>
public abstract class BroccoAutoSystem
{
    /// <summary>
    /// Called when the game is initialized.
    /// </summary>
    public virtual void Initialize(BroccoGame game)
    {
    }

    /// <summary>
    /// Called before the game updates.
    /// </summary>
    public virtual void PreUpdate(GameTime gameTime)
    {
    }

    /// <summary>
    /// Called after the game updates.
    /// </summary>
    public virtual void PostUpdate(GameTime gameTime)
    {
        
    }

    /// <summary>
    /// Called when the game finishes rendering.
    /// </summary>
    public virtual void Render(SpriteBatch spriteBatch, GameTime gameTime)
    {
    }
}
