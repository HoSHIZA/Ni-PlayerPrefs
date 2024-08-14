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
        public static T GetEnum<T>(string key, T defaultValue = default) where T : Enum 
            => EnumPlayerPrefsProvider.Get(key, defaultValue);
        
        /// <summary>
        /// Sets the enum value of <c>T</c> in <c>PlayerPrefs</c> by key.
        /// </summary>
        [MethodImpl(256)]
        public static void SetEnum<T>(string key, T value) where T : Enum 
            => EnumPlayerPrefsProvider.Set(key, value);
    }
    
    namespace Providers
    {
        internal readonly struct EnumPlayerPrefsProvider : IPlayerPrefsProvider
        {
            public static readonly Regex Regex = new Regex(
                pattern: @"^([a-zA-Z]{1}[\w0-9]*)\s+([\w0-9]+)$", 
                options: RegexOptions.Compiled);
        
            public static void Set<T>(string key, T value)
                where T : Enum
            {
                UnityEngine.PlayerPrefs.SetString(key, $"{value.GetType().Name} {value.ToString()}");
            }
        
            public static T Get<T>(string key, T defaultValue = default)
                where T : Enum
            {
                var input = UnityEngine.PlayerPrefs.GetString(key, defaultValue != null 
                    ? $"{defaultValue.GetType().Name} {defaultValue.ToString()}"
                    : null);
            
                if (input == null) return default;
            
                var match = Regex.Match(input);
            
                if (!match.Success)
                {
                    if (NiPrefs.EnableLogging)
                    {
                        Debug.LogWarning($"[NiPrefs] PlayerPrefs <color=yellow>\"{key}\"</color> value is incorrect <color=red>\"{input}\"</color>");
                    }
                    return default;
                }
            
                if (typeof(T).Name != match.Groups[1].Value)
                {
                    if (NiPrefs.EnableLogging)
                    {
                        Debug.LogError($"[NiPrefs] PlayerPrefs <color=yellow>\"{key}\"</color> contains another Enum Type ({typeof(T).Name} => {match.Groups[1].Value})");
                    }
                    return default;
                }
            
                return (T) Enum.Parse(typeof(T), match.Groups[2].Value, true);
            }
        }
    }
}