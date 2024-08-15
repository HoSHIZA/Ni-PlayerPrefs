using System;
using NiGames.PlayerPrefs.Providers;

namespace NiGames.PlayerPrefs
{
    public static partial class NiPrefs
    {
        /// <summary>
        /// Returns the <see cref="Type"/> value stored in <c>PlayerPrefs</c> by key.
        /// </summary>
        public static Type GetType(string key, Type defaultValue = default, PlayerPrefsEncryption encryption = default) 
            => default(TypePlayerPrefsProvider).Get(key, defaultValue, encryption);
        
        /// <summary>
        /// Sets the value of <see cref="Type"/> in <c>PlayerPrefs</c> by key.
        /// </summary>
        public static void Set(string key, Type value, PlayerPrefsEncryption encryption = default) 
            => default(TypePlayerPrefsProvider).Set(key, value, encryption);
    }
    
    namespace Providers
    {
        internal readonly struct TypePlayerPrefsProvider : IPlayerPrefsProvider<Type>
        {
            public Type Get(string key, Type defaultValue = default, PlayerPrefsEncryption encryption = default)
            {
                var typeName = NiPrefs.Internal.GetString(key, null, encryption);
                
                if (typeName == null) return defaultValue;
                
                return Type.GetType(typeName);
            }
            
            public void Set(string key, Type value, PlayerPrefsEncryption encryption = default)
            {
                NiPrefs.Internal.SetString(key, value.AssemblyQualifiedName, encryption);
            }
        }
    }
}