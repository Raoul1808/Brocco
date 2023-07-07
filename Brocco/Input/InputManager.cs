using Microsoft.Xna.Framework.Input;

namespace Brocco.Input;

/// <summary>
/// A wrapper around XNA's State system.
/// </summary>
public static class InputManager
{
    private static KeyboardState _oldKeyboard, _keyboard;
    
    static InputManager()
    {
        _keyboard = Keyboard.GetState();
        _oldKeyboard = new KeyboardState();
    }

    internal static void Update()
    {
        _oldKeyboard = _keyboard;
        _keyboard = Keyboard.GetState();
    }

    /// <summary>
    /// Get if the specified key was just pressed this frame.
    /// </summary>
    /// <param name="key">The key to check</param>
    /// <returns>true if the key was just pressed; false otherwise</returns>
    public static bool GetKeyPress(Keys key) => _keyboard.IsKeyDown(key) && _oldKeyboard.IsKeyUp(key);
    
    /// <summary>
    /// Get if the key is held down.
    /// </summary>
    /// <param name="key">The key to check</param>
    /// <returns>true if the key is held down; false otherwise</returns>
    public static bool GetKeyDown(Keys key) => _keyboard.IsKeyDown(key);
    
    /// <summary>
    /// Get if the key was just released this frame.
    /// </summary>
    /// <param name="key">The key to check</param>
    /// <returns>true if the key was just release; false otherwise</returns>
    public static bool GetKeyRelease(Keys key) => _keyboard.IsKeyUp(key) && _oldKeyboard.IsKeyDown(key);
}
