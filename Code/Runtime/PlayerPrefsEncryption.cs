namespace NiGames.PlayerPrefs
{
    public enum PlayerPrefsEncryption
    {
        /// <summary>
        /// Uses global encryption settings.
        /// </summary>
        UseEncryptionSettings,
        
        /// <summary>
        /// Does not apply encryption.
        /// </summary>
        WithoutEncryption,
        
        /// <summary>
        /// Encrypts the key and value.
        /// </summary>
        WithEncryption,
        
        /// <summary>
        /// Encrypts only the key.
        /// </summary>
        OnlyKeyEncryption,
        
        /// <summary>
        /// Encrypts only the value.
        /// </summary>
        OnlyValueEncryption,
    }
}