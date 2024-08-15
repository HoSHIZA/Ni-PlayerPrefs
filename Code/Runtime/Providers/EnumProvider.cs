using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using NiGames.PlayerPrefs.Providers;
using UnityEngine;

namespace NiGames.PlayerPrefs
{
    public static partial class NiPrefs
    {
        /// <summary>
        /// Returns the enum <c>T</c> value stored in <c>PlayerPrefs</c> by key.
        /// </summary>
        [MethodImpl(256)]
        public static T GetEnum<T>(string key, T defaultValue = default, PlayerPrefsEncryption encryption = default) where T : Enum 
            => EnumPlayerPrefsProvider.Get(key, defaultValue, encryption);
        
        /// <summary>
        /// Sets the enum value of <c>T</c> in <c>PlayerPrefs</c> by key.
        /// </summary>
        [MethodImpl(256)]
        public static void SetEnum<T>(string key, T value, PlayerPrefsEncryption encryption = default) where T : Enum 
            => EnumPlayerPrefsProvider.Set(key, value, encryption);
    }
    
    namespace Providers
    {
        internal readonly struct EnumPlayerPrefsProvider : IPlayerPrefsProvider
        {
            public static readonly Regex Regex = new Regex(
                pattern: @"^([a-zA-Z]{1}[\w0-9]*)\s+([\w0-9]+)$", 
                options: RegexOptions.Compiled);
        
            public static void Set<T>(string key, T value, PlayerPrefsEncryption encryption = default)
                where T : Enum
            {
                NiPrefs.Internal.SetString(key, $"{value.GetType().Name} {value.ToString()}", encryption);
            }
        
            public static T Get<T>(string key, T defaultValue = default, PlayerPrefsEncryption encryption = default)
                where T : Enum
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
                
                if (typeof(T).Name != match.Groups[1].Value)
                {
                    if (NiPrefs.Settings.EnableLogging)
                    {
                        Debug.LogError($"[NiPrefs] PlayerPrefs <color=yellow>\"{key}\"</color> contains another Enum Type ({typeof(T).Name} => {match.Groups[1].Value})");
                    }
                    return defaultValue;
                }
                
                return (T) Enum.Parse(typeof(T), match.Groups[2].Value, true);
            }
        }
    }
}