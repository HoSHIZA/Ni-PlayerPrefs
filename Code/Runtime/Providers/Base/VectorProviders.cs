using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using NiGames.PlayerPrefs.Providers;
using UnityEngine;

namespace NiGames.PlayerPrefs
{
    public static partial class NiPrefs
    {
        /// <summary>
        /// Returns the <see cref="Vector2"/> value stored in <c>PlayerPrefs</c> by key.
        /// </summary>
        [MethodImpl(256)]
        public static Vector2 GetVector2(string key, Vector2 defaultValue = default, PlayerPrefsEncryption encryption = default) 
            => default(Vector2PlayerPrefsProvider).Get(key, defaultValue, encryption);
        
        /// <summary>
        /// Sets the value of <see cref="Vector2"/> in <c>PlayerPrefs</c> by key.
        /// </summary>
        [MethodImpl(256)]
        public static void Set(string key, Vector2 value, PlayerPrefsEncryption encryption = default) 
            => default(Vector2PlayerPrefsProvider).Set(key, value, encryption);
        
        /// <summary>
        /// Returns the <see cref="Vector2Int"/> value stored in <c>PlayerPrefs</c> by key.
        /// </summary>
        [MethodImpl(256)]
        public static Vector2Int GetVector2Int(string key, Vector2Int defaultValue = default, PlayerPrefsEncryption encryption = default) 
            => default(Vector2IntPlayerPrefsProvider).Get(key, defaultValue, encryption);
        
        /// <summary>
        /// Sets the value of <see cref="Vector2Int"/> in <c>PlayerPrefs</c> by key.
        /// </summary>
        [MethodImpl(256)]
        public static void Set(string key, Vector2Int value, PlayerPrefsEncryption encryption = default)
            => default(Vector2IntPlayerPrefsProvider).Set(key, value, encryption);
        
        /// <summary>
        /// Returns the <see cref="Vector3"/> value stored in <c>PlayerPrefs</c> by key.
        /// </summary>
        [MethodImpl(256)]
        public static Vector3 GetVector3(string key, Vector3 defaultValue = default, PlayerPrefsEncryption encryption = default) 
            => default(Vector3PlayerPrefsProvider).Get(key, defaultValue, encryption);
        
        /// <summary>
        /// Sets the value of <see cref="Vector3"/> in <c>PlayerPrefs</c> by key.
        /// </summary>
        [MethodImpl(256)]
        public static void Set(string key, Vector3 value, PlayerPrefsEncryption encryption = default) 
            => default(Vector3PlayerPrefsProvider).Set(key, value, encryption);
        
        /// <summary>
        /// Returns the <see cref="Vector3Int"/> value stored in <c>PlayerPrefs</c> by key.
        /// </summary>
        [MethodImpl(256)]
        public static Vector3Int GetVector3Int(string key, Vector3Int defaultValue = default, PlayerPrefsEncryption encryption = default) 
            => default(Vector3IntPlayerPrefsProvider).Get(key, defaultValue, encryption);
        
        /// <summary>
        /// Sets the value of <see cref="Vector3Int"/> in <c>PlayerPrefs</c> by key.
        /// </summary>
        [MethodImpl(256)]
        public static void Set(string key, Vector3Int value, PlayerPrefsEncryption encryption = default) 
            => default(Vector3IntPlayerPrefsProvider).Set(key, value, encryption);
        
        /// <summary>
        /// Returns the <see cref="Vector4"/> value stored in <c>PlayerPrefs</c> by key.
        /// </summary>
        [MethodImpl(256)]
        public static Vector4 GetVector4(string key, Vector4 defaultValue = default, PlayerPrefsEncryption encryption = default) 
            => default(Vector4PlayerPrefsProvider).Get(key, defaultValue, encryption);
        
        /// <summary>
        /// Sets the value of <see cref="Vector4"/> in <c>PlayerPrefs</c> by key.
        /// </summary>
        [MethodImpl(256)]
        public static void Set(string key, Vector4 value, PlayerPrefsEncryption encryption = default) 
            => default(Vector4PlayerPrefsProvider).Set(key, value, encryption);
    }
    
    namespace Providers
    {
        internal readonly struct Vector2PlayerPrefsProvider : IPlayerPrefsProvider<Vector2>
        {
            public static readonly Regex Regex = new Regex(
                pattern: @"^\(([0-9]+(?:[.][0-9]+)?),\s?([0-9]+(?:[.][0-9]+)?)\)$", 
                options: RegexOptions.Compiled);
            
            public void Set(string key, Vector2 value, PlayerPrefsEncryption encryption = default)
            {
                NiPrefs.Internal.SetString(key, value.ToString("F3"), encryption);
            }
            
            public Vector2 Get(string key, Vector2 defaultValue = default, PlayerPrefsEncryption encryption = default)
            {
                var input = NiPrefs.Internal.GetString(key, null, encryption);
                
                if (input == null) return defaultValue;
                
                var match = Regex.Match(input);
                
                if (!match.Success)
                {
                    if (NiPrefs.Settings.EnableLogging)
                    {
                        Debug.LogWarning($"[NiPrefs] PlayerPrefs <color=yellow>\"{key}\"</color> value is incorrect <color=red>\"{input}\"</color>");
                    }
                    return defaultValue;
                }
                
                return new Vector2(
                    float.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture.NumberFormat),
                    float.Parse(match.Groups[2].Value, CultureInfo.InvariantCulture.NumberFormat));
            }
        }
        
