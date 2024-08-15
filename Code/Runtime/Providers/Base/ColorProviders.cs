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
        public static Color GetColor(string key, Color defaultValue = default, PlayerPrefsEncryption encryption = default) 
            => default(ColorPlayerPrefsProvider).Get(key, defaultValue, encryption);
        
        /// <summary>
        /// Sets the value of <see cref="Color"/> in <c>PlayerPrefs</c> by key.
        /// </summary>
        [MethodImpl(256)]
        public static void Set(string key, Color value, bool writeAlpha = true, PlayerPrefsEncryption encryption = default) 
            => new ColorPlayerPrefsProvider(writeAlpha).Set(key, value, encryption);
        
        /// <summary>
        /// Returns the <see cref="Color32"/> value stored in <c>PlayerPrefs</c> by key.
        /// </summary>
        [MethodImpl(256)]
        public static Color32 GetColor32(string key, Color32 defaultValue = default, PlayerPrefsEncryption encryption = default) 
            => default(Color32PlayerPrefsProvider).Get(key, defaultValue, encryption);
        
        /// <summary>
        /// Sets the value of <see cref="Color32"/> in <c>PlayerPrefs</c> by key.
        /// </summary>
        [MethodImpl(256)]
        public static void Set(string key, Color32 value, bool writeAlpha = true, PlayerPrefsEncryption encryption = default) 
            => new Color32PlayerPrefsProvider(writeAlpha).Set(key, value, encryption);
    }
    
    namespace Providers
    {
        internal readonly struct ColorPlayerPrefsProvider : IPlayerPrefsProvider<Color>
        {
            private readonly bool _writeAlpha;
            
            public ColorPlayerPrefsProvider(bool writeAlpha)
            {
                _writeAlpha = writeAlpha;
            }
            
            public Color Get(string key, Color defaultValue = default, PlayerPrefsEncryption encryption = default)
            {
                var pref = NiPrefs.Internal.GetString(key, null, encryption);
                
                if (pref == null) return defaultValue;

                if (!pref.StartsWith('#'))
                {
                    pref = pref.Insert(0, "#");
                }
                
                return ColorUtility.TryParseHtmlString(pref, out var color) 
                    ? color 
                    : defaultValue;
            }
            
            public void Set(string key, Color value, PlayerPrefsEncryption encryption = default)
            {
                var hex = _writeAlpha 
                    ? ColorUtility.ToHtmlStringRGBA(value) 
                    : ColorUtility.ToHtmlStringRGB(value);
                
                NiPrefs.Internal.SetString(key, $"#{hex}", encryption);
            }
        }
        
        internal readonly struct Color32PlayerPrefsProvider : IPlayerPrefsProvider<Color32>
        {
            private readonly bool _writeAlpha;
            
            public Color32PlayerPrefsProvider(bool writeAlpha)
            {
                _writeAlpha = writeAlpha;
            }
            
            public Color32 Get(string key, Color32 defaultValue = default, PlayerPrefsEncryption encryption = default)
            {
                var pref = NiPrefs.Internal.GetString(key, null, encryption);
                
                if (pref == null) return defaultValue;

                if (!pref.StartsWith('#'))
                {
                    pref = pref.Insert(0, "#");
                }
                
                return ColorUtility.TryParseHtmlString(pref, out var color) 
                    ? color 
                    : defaultValue;
            }
            
            public void Set(string key, Color32 value, PlayerPrefsEncryption encryption = default)
            {
                var hex = _writeAlpha 
                    ? ColorUtility.ToHtmlStringRGBA(value) 
                    : ColorUtility.ToHtmlStringRGB(value);
                
                NiPrefs.Internal.SetString(key, $"#{hex}", encryption);
            }
        }
    }
}