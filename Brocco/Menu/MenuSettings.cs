using Microsoft.Xna.Framework;

namespace Brocco.Menu;

// TODO: add more customization options
public struct MenuSettings
{
    /// <summary>
    /// The default font size used to render every menu entry.
    /// </summary>
    public float FontSize;

    /// <summary>
    /// The default text color.
    /// </summary>
    public Color TextColor;
    
    /// <summary>
    /// The Menu Select Effect applied to the currently selected menu entry
    /// </summary>
    public MenuSelectEffect SelectEffect;

    /// <summary>
    /// The color of the selected option. Used only with a color SelectEffect.
    /// </summary>
    public Color SelectColor;
    
    public MenuSettings()
    {
        FontSize = 32f;
        TextColor = Color.White;
        SelectEffect = MenuSelectEffect.Color;
        SelectColor = Color.Yellow;
    }
}
