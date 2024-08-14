using System;
using NiGames.PlayerPrefs.Providers;

namespace NiGames.PlayerPrefs
{
    public static partial class NiPrefs
    {
        /// <summary>
        /// Returns the <see cref="Type"/> value stored in <c>PlayerPrefs</c> by key.
        /// </summary>
        public static Type GetType(string key, Type defaultValue = default) => default(TypePlayerPrefsProvider).Get(key, defaultValue);
        
        /// <summary>
        /// Sets the value of <see cref="Type"/> in <c>PlayerPrefs</c> by key.
        /// </summary>
        public static void Set(string key, Type value) => default(TypePlayerPrefsProvider).Set(key, value);
    }
    
    namespace Providers
    {
        internal readonly struct TypePlayerPrefsProvider : IPlayerPrefsProvider<Type>
        {
            public Type Get(string key, Type defaultValue = default)
            {
                var typeName = UnityEngine.PlayerPrefs.GetString(key, null);
                
                return typeName != null 
                    ? Type.GetType(typeName) 
                    : defaultValue;
            }
            
            public void Set(string key, Type value)
            {
                UnityEngine.PlayerPrefs.SetString(key, value.AssemblyQualifiedName);
            }
        }
    }
}