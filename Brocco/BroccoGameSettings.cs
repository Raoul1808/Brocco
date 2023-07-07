using System.Drawing;

namespace Brocco;

/// <summary>
/// A Brocco Game Loop descriptor.
/// </summary>
public struct BroccoGameSettings
{
    /// <summary>
    /// The size of the internal canvas in pixels. Not to be confused with the resolution.
    /// </summary>
    public Size CanvasSize;
    
    /// <summary>
    /// Whether the mouse cursor will be shown by default or not.
    /// </summary>
    public bool ShowMouse;
    
    /// <summary>
    /// The resolution of the game's window. Not to be confused with the canvas size.
    /// </summary>
    public Size Resolution;

    /// <summary>
    /// The name fo the assets directory from which resources will be loaded.
    /// </summary>
    public string AssetsDirectory;

    /// <summary>
    /// Creates a <c>BroccoGameSettings</c> instance with default settings.
    /// </summary>
    public BroccoGameSettings()
    {
        CanvasSize = new Size(640, 360);
        ShowMouse = false;
        Resolution = new Size(1280, 720);
        AssetsDirectory = "Content";
    }
}
