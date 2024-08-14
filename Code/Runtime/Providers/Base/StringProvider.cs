using NiGames.PlayerPrefs.Providers;

namespace NiGames.PlayerPrefs
{
    public static partial class NiPrefs
    {
        /// <summary>
        /// Returns the <see cref="string"/> value stored in <c>PlayerPrefs</c> by key.
        /// </summary>
        public static string GetString(string key, string defaultValue = default) => default(StringPlayerPrefsProvider).Get(key, defaultValue);
        
        /// <summary>
        /// Sets the value of <see cref="string"/> in <c>PlayerPrefs</c> by key.
        /// </summary>
        public static void Set(string key, string value) => default(StringPlayerPrefsProvider).Set(key, value);
    }
    
    namespace Providers
    {
        internal readonly struct StringPlayerPrefsProvider : IPlayerPrefsProvider<string>
        {
            public string Get(string key, string defaultValue = default)
            {
                return UnityEngine.PlayerPrefs.GetString(key, defaultValue);
            }
        
            public void Set(string key, string value)
            {
                UnityEngine.PlayerPrefs.SetString(key, value);
            }
        }
    }
}