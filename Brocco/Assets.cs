using System.Collections.Generic;
using System.IO;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Brocco;

public static class Assets
{
    private static ContentManager _content;
    private static GraphicsDevice _graphicsDevice;

    /// <summary>
    /// A reference to a single white pixel.
    /// </summary>
    public static Texture2D Pixel { get; private set; }

    private static Dictionary<string, Texture2D> _loadedTextures = new();
    private static Dictionary<string, SoundEffect> _loadedSounds = new();
    private static Dictionary<string, FontSystem> _loadedFontFamilies = new();

    internal static void Prepare(ContentManager content, GraphicsDevice graphicsDevice)
    {
        _content = content;
        _graphicsDevice = graphicsDevice;
        Pixel = new Texture2D(_graphicsDevice, 1, 1);
        Pixel.SetData(new[] { Color.White });
    }

    /// <summary>
    /// Gets a texture corresponding to the requested name. If the texture isn't already loaded, it will be cached when calling this method.
    /// </summary>
    /// <param name="assetName">The asset to load/get</param>
    /// <returns>The asset requested, or a white pixel if not found.</returns>
    public static Texture2D GetTexture(string assetName)
    {
        if (_loadedTextures.TryGetValue(assetName, out var tex)) return tex;
        try
        {
            tex = _content.Load<Texture2D>(assetName);
            _loadedTextures.Add(assetName, tex);
            return tex;
        }
        catch
        {
            return Pixel;
        }
    }

    /// <summary>
    /// Gets a sound effect corresponding to the requested name. If the sound effect isn't already loaded, it will be cached when calling this method.
    /// </summary>
    /// <param name="assetName">The asset to load/get</param>
    /// <returns>The asset requested, or null if not found.</returns>
    public static SoundEffect GetSound(string assetName)
    {
        if (_loadedSounds.TryGetValue(assetName, out var sfx)) return sfx;
        try
        {
            sfx = _content.Load<SoundEffect>(assetName);
            _loadedSounds.Add(assetName, sfx);
            return sfx;
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Preloads a font for future use.
    /// </summary>
    /// <param name="fontName">The font name</param>
    /// <param name="fontFiles">A collection of paths to the font</param>
    public static void PreloadFont(string fontName, string[] fontFiles)
    {
        FontSystem font = new();
        foreach (string file in fontFiles)
        {
            var path = Path.Join(_content.RootDirectory, file);
            font.AddFont(File.ReadAllBytes(path));
        }
        _loadedFontFamilies.Add(fontName, font);
    }

    public static FontSystem GetFontSystem(string fontName)
    {
        return _loadedFontFamilies[fontName];
    }
}
