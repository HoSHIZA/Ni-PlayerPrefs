using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace NiGames.PlayerPrefs
{
    public static partial class NiPrefs
    {
        public static class Encryption
        {
#if UNITY_EDITOR
            private static readonly string _hash = "062e8911666e07d331ceb351d8d31faa";
#else
            private static readonly string _hash = UnityEngine.SystemInfo.deviceUniqueIdentifier;
#endif
            
            public static string Hash => string.IsNullOrEmpty(Settings.EncryptionHash) ? _hash : Settings.EncryptionHash;
            
            [MethodImpl(256)]
            internal static string GetEncodedKey(string key, PlayerPrefsEncryption encryption)
            {
                if ((encryption is PlayerPrefsEncryption.UseEncryptionSettings && Settings.EncryptKey) || 
                    encryption is PlayerPrefsEncryption.WithEncryption ||
                    encryption is PlayerPrefsEncryption.OnlyKeyEncryption)
                {
                    return EncodeString(key, Hash);
                }
                
                return key;
            }
            
            public static string EncodeString(string input, string hash)
            {
                var inputBytes = Encoding.UTF8.GetBytes(input);
                var hashBytes = Encoding.UTF8.GetBytes(hash);
                
                var result = new byte[inputBytes.Length];
                
                for (var i = 0; i < inputBytes.Length; i++)
                {
                    result[i] = (byte)(inputBytes[i] ^ hashBytes[i % hashBytes.Length]);
                }
                
                return Convert.ToBase64String(result);
            }
            
            public static string DecodeString(string encoded, string hash)
            {
                var inputBytes = Convert.FromBase64String(encoded);
                var hashBytes = Encoding.UTF8.GetBytes(hash);

                var result = new byte[inputBytes.Length];

                for (var i = 0; i < inputBytes.Length; i++)
                {
                    result[i] = (byte)(inputBytes[i] ^ hashBytes[i % hashBytes.Length]);
                }

                return Encoding.UTF8.GetString(result);
            }
        }
    }
}