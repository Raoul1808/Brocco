using System;
using System.Linq;
using Brocco.Basic;
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
    private static MouseState _oldMouse, _mouse;
    private static GamePadState[] _oldGamepads = new GamePadState[GAMEPADS], _gamepads = new GamePadState[GAMEPADS];

    private static bool _inTextInput = false;
    private static Action<string> _editStringCallback;
    private static Action _endEditCallback;
    private static string _currentTextInput;

    internal static float CanvasRenderScale;
    internal static Vector2 CanvasOffset;
    internal static bool GameActive = true;

    private static int _leftInputTimer = 0;
    public static bool JustLeftTextInput => _leftInputTimer > 0;

    internal static void Update()
    {
        if (_leftInputTimer > 0)
            _leftInputTimer--;
        _oldKeyboard = _keyboard;
        _oldMouse = _mouse;
        _gamepads.CopyTo(_oldGamepads, 0);

        _keyboard = Keyboard.GetState();
        _mouse = Mouse.GetState();
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
    public static bool GetKeyPress(Keys key) => !_inTextInput && _keyboard.IsKeyDown(key) && _oldKeyboard.IsKeyUp(key);
    
    /// <summary>
    /// Get if the specified key is held down.
    /// </summary>
    /// <param name="key">The key to check</param>
    /// <returns>true if the key is held down; false otherwise</returns>
    public static bool GetKeyDown(Keys key) => !_inTextInput && _keyboard.IsKeyDown(key);
    
    /// <summary>
    /// Get if the specified key was just released this frame.
    /// </summary>
    /// <param name="key">The key to check</param>
    /// <returns>true if the key was just released; false otherwise</returns>
    public static bool GetKeyRelease(Keys key) => !_inTextInput && _keyboard.IsKeyUp(key) && _oldKeyboard.IsKeyDown(key);

    /// <summary>
    /// Get an array of all new key presses this frame.
    /// </summary>
    /// <returns>An array containing the newly pressed keys</returns>
    public static Keys[] GetNewKeyPresses() => _inTextInput ? Array.Empty<Keys>() : _keyboard.GetPressedKeys().Except(_oldKeyboard.GetPressedKeys()).ToArray();

    public static void StartTextInput(string currentString, Action<string> editStringCallback, Action endCallback)
    {
        if (JustLeftTextInput) return;
        TextInputEXT.TextInput += DoTextInput;
        TextInputEXT.StartTextInput();
        _inTextInput = true;
        _currentTextInput = currentString;
        _editStringCallback = editStringCallback;
        _endEditCallback = endCallback;
    }

    private static void StopTextInput()
    {
        _leftInputTimer = 2;
        TextInputEXT.StopTextInput();
        TextInputEXT.TextInput -= DoTextInput;
        _inTextInput = false;
        _endEditCallback?.Invoke();
        _editStringCallback = null;
        _endEditCallback = null;
    }

    private static void DoTextInput(char c)
    {
        if (_keyboard.IsKeyDown(Keys.Escape) || c == 13)
        {
            StopTextInput();
            return;
        }
        if (char.IsLetterOrDigit(c) || c == ' ' || c == '.' || c == '-' || c == '_')
            _currentTextInput += c;
        if (c == 8 && _currentTextInput.Length > 0)
            _currentTextInput = _currentTextInput.Remove(_currentTextInput.Length - 1);
        _editStringCallback?.Invoke(_currentTextInput);
    }

    /// <summary>
    /// Get if the specified gamepad button was just pressed this frame on any gamepad.
    /// </summary>
    /// <param name="button">The button to check</param>
    /// <returns>true if the button was just pressed; false otherwise</returns>
    public static bool GetButtonPress(Buttons button)
    {
        if (_inTextInput) return false;
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
    public static bool GetButtonPress(Buttons button, int index) => !_inTextInput && _gamepads[index].IsButtonDown(button) && _oldGamepads[index].IsButtonUp(button);

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
        if (_inTextInput) return false;
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
    public static bool GetButtonDown(Buttons button, int index) => !_inTextInput && _gamepads[index].IsButtonDown(button);

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
        if (_inTextInput) return false;
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

    /// <summary>
    /// Get if the specified mouse button was just pressed this frame.
    /// </summary>
    /// <param name="button">The mouse button to check</param>
    /// <returns>true if the button was just pressed; false otherwise</returns>
    public static bool GetClickPress(MouseButtons button) => GameActive && !_inTextInput && _mouse.IsButtonDown(button) && _oldMouse.IsButtonUp(button);

    /// <summary>
    /// Get if the specified mouse button is held down this frame.
    /// </summary>
    /// <param name="button">The mouse button to check</param>
    /// <returns>true if the button is currently down; false otherwise</returns>
    public static bool GetClickDown(MouseButtons button) => GameActive && !_inTextInput && _mouse.IsButtonDown(button);

    /// <summary>
    /// Get if the specified mouse button was just released this frame.
    /// </summary>
    /// <param name="button">The mouse button to check</param>
    /// <returns>true if the button was just released; false otherwise</returns>
    public static bool GetClickRelease(MouseButtons button) => GameActive && !_inTextInput && _mouse.IsButtonUp(button) && _oldMouse.IsButtonDown(button);

    /// <summary>
    /// Get the current mouse position on the canvas.
    /// </summary>
    /// <returns>The relative mouse position on the canvas.</returns>
    public static Vector2 GetCanvasMousePosition() => (new Vector2(_mouse.X, _mouse.Y) - CanvasOffset) / CanvasRenderScale;

    /// <summary>
    /// Get the current mouse on the window relative to the top-right corner.
    /// </summary>
    /// <returns>The absolute mouse position on the window.</returns>
    public static Point GetMousePosition() => new Point(_mouse.X, _mouse.Y);
}
