using Microsoft.Xna.Framework.Input;

namespace Brocco.Input;

public static class InputExtensions
{
    public static bool IsButtonDown(this MouseState mouse, MouseButtons button)
    {
        return button switch
        {
            MouseButtons.Left => mouse.LeftButton == ButtonState.Pressed,
            MouseButtons.Right => mouse.RightButton == ButtonState.Pressed,
            MouseButtons.Middle => mouse.MiddleButton == ButtonState.Pressed,
            _ => false,
        };
    }

    public static bool IsButtonUp(this MouseState mouse, MouseButtons button)
    {
        return button switch
        {
            MouseButtons.Left => mouse.LeftButton == ButtonState.Released,
            MouseButtons.Right => mouse.RightButton == ButtonState.Released,
            MouseButtons.Middle => mouse.MiddleButton == ButtonState.Released,
            _ => false,
        };
    }
}
