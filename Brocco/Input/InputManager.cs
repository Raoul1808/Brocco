using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Brocco.Input;

/// <summary>
/// A wrapper around XNA's Input State system.
/// </summary>
public static class InputManager
{
    // ReSharper disable once InconsistentNaming
    public const int GAMEPADS = 4;
    
    private static KeyboardState _oldKeyboard, _keyboard;
    private static GamePadState[] _oldGamepads = new GamePadState[GAMEPADS], _gamepads = new GamePadState[GAMEPADS];

    internal static void Update()
    {
        _oldKeyboard = _keyboard;
        _gamepads.CopyTo(_oldGamepads, 0); 

        _keyboard = Keyboard.GetState();
        for (int i = 0; i < GAMEPADS; i++)
        {
            _gamepads[i] = GamePad.GetState((PlayerIndex)i);
        }
    }

    /// <summary>
    /// Get if the specified key was just pressed this frame.
    /// </summary>
    /// <param name="key">The key to check</param>
    /// <returns>true if the key was just pressed; false otherwise</returns>
    public static bool GetKeyPress(Keys key) => _keyboard.IsKeyDown(key) && _oldKeyboard.IsKeyUp(key);
    
    /// <summary>
    /// Get if the specified key is held down.
    /// </summary>
    /// <param name="key">The key to check</param>
    /// <returns>true if the key is held down; false otherwise</returns>
    public static bool GetKeyDown(Keys key) => _keyboard.IsKeyDown(key);
    
    /// <summary>
    /// Get if the specified key was just released this frame.
    /// </summary>
    /// <param name="key">The key to check</param>
    /// <returns>true if the key was just released; false otherwise</returns>
    public static bool GetKeyRelease(Keys key) => _keyboard.IsKeyUp(key) && _oldKeyboard.IsKeyDown(key);

    /// <summary>
    /// Get if the specified gamepad button was just pressed this frame on any gamepad.
    /// </summary>
    /// <param name="button">The button to check</param>
    /// <returns>true if the button was just pressed; false otherwise</returns>
    public static bool GetButtonPress(Buttons button)
    {
        for (int i = 0; i < GAMEPADS; i++)
        {
            bool pressed = GetButtonPress(button, i);
            if (pressed) return true;
        }

        return false;
    }

    /// <summary>
    /// Get if the specified gamepad button was just pressed this frame on the specified gamepad.
    /// </summary>
    /// <param name="button">The button to check</param>
    /// <param name="index">The gamepad index to check</param>
    /// <returns>true if the button was just pressed; false otherwise</returns>
    public static bool GetButtonPress(Buttons button, int index) => _gamepads[index].IsButtonDown(button) && _oldGamepads[index].IsButtonUp(button);

    /// <summary>
    /// Get if the specified gamepad button was just pressed this frame on the specified gamepad.
    /// </summary>
    /// <param name="button">The button to check</param>
    /// <param name="index">The gamepad index to check</param>
    /// <returns>true if the button was just pressed; false otherwise</returns>
    public static bool GetButtonPress(Buttons button, PlayerIndex index) => GetButtonPress(button, (int)index);

    /// <summary>
    /// Get if the specified gamepad button is held down this frame on any gamepad.
    /// </summary>
    /// <param name="button">The button to check</param>
    /// <returns>true if the button is currently down; false otherwise</returns>
    public static bool GetButtonDown(Buttons button)
    {
        for (int i = 0; i < GAMEPADS; i++)
        {
            bool down = GetButtonDown(button, i);
            if (down) return true;
        }

        return false;
    }

    /// <summary>
    /// Get if the specified gamepad button is held down this frame on the specified gamepad.
    /// </summary>
    /// <param name="button">The button to check</param>
    /// <param name="index">The gamepad index to check</param>
    /// <returns>true if the button is currently down; false otherwise</returns>
    public static bool GetButtonDown(Buttons button, int index) => _gamepads[index].IsButtonDown(button);

    /// <summary>
    /// Get if the specified gamepad button is held down this frame on the specified gamepad.
    /// </summary>
    /// <param name="button">The button to check</param>
    /// <param name="index">The gamepad index to check</param>
    /// <returns>true if the button is currently down; false otherwise</returns>
    public static bool GetButtonDown(Buttons button, PlayerIndex index) => GetButtonDown(button, (int)index);

    /// <summary>
    /// Get if the specified gamepad button was just released this frame on any gamepad.
    /// </summary>
    /// <param name="button">The button to check</param>
    /// <returns>true if the button was just released; false otherwise</returns>
    public static bool GetButtonRelease(Buttons button)
    {
        for (int i = 0; i < GAMEPADS; i++)
        {
            bool released = GetButtonRelease(button, i);
            if (released) return true;
        }

        return false;
    }

    /// <summary>
    /// Get if the specified gamepad button was just released this frame on the specified gamepad.
    /// </summary>
    /// <param name="button">The button to check</param>
    /// <param name="index">The gamepad index to check</param>
    /// <returns>true if the button was just released; false otherwise</returns>
    public static bool GetButtonRelease(Buttons button, int index) => _gamepads[GAMEPADS].IsButtonUp(button) && _oldGamepads[index].IsButtonDown(button);

    /// <summary>
    /// Get if the specified gamepad button was just released this frame on the specified gamepad.
    /// </summary>
    /// <param name="button">The button to check</param>
    /// <param name="index">The gamepad index to check</param>
    /// <returns>true if the button was just released; false otherwise</returns>
    public static bool GetButtonRelease(Buttons button, PlayerIndex index) => GetButtonRelease(button, (int)index);
}
