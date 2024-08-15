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
        public static T GetBinary<T>(string key, T defaultValue = default, PlayerPrefsEncryption encryption = default) 
            => BinarySerializationPlayerPrefsProvider.Get(key, defaultValue, encryption);
        
        /// <summary>
        /// Sets the value of the key in <c>PlayerPrefs</c> binary serializing the object.
        /// </summary>
        [MethodImpl(256)]
        public static void SetBinary<T>(string key, T value, PlayerPrefsEncryption encryption = default) 
            => BinarySerializationPlayerPrefsProvider.Set(key, value, encryption);
    }
    
    namespace Providers
    {
        public readonly struct BinarySerializationPlayerPrefsProvider : IPlayerPrefsProvider
        {
            public static T Get<T>(string key, T defaultValue = default, PlayerPrefsEncryption encryption = default)
            {
                var value = NiPrefs.Internal.GetString(key, null, encryption);
                
                if (value == null) return defaultValue;
                
                var formatter = new BinaryFormatter();
                var data = Convert.FromBase64String(value);
                var stream = new MemoryStream(data);
                
                try
                {
                    return (T)formatter.Deserialize(stream);
                }
                catch
                {
                    return defaultValue;
                }
            }
            
            public static void Set<T>(string key, T value, PlayerPrefsEncryption encryption = default)
            {
                var formatter = new BinaryFormatter();
                var stream = new MemoryStream();
                
                formatter.Serialize(stream, value);
                
                var data = Convert.ToBase64String(stream.ToArray());
                
                NiPrefs.Internal.SetString(key, data, encryption);
            }
        }
    }
}