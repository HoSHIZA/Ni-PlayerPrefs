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
        /// Returns the <see cref="Quaternion"/> value stored in <c>PlayerPrefs</c> by key.
        /// </summary>
        [MethodImpl(256)]
        public static Quaternion GetQuaternion(string key, Quaternion defaultValue = default, PlayerPrefsEncryption encryption = default) 
            => default(QuaternionPlayerPrefsProvider).Get(key, defaultValue, encryption);
        
        /// <summary>
        /// Sets the value of <see cref="Quaternion"/> in <c>PlayerPrefs</c> by key.
        /// </summary>
        [MethodImpl(256)]
        public static void Set(string key, Quaternion value, PlayerPrefsEncryption encryption = default) 
            => default(QuaternionPlayerPrefsProvider).Set(key, value, encryption);
    }
    
    namespace Providers
    {
        internal readonly struct QuaternionPlayerPrefsProvider : IPlayerPrefsProvider<Quaternion>
        {
            public static readonly Regex Regex = new Regex(
                pattern: @"^\(([0-9]+(?>[.][0-9]+)?),\s?([0-9]+(?>[.][0-9]+)?),\s?([0-9]+(?>[.][0-9]+)?),\s?([0-9]+(?>[.][0-9]+)?)\)$", 
                options: RegexOptions.Compiled);
            
            public Quaternion Get(string key, Quaternion defaultValue = default, PlayerPrefsEncryption encryption = default)
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
            
                return new Quaternion(
                    float.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture.NumberFormat),
                    float.Parse(match.Groups[2].Value, CultureInfo.InvariantCulture.NumberFormat),
                    float.Parse(match.Groups[3].Value, CultureInfo.InvariantCulture.NumberFormat),
                    float.Parse(match.Groups[4].Value, CultureInfo.InvariantCulture.NumberFormat));
            }

            public void Set(string key, Quaternion value, PlayerPrefsEncryption encryption = default)
            {
                NiPrefs.Internal.SetString(key, value.ToString("F3"), encryption);
            }
        }
    }
}