using System;
using JetBrains.Annotations;
using UnityEngine;

namespace NiGames.PlayerPrefs
{
    /// <summary>
    /// Class for managing Unity PlayerPrefs using various providers for different types.
    /// </summary>
    [PublicAPI]
    public static partial class NiPrefs
    {
        private static bool _init;
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void Init()
        {
#if UNITY_EDITOR
            var domainReloadDisabled =
                UnityEditor.EditorSettings.enterPlayModeOptionsEnabled &&
                (UnityEditor.EditorSettings.enterPlayModeOptions & UnityEditor.EnterPlayModeOptions.DisableDomainReload) != 0;

            if (_init && !domainReloadDisabled) return;
#else
            if (_init) return;
#endif

            RegisterBuiltInProviders();
            
            _init = true;
        }
        
        /// <summary>
        /// Returns the value corresponding to the PlayerPrefs key, using the appropriate provider.
        /// Returns an error if no provider is found.
        /// </summary>
        /// <remarks>
        /// Use <see cref="GetEnum{T}"/> to read <c>Enum</c>.
        /// </remarks>
        public static T Get<T>(string key, T defaultValue = default, 
            PlayerPrefsEncryption encryption = PlayerPrefsEncryption.UseEncryptionSettings, 
            PlayerPrefsFallback fallback = PlayerPrefsFallback.Throw)
        {
            if (!_providers.ContainsKey(typeof(T)))
            {
                switch (fallback)
                {
                    case PlayerPrefsFallback.Throw:
                        throw new Exception($"[NiPrefs] Provider with type `{typeof(T)}` is not registered.");
                    case PlayerPrefsFallback.Ignore:
                        return defaultValue;
                    case PlayerPrefsFallback.TryJson:
                        return GetJson(key, defaultValue, encryption);
                    case PlayerPrefsFallback.TryBinary:
                        return GetBinary(key, defaultValue, encryption);
                }
            }
            
            if (_providers[typeof(T)] is IPlayerPrefsProvider<T> provider)
            {
                return provider.Get(key, defaultValue, encryption);
            }
            
            return defaultValue;
        }
        
        /// <summary>
        /// Sets the value corresponding to the PlayerPrefs key using the appropriate provider.
        /// Returns an error if no provider is found.
        /// </summary>
        /// <remarks>
        /// Use <see cref="SetEnum{T}"/> to write <c>Enum</c>.
        /// </remarks>
        public static void Set<T>(string key, T value, 
            PlayerPrefsEncryption encryption = PlayerPrefsEncryption.UseEncryptionSettings, 
            PlayerPrefsFallback fallback = PlayerPrefsFallback.Throw)
        {
            if (!_providers.ContainsKey(typeof(T)))
            {
                switch (fallback)
                {
                    case PlayerPrefsFallback.Throw:
                        throw new Exception($"[NiPrefs] Provider with type `{typeof(T)}` is not registered.");
                    case PlayerPrefsFallback.Ignore:
                        return;
                    case PlayerPrefsFallback.TryJson:
                        SetJson(key, value, encryption);
                        break;
                    case PlayerPrefsFallback.TryBinary:
                        SetBinary(key, value, encryption);
                        break;
                }
            }
            
            if (_providers[typeof(T)] is IPlayerPrefsProvider<T> provider)
            {
                provider.Set(key, value, encryption);
            }
        }
        
        /// <summary>
        /// Parsing the PlayerPrefs entry type.
        /// </summary>
        public static PlayerPrefsType ParseKeyType(string key)
        {
            if (UnityEngine.PlayerPrefs.HasKey(key)) return PlayerPrefsType.Invalid;
            
            if (IsString(key, false))  return PlayerPrefsType.String;
            if (IsFloat(key, false))   return PlayerPrefsType.Float;
            if (IsInt(key, false))     return PlayerPrefsType.Int;
            
            return PlayerPrefsType.Invalid;
        }

        /// <summary>
        /// Checks the type of PlayerPrefs entry.
        /// </summary>
        public static bool IsString(string key, bool checkKeyExists = true)
        {
            return (!checkKeyExists || UnityEngine.PlayerPrefs.HasKey(key)) && !(
                UnityEngine.PlayerPrefs.GetString(key, defaultValue: null) == null &&
                UnityEngine.PlayerPrefs.GetString(key, defaultValue: string.Empty) == string.Empty
            );
        }

        /// <summary>
        /// Checks the type of PlayerPrefs entry.
        /// </summary>
        public static bool IsFloat(string key, bool checkKeyExists = true)
        {
            return (!checkKeyExists || UnityEngine.PlayerPrefs.HasKey(key)) && !(
                Mathf.Approximately(UnityEngine.PlayerPrefs.GetFloat(key, defaultValue: -1f), -1f) &&
                Mathf.Approximately(UnityEngine.PlayerPrefs.GetFloat(key, defaultValue: 1f), 1f)
            );
        }

        /// <summary>
        /// Checks the type of PlayerPrefs entry.
        /// </summary>
        public static bool IsInt(string key, bool checkKeyExists = true)
        {
            return (!checkKeyExists || UnityEngine.PlayerPrefs.HasKey(key)) && !(
                UnityEngine.PlayerPrefs.GetInt(key, defaultValue: -1) == -1 &&
                UnityEngine.PlayerPrefs.GetInt(key, defaultValue: 1) == 1
            );
        }

        /// <inheritdoc cref="UnityEngine.PlayerPrefs.Save"/>
        public static void Save()
        {
            UnityEngine.PlayerPrefs.Save();
        }
        
        /// <inheritdoc cref="UnityEngine.PlayerPrefs.DeleteAll"/>
        public static void DeleteAll()
        {
            UnityEngine.PlayerPrefs.DeleteAll();
        }
        
        /// <inheritdoc cref="UnityEngine.PlayerPrefs.DeleteKey"/>
        public static void DeleteKey(string key)
        {
            UnityEngine.PlayerPrefs.DeleteKey(key);
        }
        
        /// <inheritdoc cref="UnityEngine.PlayerPrefs.HasKey"/>
        public static bool HasKey(string key)
        {
            return UnityEngine.PlayerPrefs.HasKey(key);
        }
    }
}