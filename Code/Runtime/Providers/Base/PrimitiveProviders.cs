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
        public static int GetInt(string key, int defaultValue = default) => default(IntPlayerPrefsProvider).Get(key, defaultValue);
        
        /// <summary>
        /// Sets the value of <see cref="int"/> in <c>PlayerPrefs</c> by key.
        /// </summary>
        [MethodImpl(256)]
        public static void Set(string key, int value) => default(IntPlayerPrefsProvider).Get(key, value);
        
        /// <summary>
        /// Returns the <see cref="float"/> value stored in <c>PlayerPrefs</c> by key.
        /// </summary>
        [MethodImpl(256)]
        public static float GetFloat(string key, float defaultValue = default) => default(FloatPlayerPrefsProvider).Get(key, defaultValue);
        
        /// <summary>
        /// Sets the value of <see cref="float"/> in <c>PlayerPrefs</c> by key.
        /// </summary>
        [MethodImpl(256)]
        public static void Set(string key, float value) => default(FloatPlayerPrefsProvider).Get(key, value);
        
        /// <summary>
        /// Returns the <see cref="bool"/> value stored in <c>PlayerPrefs</c> by key.
        /// </summary>
        [MethodImpl(256)]
        public static bool GetBool(string key, bool defaultValue = default) => default(BoolPlayerPrefsProvider).Get(key, defaultValue);
        
        /// <summary>
        /// Sets the value of <see cref="bool"/> in <c>PlayerPrefs</c> by key.
        /// </summary>
        [MethodImpl(256)]
        public static void Set(string key, bool value) => default(BoolPlayerPrefsProvider).Get(key, value);
    }
}

namespace NiGames.PlayerPrefs.Providers
{
    internal readonly struct IntPlayerPrefsProvider : IPlayerPrefsProvider<int>
    {
        public int Get(string key, int defaultValue = default)
        {
            return UnityEngine.PlayerPrefs.GetInt(key, defaultValue);
        }

        public void Set(string key, int value)
        {
            UnityEngine.PlayerPrefs.SetInt(key, value);
        }
    }
    
    internal readonly struct FloatPlayerPrefsProvider : IPlayerPrefsProvider<float>
    {
        public float Get(string key, float defaultValue = default)
        {
            return UnityEngine.PlayerPrefs.GetFloat(key, defaultValue);
        }

        public void Set(string key, float value)
        {
            UnityEngine.PlayerPrefs.SetFloat(key, value);
        }
    }
    
    internal readonly struct BoolPlayerPrefsProvider : IPlayerPrefsProvider<bool>
    {
        public bool Get(string key, bool defaultValue = default)
        {
            return UnityEngine.PlayerPrefs.GetInt(key, defaultValue ? 1 : 0) != 0;
        }

        public void Set(string key, bool value)
        {
            UnityEngine.PlayerPrefs.SetInt(key, value ? 1 : 0);
        }
    }
}