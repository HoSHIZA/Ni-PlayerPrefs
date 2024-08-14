using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using NiGames.PlayerPrefs.Providers;
using NiGames.PlayerPrefs.Utility;
using UnityEngine;

namespace NiGames.PlayerPrefs
{
    public static partial class NiPrefs
    {
        /// <summary>
        /// Returns the <see cref="Resolution"/> value stored in <c>PlayerPrefs</c> by key.
        /// </summary>
        [MethodImpl(256)]
        public static Resolution GetResolution(string key, Resolution defaultValue = default) => default(ResolutionPlayerPrefsProvider).Get(key, defaultValue);
        
        /// <summary>
        /// Sets the value of <see cref="Resolution"/> in <c>PlayerPrefs</c> by key.
        /// </summary>
        [MethodImpl(256)]
        public static void Set(string key, Resolution value) => default(ResolutionPlayerPrefsProvider).Set(key, value);
    }
    
    namespace Providers
    {
        internal readonly struct ResolutionPlayerPrefsProvider : IPlayerPrefsProvider<Resolution>
        {
            public static readonly Regex Regex = new Regex(
#if UNITY_2022_2_OR_NEWER
                pattern: @"^([0-9]+)\s?x\s?([0-9]+)\s?@\s?([0-9]+(?:\.?[0-9]+))(?:[Hh][Zz])?$",
#else
            pattern: @"^([0-9]+)\s?x\s?([0-9]+)\s?@\s?([0-9]+)(?:[Hh][Zz])?$",
#endif
                options: RegexOptions.Compiled);
        
            public Resolution Get(string key, Resolution defaultValue = default)
            {
                var input = UnityEngine.PlayerPrefs.GetString(key, defaultValue.ToString());
                Debug.LogWarning(input);
                var match = Regex.Match(input);
            
                if (!match.Success)
                {
                    if (NiPrefs.EnableLogging)
                    {
                        Debug.LogWarning($"[NiPrefs] PlayerPrefs <color=yellow>\"{key}\"</color> value is incorrect <color=red>\"{input}\"</color>");
                    }
                
                    UnityEngine.PlayerPrefs.DeleteKey(key);
                    return default;
                }
            
                var resolution = new Resolution
                {
                    width = int.Parse(match.Groups[1].Value),
                    height = int.Parse(match.Groups[2].Value),
#if UNITY_2022_2_OR_NEWER
                    refreshRateRatio = ResolutionUtility.ConvertToRefreshRateRatio(double.Parse(match.Groups[3].Value)),
#else
                refreshRate = int.Parse(match.Groups[3].Value),
#endif
                };
            
                return resolution;
            }
        
            public void Set(string key, Resolution value)
            {
                UnityEngine.PlayerPrefs.SetString(key, value.ToString());
            }
        }
    }
}