        internal readonly struct Vector2IntPlayerPrefsProvider : IPlayerPrefsProvider<Vector2Int>
        {
            public static readonly Regex Regex = new Regex(
                pattern: @"^\(([0-9]+),\s?([0-9]+)\)$", 
                options: RegexOptions.Compiled);
            
            public void Set(string key, Vector2Int value, PlayerPrefsEncryption encryption = default)
            {
                NiPrefs.Internal.SetString(key, value.ToString("F3"), encryption);
            }
            
            public Vector2Int Get(string key, Vector2Int defaultValue = default, PlayerPrefsEncryption encryption = default)
            {
                var input = NiPrefs.Internal.GetString(key, null, encryption);
                
                if (input == null) return defaultValue;
                
                var match = Regex.Match(input);
                
                if (!match.Success)
                {
                    if (NiPrefs.Settings.EnableLogging)
                    {
                        Debug.LogWarning($"[NiPrefs] PlayerPrefs <color=yellow>\"{key}\"</color> value is incorrect <color=red>\"{input}\"</color>");
                    }
                    return defaultValue;
                }
                
                return new Vector2Int(
                    int.Parse(match.Groups[1].Value), 
                    int.Parse(match.Groups[2].Value));
            }
        }
        
        internal readonly struct Vector3PlayerPrefsProvider : IPlayerPrefsProvider<Vector3>
        {
            public static readonly Regex Regex = new Regex(
                pattern: @"^\(([0-9]+(?>[.][0-9]+)?),\s?([0-9]+(?>[.][0-9]+)?),\s?([0-9]+(?>[.][0-9]+)?)\)$", 
                options: RegexOptions.Compiled);
            
            public void Set(string key, Vector3 value, PlayerPrefsEncryption encryption = default)
            {
                NiPrefs.Internal.SetString(key, value.ToString("F3"), encryption);
            }
            
            public Vector3 Get(string key, Vector3 defaultValue = default, PlayerPrefsEncryption encryption = default)
            {
                var input = NiPrefs.Internal.GetString(key, null, encryption);
                
                if (input == null) return defaultValue;
                
                var match = Regex.Match(input);
                
                if (!match.Success)
                {
                    if (NiPrefs.Settings.EnableLogging)
                    {
                        Debug.LogWarning($"[NiPrefs] PlayerPrefs <color=yellow>\"{key}\"</color> value is incorrect <color=red>\"{input}\"</color>");
                    }
                    return defaultValue;
                }
                
                return new Vector3(
                    float.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture.NumberFormat),
                    float.Parse(match.Groups[2].Value, CultureInfo.InvariantCulture.NumberFormat),
                    float.Parse(match.Groups[3].Value, CultureInfo.InvariantCulture.NumberFormat));
            }
        }
        
        internal readonly struct Vector3IntPlayerPrefsProvider : IPlayerPrefsProvider<Vector3Int>
        {
            public static readonly Regex Regex = new Regex(
                pattern: @"^\(([0-9]+),\s?([0-9]+),\s?([0-9]+)\)$", 
                options: RegexOptions.Compiled);
            
            public void Set(string key, Vector3Int value, PlayerPrefsEncryption encryption = default)
            {
                NiPrefs.Internal.SetString(key, value.ToString("F3"), encryption);
            }
            
            public Vector3Int Get(string key, Vector3Int defaultValue = default, PlayerPrefsEncryption encryption = default)
            {
                var input = NiPrefs.Internal.GetString(key, null, encryption);
                
                if (input == null) return defaultValue;
                
                var match = Regex.Match(input);
                
                if (!match.Success)
                {
                    if (NiPrefs.Settings.EnableLogging)
                    {
                        Debug.LogWarning($"[NiPrefs] PlayerPrefs <color=yellow>\"{key}\"</color> value is incorrect <color=red>\"{input}\"</color>");
                    }
                    return defaultValue;
                }
                
                return new Vector3Int(
                    int.Parse(match.Groups[1].Value),
                    int.Parse(match.Groups[2].Value),
                    int.Parse(match.Groups[3].Value));
            }
        }
        
        internal readonly struct Vector4PlayerPrefsProvider : IPlayerPrefsProvider<Vector4>
        {
            public static readonly Regex Regex = new Regex(
                pattern: @"^\(([0-9]+(?>[.][0-9]+)?),\s?([0-9]+(?>[.][0-9]+)?),\s?([0-9]+(?>[.][0-9]+)?),\s?([0-9]+(?>[.][0-9]+)?)\)$", 
                options: RegexOptions.Compiled);
            
            public void Set(string key, Vector4 value, PlayerPrefsEncryption encryption = default)
            {
                NiPrefs.Internal.SetString(key, value.ToString("F3"), encryption);
            }
            
            public Vector4 Get(string key, Vector4 defaultValue = default, PlayerPrefsEncryption encryption = default)
            {
                var input = NiPrefs.Internal.GetString(key, null, encryption);
                
                if (input == null) return defaultValue;
                
                var match = Regex.Match(input);
                
                if (!match.Success)
                {
                    if (NiPrefs.Settings.EnableLogging)
                    {
                        Debug.LogWarning($"[NiPrefs] PlayerPrefs <color=yellow>\"{key}\"</color> value is incorrect <color=red>\"{input}\"</color>");
                    }
                    return defaultValue;
                }
                
                return new Vector4(
                    float.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture.NumberFormat),
                    float.Parse(match.Groups[2].Value, CultureInfo.InvariantCulture.NumberFormat),
                    float.Parse(match.Groups[3].Value, CultureInfo.InvariantCulture.NumberFormat),
                    float.Parse(match.Groups[4].Value, CultureInfo.InvariantCulture.NumberFormat));
            }
        }
    }
}