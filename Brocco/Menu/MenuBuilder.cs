using System.Collections.Generic;
using FontStashSharp;
using Microsoft.Xna.Framework;

namespace Brocco.Menu;

public class MenuBuilder
{
    private List<MenuEntry> _menuEntries = new();
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

    public MenuBuilder AddButton(string name, MenuButton.MenuButtonPress action = null)
    {
        var entry = new MenuButton
        {
            Label = name,
            FontSize = _fontSize,
        };
        if (action is not null)
            entry.OnButtonPress += action;
        _menuEntries.Add(entry);
        return this;
    }

    public MenuBuilder AddToggle(string name, MenuToggle.MenuTogglePress action = null)
    {
        var entry = new MenuToggle
        {
            Label = name,
            FontSize = _fontSize,
        };
        if (action is not null)
            entry.OnTogglePress += action;
        _menuEntries.Add(entry);
        return this;
    }

    public MenuBuilder AddArraySelect<T>(string name, T[] selections, MenuArraySelect<T>.MenuArraySelectChange action = null)
    {
        var entry = new MenuArraySelect<T>()
        {
            Label = name,
            FontSize = _fontSize,
            SelectOptions = selections,
        };
        if (action is not null)
            entry.OnMenuArraySelectChange += action;
        _menuEntries.Add(entry);
        return this;
    }

    public MenuObject Build()
    {
        var menu = new MenuObject(_font, _startingPosition, _fontSize);
        foreach (var entry in _menuEntries)
            menu.AddEntry(entry);
        return menu;
    }
}
