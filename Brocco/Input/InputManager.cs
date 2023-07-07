using Microsoft.Xna.Framework.Input;

namespace Brocco.Input;

public static class InputManager
{
    private static KeyboardState _oldKeyboard, _keyboard;
    
    static InputManager()
    {
        _keyboard = Keyboard.GetState();
        _oldKeyboard = new KeyboardState();
    }

    public static void Update()
    {
        _oldKeyboard = _keyboard;
        _keyboard = Keyboard.GetState();
    }

    public static bool GetKeyPress(Keys key) => _keyboard.IsKeyDown(key) && _oldKeyboard.IsKeyUp(key);
    public static bool GetKeyDown(Keys key) => _keyboard.IsKeyDown(key);
    public static bool GetKeyRelease(Keys key) => _keyboard.IsKeyUp(key) && _oldKeyboard.IsKeyDown(key);
}
