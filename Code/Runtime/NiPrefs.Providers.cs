using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NiGames.PlayerPrefs.Providers;
using UnityEngine;

namespace NiGames.PlayerPrefs
{
    public static partial class NiPrefs
    {
        private static readonly Dictionary<Type, IPlayerPrefsProvider> _providers = new Dictionary<Type, IPlayerPrefsProvider>(16);
        
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
        
        [MethodImpl(256)]
        private static void RegisterBuiltInProviders()
        {
            RegisterProvider<string, StringPlayerPrefsProvider>();
            
            RegisterProvider<int, IntPlayerPrefsProvider>();
            RegisterProvider<float, FloatPlayerPrefsProvider>();
            RegisterProvider<bool, BoolPlayerPrefsProvider>();
            
            RegisterProvider<Color, ColorPlayerPrefsProvider>();
            RegisterProvider<Color32, Color32PlayerPrefsProvider>();
            
            RegisterProvider<Vector2, Vector2PlayerPrefsProvider>();
            RegisterProvider<Vector2Int, Vector2IntPlayerPrefsProvider>();
            RegisterProvider<Vector3, Vector3PlayerPrefsProvider>();
            RegisterProvider<Vector3Int, Vector3IntPlayerPrefsProvider>();
            RegisterProvider<Vector4, Vector4PlayerPrefsProvider>();
            
            RegisterProvider<Quaternion, QuaternionPlayerPrefsProvider>();
            
            RegisterProvider<Resolution, ResolutionPlayerPrefsProvider>();
        }
    }
}