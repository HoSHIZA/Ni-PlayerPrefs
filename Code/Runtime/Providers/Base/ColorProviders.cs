using System.Runtime.CompilerServices;
using NiGames.PlayerPrefs.Providers;
using UnityEngine;

namespace NiGames.PlayerPrefs
{
    public static partial class NiPrefs
    {
        /// <summary>
        /// Returns the <see cref="Color"/> value stored in <c>PlayerPrefs</c> by key.
        /// </summary>
        [MethodImpl(256)]
        public static Color GetColor(string key, Color defaultValue = default) => default(ColorPlayerPrefsProvider).Get(key, defaultValue);
        
        /// <summary>
        /// Sets the value of <see cref="Color"/> in <c>PlayerPrefs</c> by key.
        /// </summary>
        [MethodImpl(256)]
        public static void Set(string key, Color value, bool writeAlpha = true) => new ColorPlayerPrefsProvider(writeAlpha).Get(key, value);
        
        /// <summary>
        /// Returns the <see cref="Color32"/> value stored in <c>PlayerPrefs</c> by key.
        /// </summary>
        [MethodImpl(256)]
        public static Color32 GetColor32(string key, Color32 defaultValue = default) => default(Color32PlayerPrefsProvider).Get(key, defaultValue);
        
        /// <summary>
        /// Sets the value of <see cref="Color32"/> in <c>PlayerPrefs</c> by key.
        /// </summary>
        [MethodImpl(256)]
        public static void Set(string key, Color32 value, bool writeAlpha = true) => new Color32PlayerPrefsProvider(writeAlpha).Get(key, value);
    }
}

namespace NiGames.PlayerPrefs.Providers
{
    internal readonly struct ColorPlayerPrefsProvider : IPlayerPrefsProvider<Color>
    {
        private readonly bool _writeAlpha;
        
        public ColorPlayerPrefsProvider(bool writeAlpha)
        {
            _writeAlpha = writeAlpha;
        }
        
        public Color Get(string key, Color defaultValue = default)
        {
            var pref = UnityEngine.PlayerPrefs.GetString(key, null);
            
            if (pref == null) return defaultValue;
            
            return ColorUtility.TryParseHtmlString(pref, out var color) 
                ? color 
                : defaultValue;
        }
        
        public void Set(string key, Color value)
        {
            var hex = _writeAlpha 
                ? ColorUtility.ToHtmlStringRGBA(value) 
                : ColorUtility.ToHtmlStringRGB(value);

            UnityEngine.PlayerPrefs.SetString(key, hex);
        }
    }
    
    internal readonly struct Color32PlayerPrefsProvider : IPlayerPrefsProvider<Color32>
    {
        private readonly bool _writeAlpha;
        
        public Color32PlayerPrefsProvider(bool writeAlpha)
        {
            _writeAlpha = writeAlpha;
        }
        
        public Color32 Get(string key, Color32 defaultValue = default)
        {
            var pref = UnityEngine.PlayerPrefs.GetString(key, null);
            
            if (pref == null) return defaultValue;
            
            return ColorUtility.TryParseHtmlString(pref, out var color) 
                ? color 
                : defaultValue;
        }
        
        public void Set(string key, Color32 value)
        {
            var hex = _writeAlpha 
                ? ColorUtility.ToHtmlStringRGBA(value) 
                : ColorUtility.ToHtmlStringRGB(value);
            
            UnityEngine.PlayerPrefs.SetString(key, hex);
        }
    }
}