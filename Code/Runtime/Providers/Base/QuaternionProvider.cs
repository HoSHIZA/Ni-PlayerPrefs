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
        public static Quaternion GetQuaternion(string key, Quaternion defaultValue = default) => default(QuaternionPlayerPrefsProvider).Get(key, defaultValue);
        
        /// <summary>
        /// Sets the value of <see cref="Quaternion"/> in <c>PlayerPrefs</c> by key.
        /// </summary>
        [MethodImpl(256)]
        public static void Set(string key, Quaternion value) => default(QuaternionPlayerPrefsProvider).Set(key, value);
    }
    
    namespace Providers
    {
        internal readonly struct QuaternionPlayerPrefsProvider : IPlayerPrefsProvider<Quaternion>
        {
            public static readonly Regex Regex = new Regex(
                pattern: @"^\(([0-9]+(?>[.][0-9]+)?),\s?([0-9]+(?>[.][0-9]+)?),\s?([0-9]+(?>[.][0-9]+)?),\s?([0-9]+(?>[.][0-9]+)?)\)$", 
                options: RegexOptions.Compiled);


            public Quaternion Get(string key, Quaternion defaultValue = default)
            {
                var input = UnityEngine.PlayerPrefs.GetString(key, defaultValue.ToString());
                var match = Regex.Match(input);
            
                if (!match.Success)
                {
                    if (NiPrefs.EnableLogging)
                    {
                        Debug.LogWarning($"[NiPrefs] PlayerPrefs <color=yellow>\"{key}\"</color> value is incorrect <color=red>\"{input}\"</color>");
                    }
                    return default;
                }
            
                return new Quaternion(
                    float.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture.NumberFormat),
                    float.Parse(match.Groups[2].Value, CultureInfo.InvariantCulture.NumberFormat),
                    float.Parse(match.Groups[3].Value, CultureInfo.InvariantCulture.NumberFormat),
                    float.Parse(match.Groups[4].Value, CultureInfo.InvariantCulture.NumberFormat));
            }

            public void Set(string key, Quaternion value)
            {
                UnityEngine.PlayerPrefs.SetString(key, value.ToString("F3"));
            }
        }
    }
}