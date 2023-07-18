using System.Collections.Generic;
using FontStashSharp;
using Microsoft.Xna.Framework;

namespace Brocco.Menu;

public class MenuBuilder
{
    private List<string> _menuEntries = new();
    private Vector2 _startingPosition;
    private FontSystem _font;
    private float _fontSize;
    
    private MenuBuilder(FontSystem font, Vector2 position, float fontSize)
    {
        _font = font;
        _startingPosition = position;
        _fontSize = fontSize;
    }

    public static MenuBuilder CreateMenu(FontSystem font, Vector2 position, float fontSize = 32f)
    {
        return new MenuBuilder(font, position, fontSize);
    }

    public MenuBuilder AddEntry(string entry)
    {
        _menuEntries.Add(entry);
        return this;
    }

    public MenuObject Build()
    {
        var menu = new MenuObject(_font, _startingPosition, _fontSize);
        foreach (string entry in _menuEntries)
            menu.AddOption(entry);
        return menu;
    }
}
