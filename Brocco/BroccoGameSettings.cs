using System.Drawing;

namespace Brocco;

public struct BroccoGameSettings
{
    public Size CanvasSize;
    public bool ShowMouse;
    public Size Resolution;

    public BroccoGameSettings()
    {
        CanvasSize = new Size(640, 360);
        ShowMouse = false;
        Resolution = new Size(1280, 720);
    }
}
