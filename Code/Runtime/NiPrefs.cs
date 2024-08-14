using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace NiGames.PlayerPrefs
{
    /// <summary>
    /// Class for managing Unity PlayerPrefs using various providers for different types.
    /// </summary>
    [PublicAPI]
    public static partial class NiPrefs
    {
        public static bool EnableLogging = true;
        
        private static readonly Dictionary<Type, IPlayerPrefsProvider> _providers = new Dictionary<Type, IPlayerPrefsProvider>(12);
        
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
        /// Returns the value corresponding to key in the preference file if it exists.
        /// Uses one of the available providers.
        /// Returns an error if no provider is found.
        /// </summary>
        /// <remarks>
        /// Use <see cref="SetEnum{T}"/> to write <c>Enum</c>.
        /// To write objects for which there is no <see cref="IPlayerPrefsProvider{T}"/>,
        /// use <see cref="SetObject{T}"/> or some other way of writing.
        /// </remarks>
        public static T Get<T>(string key, T defaultValue = default)
        {
            if (!_providers.ContainsKey(typeof(T)))
            {
                throw new Exception($"[NiPrefs] Provider with type `{typeof(T)}` is not registered.");
            }
            
            if (_providers[typeof(T)] is IPlayerPrefsProvider<T> provider)
            {
                return provider.Get(key, defaultValue);
            }
            
            return defaultValue;
        }
        
        /// <summary>
        /// Sets a value of type <c>T</c> for the preference identified by the given key.
        /// You can use <see cref="Get{T}"/> to get this value
        /// </summary>
        /// <remarks>
        /// Use <see cref="GetEnum{T}"/> to read <c>Enum</c>.
        /// To write objects for which there is no <see cref="IPlayerPrefsProvider{T}"/>,
        /// use <see cref="GetObject{T}"/> or some other way of reading.
        /// </remarks>
        public static void Set<T>(string key, T value, bool throwIdProviderNotFound = true)
        {
            if (!_providers.ContainsKey(typeof(T)))
            {
                if (throwIdProviderNotFound)
                {
                    throw new Exception($"[NiPrefs] Provider with type `{typeof(T)}` is not registered.");
                }
            }
            
            if (_providers[typeof(T)] is IPlayerPrefsProvider<T> provider)
            {
                provider.Set(key, value);
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