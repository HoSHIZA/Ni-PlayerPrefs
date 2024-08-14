using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using NiGames.PlayerPrefs.Providers;

namespace NiGames.PlayerPrefs
{
    public static partial class NiPrefs
    {
        /// <summary>
        /// Gets the key value from <c>PlayerPrefs</c> using a binary serialized object.
        /// </summary>
        [MethodImpl(256)]
        public static T GetObject<T>(string key, T defaultValue = default) where T : class, new()
            => ObjectPlayerPrefsProvider.Get(key, defaultValue);
        
        /// <summary>
        /// Sets the value of the key in <c>PlayerPrefs</c> binary serializing the object.
        /// </summary>
        [MethodImpl(256)]
        public static void SetObject<T>(string key, T value) where T : class, new()
            => ObjectPlayerPrefsProvider.Set(key, value);
    }
    
    namespace Providers
    {
        public readonly struct ObjectPlayerPrefsProvider : IPlayerPrefsProvider
        {
            public static T Get<T>(string key, T defaultValue = default)
                where T : class, new()
            {
                var value = UnityEngine.PlayerPrefs.GetString(key);

                if (string.IsNullOrEmpty(value))
                {
                    return defaultValue;
                }
            
                var formatter = new BinaryFormatter();
                var data = Convert.FromBase64String(value);
                var stream = new MemoryStream(data);
            
                return (T)formatter.Deserialize(stream);
            }

            public static void Set<T>(string key, T value)
                where T : class, new()
            {
                var formatter = new BinaryFormatter();
                var stream = new MemoryStream();
            
                formatter.Serialize(stream, value);
                UnityEngine.PlayerPrefs.SetString(key, Convert.ToBase64String(stream.ToArray()));
            }
        }
    }
}