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
    internal struct FontLoadRequest
    {
        public string FontName;
        public string[] FontFiles;
    }
    
    private static ContentManager _content;
    private static GraphicsDevice _graphicsDevice;

    /// <summary>
    /// A reference to a single white pixel.
    /// </summary>
    public static Texture2D Pixel { get; private set; }

    public static string TexturesRoot { get; set; }
    public static string SoundsRoot { get; set; }
    public static string FontsRoot { get; set; }
    public static string EffectsRoot { get; set; }
    public static string AssetsPath => _content.RootDirectory;

    private static string GetTexturesPath() => string.IsNullOrWhiteSpace(TexturesRoot) ? "" : TexturesRoot + "/";
    private static string GetSoundsPath() => string.IsNullOrWhiteSpace(SoundsRoot) ? "" : SoundsRoot + "/";
    private static string GetEffectsPath() => string.IsNullOrWhiteSpace(EffectsRoot) ? "" : EffectsRoot + "/";

    private static Dictionary<string, Texture2D> _loadedTextures = new();
    private static Dictionary<string, SoundEffect> _loadedSounds = new();
    private static Dictionary<string, FontSystem> _loadedFontFamilies = new();
    private static Dictionary<string, Effect> _loadedEffects = new();

    private static List<FontLoadRequest> _fontsToLoad = new();
    
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
            string path = GetTexturesPath() + assetName;
            tex = _content.Load<Texture2D>(path);
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
            string path = GetSoundsPath() + assetName;
            sfx = _content.Load<SoundEffect>(path);
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
        _fontsToLoad.Add(new FontLoadRequest
        {
            FontName = fontName,
            FontFiles = fontFiles,
        });
    }

    internal static void LoadFonts()
    {
        foreach (var fontToLoad in _fontsToLoad)
        {
            FontSystem font = new();
            foreach (string file in fontToLoad.FontFiles)
            {
                string f = string.IsNullOrWhiteSpace(FontsRoot) ? file : Path.Join(FontsRoot, file);
                string path = Path.Join(_content.RootDirectory, f);
                font.AddFont(File.ReadAllBytes(path));
            }

            _loadedFontFamilies.Add(fontToLoad.FontName, font);
        }
    }

    public static FontSystem GetFontSystem(string fontName)
    {
        return _loadedFontFamilies[fontName];
    }

    /// <summary>
    /// Gets a shader effect corresponding to the requested name. If the shader effect isn't already loaded, it will be cached when calling this method.
    /// </summary>
    /// <param name="assetName">The effect to load/get</param>
    /// <returns>The asset requested, or null if not found.</returns>
    public static Effect GetEffect(string assetName)
    {
        if (_loadedEffects.TryGetValue(assetName, out var effect)) return effect;
        try
        {
            string path = GetEffectsPath() + assetName;
            effect = _content.Load<Effect>(path);
            _loadedEffects.Add(assetName, effect);
            return effect;
        }
        catch
        {
            return null;
        }
    }
}
