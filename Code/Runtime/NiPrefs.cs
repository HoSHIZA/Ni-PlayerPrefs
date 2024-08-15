using System;
using System.Collections.Generic;
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
        private static readonly Dictionary<Type, IPlayerPrefsProvider> _providers = new Dictionary<Type, IPlayerPrefsProvider>(16);
        
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

            Register();
            
            _init = true;
        }
        
        /// <summary>
        /// Adds a new provider for a specific type to the list of available PlayerPrefs providers.
        /// </summary>
        public static void RegisterProvider<T>(IPlayerPrefsProvider<T> provider)
        {
            if (_providers.ContainsKey(typeof(T))) return;
            
            _providers.Add(typeof(T), provider);
        }
        
        /// <summary>
        /// Adds a new provider for a specific type to the list of available PlayerPrefs providers.
        /// </summary>
        public static void RegisterProvider<TKey, T>()
            where T : IPlayerPrefsProvider<TKey>
        {
            if (_providers.ContainsKey(typeof(TKey))) return;
            
            _providers.Add(typeof(TKey), Activator.CreateInstance<T>());
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