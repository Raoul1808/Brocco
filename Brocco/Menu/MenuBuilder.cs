using System.Collections.Generic;
using FontStashSharp;
using Microsoft.Xna.Framework;

namespace Brocco.Menu;

/// <summary>
/// A helper class designed to make menu creation easier.
/// </summary>
public class MenuBuilder
{
    private List<MenuEntry> _menuEntries = new();
    private Vector2 _startingPosition;
    private FontSystem _font;
    private MenuSettings _menuSettings;
    private float _maxLength;

    private MenuBuilder(FontSystem font, Vector2 position, MenuSettings settings)
    {
        _font = font;
        _startingPosition = position;
        _menuSettings = settings;
    }

    /// <summary>
    /// Creates a menu builder instance.
    /// </summary>
    /// <param name="font">The font to use for the menu</param>
    /// <param name="position">The starting position of the menu</param>
    /// <param name="settings">The menu's settings</param>
    /// <returns>A new <c>MenuBuilder</c> instance</returns>
    public static MenuBuilder CreateMenu(FontSystem font, Vector2 position, MenuSettings? settings = null)
    {
        return new MenuBuilder(font, position, settings ?? new ());
    }

    /// <summary>
    /// Adds a menu button to the menu builder.
    /// </summary>
    /// <param name="name">The label attached to the button</param>
    /// <param name="action">The method that will run when this button is pressed</param>
    /// <returns>The current <c>MenuBuilder</c> instance</returns>
    public MenuBuilder AddButton(string name, MenuButton.MenuButtonPress action = null)
    {
        var entry = new MenuButton
        {
            Label = name,
            FontSize = _menuSettings.FontSize,
        };
        if (action is not null)
            entry.OnButtonPress += action;
        _menuEntries.Add(entry);
        return this;
    }

    /// <summary>
    /// Adds a toggle button to the menu builder.
    /// </summary>
    /// <param name="name">The label attached to the toggle</param>
    /// <param name="action">The method that will run when this toggle changes state</param>
    /// <returns>The current <c>MenuBuilder</c> instance</returns>
    public MenuBuilder AddToggle(string name, MenuToggle.MenuTogglePress action = null)
    {
        var entry = new MenuToggle
        {
            Label = name,
            FontSize = _menuSettings.FontSize,
        };
        if (action is not null)
            entry.OnTogglePress += action;
        _menuEntries.Add(entry);
        return this;
    }

    /// <summary>
    /// Adds a selection button to the menu builder.
    /// </summary>
    /// <param name="name">The label attached to the selection</param>
    /// <param name="selections">The different possible selections as an array</param>
    /// <param name="action">The method that will run when the selection changes</param>
    /// <typeparam name="T">The type of the selection array</typeparam>
    /// <returns>The current <c>MenuBuilder</c> instance</returns>
    public MenuBuilder AddArraySelect<T>(string name, T[] selections, MenuArraySelect<T>.MenuArraySelectChange action = null)
    {
        var entry = new MenuArraySelect<T>()
        {
            Label = name,
            FontSize = _menuSettings.FontSize,
            SelectOptions = selections,
        };
        entry.PrecalculateOffsets(_font);
        if (action is not null)
            entry.OnMenuArraySelectChange += action;
        _menuEntries.Add(entry);
        return this;
    }

    /// <summary>
    /// Creates the final Menu instance, ready to use.
    /// </summary>
    /// <returns>The <c>Menu</c> instance created</returns>
    public MenuObject Build()
    {
        var menu = new MenuObject(_font, _startingPosition);
        foreach (var entry in _menuEntries)
            menu.AddEntry(entry);
        return menu;
    }
}
