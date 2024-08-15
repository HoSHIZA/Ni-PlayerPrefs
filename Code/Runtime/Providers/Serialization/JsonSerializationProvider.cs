using System.Runtime.CompilerServices;
using NiGames.PlayerPrefs.Providers;
using UnityEngine;

namespace NiGames.PlayerPrefs
{
    public static partial class NiPrefs
    {
        /// <summary>
        /// Gets the key value from <c>PlayerPrefs</c> using a json serialized object.
        /// </summary>
        [MethodImpl(256)]
        public static T GetJson<T>(string key, T defaultValue = default, PlayerPrefsEncryption encryption = default)
            => JsonSerializationPlayerPrefsProvider.Get(key, defaultValue, encryption);
        
        /// <summary>
        /// Sets the value of the key in <c>PlayerPrefs</c> json serializing the object.
        /// </summary>
        [MethodImpl(256)]
        public static void SetJson<T>(string key, T value, PlayerPrefsEncryption encryption = default)
            => JsonSerializationPlayerPrefsProvider.Set(key, value, encryption);
    }
    
    namespace Providers
    {
        public readonly struct JsonSerializationPlayerPrefsProvider : IPlayerPrefsProvider
        {
            public static T Get<T>(string key, T defaultValue = default, PlayerPrefsEncryption encryption = default)
            {
                var value = NiPrefs.Internal.GetString(key, null, encryption);
                
                if (value == null) return defaultValue;
                
                try
                {
                    return JsonUtility.FromJson<T>(value);
                }
                catch
                {
                    return defaultValue;
                }
            }
            
            public static void Set<T>(string key, T value, PlayerPrefsEncryption encryption = default)
            {
                var data = JsonUtility.ToJson(value);
                
                NiPrefs.Internal.SetString(key, data, encryption);
            }
        }
    }
}