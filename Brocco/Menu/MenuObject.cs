using System.Collections.Generic;
using Brocco.Input;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Brocco.Menu;

/// <summary>
/// A menu system with embedded menu logic.
/// </summary>
public class MenuObject
{
    private List<MenuEntry> _entries = new();
    private int _currentOption = 0;
    private Vector2 _position;
    private FontSystem _font;
    private float _fontSize;

    /// <summary>
    /// Creates a new menu instance.
    /// </summary>
    /// <param name="font">The font used by the menu</param>
    /// <param name="position">The starting position of the menu</param>
    /// <param name="fontSize">The font size used for text rendering</param>
    public MenuObject(FontSystem font, Vector2 position, float fontSize)
    {
        _font = font;
        _fontSize = fontSize;
        _position = position;
    }

    /// <summary>
    /// Adds a menu option.
    /// </summary>
    /// <param name="option">The option to add</param>
    public void AddEntry(MenuEntry entry)
    {
        _entries.Add(entry);
    }

    /// <summary>
    /// This method is called every frame.
    /// </summary>
    public void Update()
    {
        if (InputManager.GetKeyPress(Keys.Up))
        {
            _currentOption--;
            if (_currentOption < 0)
                _currentOption = _entries.Count - 1;
        }

        if (InputManager.GetKeyPress(Keys.Down))
        {
            _currentOption++;
            if (_currentOption >= _entries.Count)
                _currentOption = 0;
        }
        
        _entries[_currentOption].Update();
    }

    private Vector2 StringLength(string text)
    {
        Bounds b = _font.GetFont(_fontSize).TextBounds(text, Vector2.Zero);
        return new Vector2(b.X2, _fontSize);
    }

    /// <summary>
    /// This method is called every frame. Renders the menu and its text.
    /// </summary>
    /// <param name="spriteBatch">The <c>SpriteBatch</c> instance used in the Brocco Game Loop</param>
    public void Render(SpriteBatch spriteBatch)
    {
        var font = _font.GetFont(_fontSize);
        float gap = _fontSize * 1.125f;
        for (int i = 0; i < _entries.Count; i++)
        {
            bool isCurrent = i == _currentOption;
            var entry = _entries[i];
            Vector2 pos = new Vector2(_position.X, _position.Y + i * gap);
            Vector2 length = StringLength(entry.Label);
            entry.Render(spriteBatch, _font, pos, isCurrent ? Color.Yellow : Color.White, origin: length / 2);
        }
    }
}
