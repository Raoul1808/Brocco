using System.Collections.Generic;
using Brocco.Input;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StbTrueTypeSharp;

namespace Brocco;

public class Menu
{
    private List<string> _options = new();
    private int _currentOption = 0;
    private Vector2 _position;
    private FontSystem _font;
    private float _fontSize;

    public Menu(FontSystem font, Vector2 position, float fontSize)
    {
        _font = font;
        _fontSize = fontSize;
        _position = position;
    }

    public void AddOption(string option)
    {
        _options.Add(option);
    }

    public void Update()
    {
        if (InputManager.GetKeyPress(Keys.Up))
        {
            _currentOption--;
            if (_currentOption < 0)
                _currentOption = _options.Count - 1;
        }

        if (InputManager.GetKeyPress(Keys.Down))
        {
            _currentOption++;
            if (_currentOption >= _options.Count)
                _currentOption = 0;
        }
    }

    private Vector2 StringLength(string text)
    {
        Bounds b = new Bounds();
        b = _font.GetFont(_fontSize).TextBounds(text, Vector2.Zero);
        return new Vector2(b.X2, _fontSize);
    }

    public void Render(SpriteBatch spriteBatch)
    {
        var font = _font.GetFont(_fontSize);
        float gap = font.FontSize * 1.125f;
        for (int i = 0; i < _options.Count; i++)
        {
            bool isCurrent = i == _currentOption;
            string option = _options[i];
            Vector2 pos = new Vector2(_position.X, _position.Y + i * gap);
            Vector2 offset = StringLength(option) * 0.5f;
            spriteBatch.DrawString(font, option, pos, isCurrent ? Color.Yellow : Color.White, origin: offset);
        }
    }
}
