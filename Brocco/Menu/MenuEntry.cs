using Brocco.Input;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Brocco.Menu;

public abstract class MenuEntry
{
    public string Label { get; set; }
    public float FontSize { get; set; }

    public abstract void Update();
    public abstract void Render(SpriteBatch spriteBatch, FontSystem font, Vector2 position, Color color, Vector2 origin);
}

public sealed class MenuButton : MenuEntry
{
    public override void Update()
    {
        if (InputManager.GetKeyPress(Keys.Enter))
            OnButtonPress?.Invoke(this);
    }

    public override void Render(SpriteBatch spriteBatch, FontSystem font, Vector2 position, Color color, Vector2 origin)
    {
        spriteBatch.DrawString(font.GetFont(FontSize), Label, position, color, origin: origin);
    }

    public delegate void MenuButtonPress(MenuButton sender);

    public event MenuButtonPress OnButtonPress;
}

public sealed class MenuToggle : MenuEntry
{
    public bool IsChecked { get; set; }

    public override void Update()
    {
        if (InputManager.GetKeyPress(Keys.Enter))
        {
            IsChecked = !IsChecked;
            OnTogglePress?.Invoke(this, IsChecked);
        }
    }

    public override void Render(SpriteBatch spriteBatch, FontSystem font, Vector2 position, Color color, Vector2 origin)
    {
        spriteBatch.DrawString(font.GetFont(FontSize), Label + ": " + IsChecked, position, color, origin: origin);
    }

    public delegate void MenuTogglePress(MenuToggle sender, bool newState);

    public event MenuTogglePress OnTogglePress;
}

public sealed class MenuArraySelect<T> : MenuEntry
{
    public T[] SelectOptions { get; set; }
    public int CurrentOption { get; private set; }

    private float _longestStringLength = 0f;

    public override void Update()
    {
        if (InputManager.GetKeyPress(Keys.Left))
        {
            CurrentOption--;
            if (CurrentOption < 0)
                CurrentOption = SelectOptions.Length - 1;
            OnMenuArraySelectChange?.Invoke(this, SelectOptions[CurrentOption]);
        }

        if (InputManager.GetKeyPress(Keys.Right))
        {
            CurrentOption++;
            if (CurrentOption >= SelectOptions.Length)
                CurrentOption = 0;
            OnMenuArraySelectChange?.Invoke(this, SelectOptions[CurrentOption]);
        }
    }

    public override void Render(SpriteBatch spriteBatch, FontSystem font, Vector2 position, Color color, Vector2 origin)
    {
        spriteBatch.DrawString(font.GetFont(FontSize), Label + "  < " + SelectOptions[CurrentOption] + " >", position, color, origin: origin);
    }

    public delegate void MenuArraySelectChange(MenuArraySelect<T> sender, T selectedOption);

    public event MenuArraySelectChange OnMenuArraySelectChange;
}
