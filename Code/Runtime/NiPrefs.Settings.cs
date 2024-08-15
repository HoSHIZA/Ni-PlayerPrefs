using JetBrains.Annotations;

namespace NiGames.PlayerPrefs
{
    public static partial class NiPrefs
    {
        [PublicAPI]
        public static class Settings
        {
            /// <summary>
            /// Enable Logging.
            /// </summary>
            public static bool EnableLogging = true;
            
            /// <summary>
            /// Enables key encryption on PlayerPrefs.
            /// </summary>
            public static bool EncryptKey;
            
            /// <summary>
            /// Enables value encryption on PlayerPrefs.
            /// </summary>
            public static bool EncryptValue;
            
            /// <summary>
            /// Hash used for encryption. If <c>null</c>, <c>Unique Device Key</c> is used.
            /// </summary>
            public static string EncryptionHash;
        }
    }
}