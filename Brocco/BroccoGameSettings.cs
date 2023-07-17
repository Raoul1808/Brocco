using Microsoft.Xna.Framework;
using Size = System.Drawing.Size;

namespace Brocco;

/// <summary>
/// A Brocco Game Loop descriptor.
/// </summary>
public struct BroccoGameSettings
{
    /// <summary>
    /// The size of the internal canvas in pixels. Not to be confused with the resolution. Defaults to 640x360.
    /// </summary>
    public Size CanvasSize;
    
    /// <summary>
    /// Whether the mouse cursor will be shown by default or not. Defaults to false.
    /// </summary>
    public bool ShowMouse;
    
    /// <summary>
    /// The resolution of the game's window. Not to be confused with the canvas size. Defaults to 2 times the canvas size.
    /// </summary>
    public Size Resolution;

    /// <summary>
    /// The name of the assets directory from which resources will be loaded. Defaults to "Content"
    /// </summary>
    public string AssetsDirectory;

    /// <summary>
    /// The canvas' clear color. Defaults to Black.
    /// </summary>
    public Color ClearColor;

    /// <summary>
    /// Creates a <c>BroccoGameSettings</c> instance with default settings.
    /// </summary>
    public BroccoGameSettings()
    {
        CanvasSize = new Size(640, 360);
        ShowMouse = false;
        Resolution = CanvasSize * 2;
        AssetsDirectory = "Content";
        ClearColor = Color.Black;
    }
}
