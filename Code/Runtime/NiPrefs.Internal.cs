using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace NiGames.PlayerPrefs
{
    public static partial class NiPrefs
    {
        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        internal static class Internal
        {
            /// <summary>
            /// Gets the <c>int</c> value from an entry in PlayerPrefs, using the specified encryption settings.
            /// </summary>
            public static int GetInt(string key, int defaultValue, PlayerPrefsEncryption encryption = default)
            {
                key = Encryption.GetEncodedKey(key, encryption);
                
                if ((encryption is PlayerPrefsEncryption.UseEncryptionSettings && Settings.EncryptValue) ||
                    encryption is PlayerPrefsEncryption.WithEncryption ||
                    encryption is PlayerPrefsEncryption.OnlyValueEncryption)
                {
                    var str = UnityEngine.PlayerPrefs.GetString(key, null);
                    
                    if (string.IsNullOrEmpty(str)) return defaultValue;
                    
                    var decoded = Encryption.DecodeString(str, Encryption.Hash);
                    
                    return int.TryParse(decoded, out var result) ? result : defaultValue;
                }
                
                return UnityEngine.PlayerPrefs.GetInt(key, defaultValue);
            }
            
            /// <summary>
            /// Gets the <c>float</c> value from an entry in PlayerPrefs, using the specified encryption settings.
            /// </summary>
            public static float GetFloat(string key, float defaultValue, PlayerPrefsEncryption encryption = default)
            {
                key = Encryption.GetEncodedKey(key, encryption);
                
                if ((encryption is PlayerPrefsEncryption.UseEncryptionSettings && Settings.EncryptValue) ||
                    encryption is PlayerPrefsEncryption.WithEncryption ||
                    encryption is PlayerPrefsEncryption.OnlyValueEncryption)
                {
                    var str = UnityEngine.PlayerPrefs.GetString(key, null);
                    
                    if (string.IsNullOrEmpty(str)) return defaultValue;
                    
                    var decoded = Encryption.DecodeString(str, Encryption.Hash);
                    
                    return float.TryParse(decoded, out var result) ? result : defaultValue;
                }
                
                return UnityEngine.PlayerPrefs.GetFloat(key, defaultValue);
            }
            
            /// <summary>
            /// Gets the <c>string</c> value from an entry in PlayerPrefs, using the specified encryption settings.
            /// </summary>
            public static string GetString(string key, string defaultValue, PlayerPrefsEncryption encryption = default)
            {
                key = Encryption.GetEncodedKey(key, encryption);
                
                if ((encryption is PlayerPrefsEncryption.UseEncryptionSettings && Settings.EncryptValue) ||
                    encryption is PlayerPrefsEncryption.WithEncryption ||
                    encryption is PlayerPrefsEncryption.OnlyValueEncryption)
                {
                    var str = UnityEngine.PlayerPrefs.GetString(key, null);
                    
                    if (string.IsNullOrEmpty(str)) return defaultValue;
                    
                    return Encryption.DecodeString(str, Encryption.Hash);
                }
                
                return UnityEngine.PlayerPrefs.GetString(key, defaultValue);
            }
            
            /// <summary>
            /// Sets the <c>int</c> value in PlayerPrefs using the specified encryption settings.
            /// </summary>
            public static void SetInt(string key, int value, PlayerPrefsEncryption encryption = default)
            {
                key = Encryption.GetEncodedKey(key, encryption);
                
                if ((encryption is PlayerPrefsEncryption.UseEncryptionSettings && Settings.EncryptValue) ||
                    encryption is PlayerPrefsEncryption.WithEncryption ||
                    encryption is PlayerPrefsEncryption.OnlyValueEncryption)
                {
                    var str = Encryption.EncodeString(value.ToString(CultureInfo.InvariantCulture), Encryption.Hash);
                    
                    UnityEngine.PlayerPrefs.SetString(key, str);
                    
                    return;
                }
                
                UnityEngine.PlayerPrefs.SetInt(key, value);
            }
            
            /// <summary>
            /// Sets the <c>float</c> value in PlayerPrefs using the specified encryption settings.
            /// </summary>
            public static void SetFloat(string key, float value, PlayerPrefsEncryption encryption = default)
            {
                key = Encryption.GetEncodedKey(key, encryption);
                
                if ((encryption is PlayerPrefsEncryption.UseEncryptionSettings && Settings.EncryptValue) ||
                    encryption is PlayerPrefsEncryption.WithEncryption ||
                    encryption is PlayerPrefsEncryption.OnlyValueEncryption)
                {
                    var str = Encryption.EncodeString(value.ToString(CultureInfo.InvariantCulture), Encryption.Hash);
                    
                    UnityEngine.PlayerPrefs.SetString(key, str);
                    
                    return;
                }
                
                UnityEngine.PlayerPrefs.SetFloat(key, value);
            }

            /// <summary>
            /// Sets the <c>string</c> value in PlayerPrefs using the specified encryption settings.
            /// </summary>
            public static void SetString(string key, string value, PlayerPrefsEncryption encryption = default)
            {
                key = Encryption.GetEncodedKey(key, encryption);
                
                if ((encryption is PlayerPrefsEncryption.UseEncryptionSettings && Settings.EncryptValue) ||
                    encryption is PlayerPrefsEncryption.WithEncryption ||
                    encryption is PlayerPrefsEncryption.OnlyValueEncryption)
                {
                    value = Encryption.EncodeString(value, Encryption.Hash);
                }
                
                UnityEngine.PlayerPrefs.SetString(key, value);
            }
        }
    }
}