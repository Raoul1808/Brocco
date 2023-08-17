using System;
using Brocco.Input;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Brocco.Menu;

public abstract class MenuEntry
{
    public string Label { get; set; }
    public float FontSize { get; set; }

    public abstract void Update();
    public abstract void Render(SpriteBatch spriteBatch, FontSystem font, Vector2 position, Color color,
        TextStyle textStyle);
}

public sealed class MenuButton : MenuEntry
{
    public override void Update()
    {
        if (InputManager.GetKeyPress(Keys.Enter))
            OnButtonPress?.Invoke(this);
    }

    public override void Render(SpriteBatch spriteBatch, FontSystem font, Vector2 position, Color color, TextStyle textStyle)
    {
        var f = font.GetFont(FontSize);
        var length = f.MeasureString(Label);
        length.Y = FontSize;
        spriteBatch.DrawString(font.GetFont(FontSize), Label, position, color, origin: length * 0.5f, textStyle: textStyle);
    }

    public delegate void MenuButtonPress(MenuButton sender);

    public event MenuButtonPress OnButtonPress;
}

public sealed class MenuToggle : MenuEntry
{
    public bool IsChecked { get; set; }

    public override void Update()
    {
        if (InputManager.GetKeyPress(Keys.Enter) || InputManager.GetKeyPress(Keys.Space) || InputManager.GetButtonPress(Buttons.A))
        {
            IsChecked = !IsChecked;
            OnTogglePress?.Invoke(this, IsChecked);
        }
    }

    public override void Render(SpriteBatch spriteBatch, FontSystem font, Vector2 position, Color color, TextStyle textStyle)
    {
        string text = Label + ": " + IsChecked;
        var f = font.GetFont(FontSize);
        var length = f.MeasureString(text);
        length.Y = FontSize;
        spriteBatch.DrawString(font.GetFont(FontSize), text, position, color, origin: length * 0.5f, textStyle: textStyle);
    }

    public delegate void MenuTogglePress(MenuToggle sender, bool newState);

    public event MenuTogglePress OnTogglePress;
}

public sealed class MenuArraySelect<T> : MenuEntry
{
    public T[] SelectOptions { get; init; }
    public int CurrentOption { get; private set; }

    private float[] _precalculatedOptionSizes;

    private Vector2 _startingOffset;
    private Vector2 _arrowLeftOffset;
    private Vector2 _optionOffset;
    private Vector2 _arrowRightOffset;

    public void PrecalculateOffsets(FontSystem font)
    {
        var f = font.GetFont(FontSize);
        string longestOption = "";
        float longestStringLength = 0f;
        _precalculatedOptionSizes = new float[SelectOptions.Length];
        for (int i = 0; i < SelectOptions.Length; i++)
        {
            var elem = SelectOptions[i];
            var l = f.MeasureString(elem.ToString()).X;
            if (l > longestStringLength)
            {
                longestStringLength = l;
                longestOption = elem.ToString();
            }

            _precalculatedOptionSizes[i] = l;
        }
        
        string text = Label + "  < " + longestOption + " >";
        float finalLength = f.MeasureString(text).X;
        float labelLength = f.MeasureString(Label).X;
        float doubleSpace = f.MeasureString("  ").X;
        float singleSpace = f.MeasureString(" ").X;
        float arrowLeftLength = f.MeasureString("<").X;
        
        _startingOffset = new Vector2(-finalLength * 0.5f, 0);
        _arrowLeftOffset = new Vector2(_startingOffset.X + labelLength + doubleSpace, 0);
        _optionOffset = new Vector2(_arrowLeftOffset.X + arrowLeftLength + singleSpace + longestStringLength * 0.5f, 0);
        _arrowRightOffset = new Vector2(_optionOffset.X + longestStringLength * 0.5f + singleSpace, 0);
    }

    public void SetIndex(int index)
    {
        CurrentOption = Math.Clamp(index, 0, SelectOptions.Length - 1);
    }

    public override void Update()
    {
        if (InputManager.GetKeyPress(Keys.Left) || InputManager.GetButtonPress(Buttons.LeftThumbstickLeft) || InputManager.GetButtonPress(Buttons.DPadLeft))
        {
            CurrentOption--;
            if (CurrentOption < 0)
                CurrentOption = SelectOptions.Length - 1;
            OnMenuArraySelectChange?.Invoke(this, SelectOptions[CurrentOption]);
        }

        if (InputManager.GetKeyPress(Keys.Right) || InputManager.GetButtonPress(Buttons.LeftThumbstickRight) || InputManager.GetButtonPress(Buttons.DPadRight))
        {
            CurrentOption++;
            if (CurrentOption >= SelectOptions.Length)
                CurrentOption = 0;
            OnMenuArraySelectChange?.Invoke(this, SelectOptions[CurrentOption]);
        }
    }

    public override void Render(SpriteBatch spriteBatch, FontSystem font, Vector2 position, Color color, TextStyle textStyle)
    {
        var f = font.GetFont(FontSize);
        float currentOptionSize = _precalculatedOptionSizes[CurrentOption];
        spriteBatch.DrawString(f, Label, position + _startingOffset, color, origin: new Vector2(0, FontSize) * 0.5f, textStyle: textStyle);
        spriteBatch.DrawString(f, "<", position + _arrowLeftOffset, color, origin: new Vector2(0, FontSize) * 0.5f);
        spriteBatch.DrawString(f, SelectOptions[CurrentOption].ToString(), position + _optionOffset, color, origin: new Vector2(currentOptionSize, FontSize) * 0.5f);
        spriteBatch.DrawString(f, ">", position + _arrowRightOffset, color, origin: new Vector2(0, FontSize) * 0.5f);
    }

    public delegate void MenuArraySelectChange(MenuArraySelect<T> sender, T selectedOption);

    public event MenuArraySelectChange OnMenuArraySelectChange;
}

public sealed class MenuTextInput : MenuEntry
{
    public string CurrentText { get; set; } = "";
    private string _previousString = "";

    public override void Update()
    {
        if (InputManager.GetKeyPress(Keys.Enter))
        {
            InputManager.StartTextInput(CurrentText, TextInputEvent, FinishTextInput);
        }
    }

    private void TextInputEvent(string s)
    {
        CurrentText = s;
    }

    private void FinishTextInput()
    {
        _previousString = CurrentText;
        OnMenuTextInputValidate?.Invoke(this, CurrentText);
    }

    public override void Render(SpriteBatch spriteBatch, FontSystem font, Vector2 position, Color color,
        TextStyle textStyle)
    {
        var f = font.GetFont(FontSize);
        string text = Label + ": " + CurrentText;
        var length = f.MeasureString(text);
        length.Y = FontSize;
        spriteBatch.DrawString(f, text, position, color, origin: length * 0.5f, textStyle: textStyle);
    }

    public delegate void MenuTextInputValidate(MenuTextInput sender, string text);

    public event MenuTextInputValidate OnMenuTextInputValidate;
}
