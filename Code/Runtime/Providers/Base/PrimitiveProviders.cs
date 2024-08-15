using System.Runtime.CompilerServices;
using NiGames.PlayerPrefs.Providers;

namespace NiGames.PlayerPrefs
{
    public static partial class NiPrefs
    {
        /// <summary>
        /// Returns the <see cref="int"/> value stored in <c>PlayerPrefs</c> by key.
        /// </summary>
        [MethodImpl(256)]
        public static int GetInt(string key, int defaultValue = default, PlayerPrefsEncryption encryption = default) 
            => default(IntPlayerPrefsProvider).Get(key, defaultValue, encryption);
        
        /// <summary>
        /// Sets the value of <see cref="int"/> in <c>PlayerPrefs</c> by key.
        /// </summary>
        [MethodImpl(256)]
        public static void Set(string key, int value, PlayerPrefsEncryption encryption = default) 
            => default(IntPlayerPrefsProvider).Set(key, value, encryption);
        
        /// <summary>
        /// Returns the <see cref="float"/> value stored in <c>PlayerPrefs</c> by key.
        /// </summary>
        [MethodImpl(256)]
        public static float GetFloat(string key, float defaultValue = default, PlayerPrefsEncryption encryption = default) 
            => default(FloatPlayerPrefsProvider).Get(key, defaultValue, encryption);
        
        /// <summary>
        /// Sets the value of <see cref="float"/> in <c>PlayerPrefs</c> by key.
        /// </summary>
        [MethodImpl(256)]
        public static void Set(string key, float value, PlayerPrefsEncryption encryption = default) 
            => default(FloatPlayerPrefsProvider).Set(key, value, encryption);
        
        /// <summary>
        /// Returns the <see cref="bool"/> value stored in <c>PlayerPrefs</c> by key.
        /// </summary>
        [MethodImpl(256)]
        public static bool GetBool(string key, bool defaultValue = default, PlayerPrefsEncryption encryption = default) 
            => default(BoolPlayerPrefsProvider).Get(key, defaultValue, encryption);
        
        /// <summary>
        /// Sets the value of <see cref="bool"/> in <c>PlayerPrefs</c> by key.
        /// </summary>
        [MethodImpl(256)]
        public static void Set(string key, bool value, PlayerPrefsEncryption encryption = default) 
            => default(BoolPlayerPrefsProvider).Set(key, value, encryption);
    }
    
    namespace Providers
    {
        internal readonly struct IntPlayerPrefsProvider : IPlayerPrefsProvider<int>
        {
            public int Get(string key, int defaultValue = default, PlayerPrefsEncryption encryption = default)
            {
                return NiPrefs.Internal.GetInt(key, defaultValue, encryption);
            }

            public void Set(string key, int value, PlayerPrefsEncryption encryption = default)
            {
                NiPrefs.Internal.SetInt(key, value, encryption);
            }
        }
    
        internal readonly struct FloatPlayerPrefsProvider : IPlayerPrefsProvider<float>
        {
            public float Get(string key, float defaultValue = default, PlayerPrefsEncryption encryption = default)
            {
                return NiPrefs.Internal.GetFloat(key, defaultValue, encryption);
            }

            public void Set(string key, float value, PlayerPrefsEncryption encryption = default)
            {
                NiPrefs.Internal.SetFloat(key, value, encryption);
            }
        }
    
        internal readonly struct BoolPlayerPrefsProvider : IPlayerPrefsProvider<bool>
        {
            public bool Get(string key, bool defaultValue = default, PlayerPrefsEncryption encryption = default)
            {
                return NiPrefs.Internal.GetInt(key, defaultValue ? 1 : 0, encryption) != 0;
            }

            public void Set(string key, bool value, PlayerPrefsEncryption encryption = default)
            {
                NiPrefs.Internal.SetInt(key, value ? 1 : 0, encryption);
            }
        }
    }
}