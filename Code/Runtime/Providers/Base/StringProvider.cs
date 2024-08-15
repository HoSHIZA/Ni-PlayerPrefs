using NiGames.PlayerPrefs.Providers;

namespace NiGames.PlayerPrefs
{
    public static partial class NiPrefs
    {
        /// <summary>
        /// Returns the <see cref="string"/> value stored in <c>PlayerPrefs</c> by key.
        /// </summary>
        public static string GetString(string key, string defaultValue = default, PlayerPrefsEncryption encryption = default) 
            => default(StringPlayerPrefsProvider).Get(key, defaultValue, encryption);
        
        /// <summary>
        /// Sets the value of <see cref="string"/> in <c>PlayerPrefs</c> by key.
        /// </summary>
        public static void Set(string key, string value, PlayerPrefsEncryption encryption = default) 
            => default(StringPlayerPrefsProvider).Set(key, value, encryption);
    }
    
    namespace Providers
    {
        internal readonly struct StringPlayerPrefsProvider : IPlayerPrefsProvider<string>
        {
            public string Get(string key, string defaultValue = default, PlayerPrefsEncryption encryption = default)
            {
                return NiPrefs.Internal.GetString(key, defaultValue, encryption);
            }
        
            public void Set(string key, string value, PlayerPrefsEncryption encryption = default)
            {
                NiPrefs.Internal.SetString(key, value, encryption);
            }
        }
    }
}