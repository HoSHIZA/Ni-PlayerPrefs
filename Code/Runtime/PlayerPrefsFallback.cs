namespace NiGames.PlayerPrefs
{
    public enum PlayerPrefsFallback
    {
        /// <summary>
        /// Throw Exception if no provider is found.
        /// </summary>
        Throw,
        
        /// <summary>
        /// Ignores no provider and returns the specified <c>defaultValue</c>.
        /// </summary>
        Ignore,
        
        /// <summary>
        /// Attempt to serialize/deserialize to/from json.
        /// </summary>
        TryJson,
        
        /// <summary>
        /// Attempt to serialize/deserialize to/from binary.
        /// </summary>
        TryBinary,
    }
}