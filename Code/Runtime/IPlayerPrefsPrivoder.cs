namespace NiGames.PlayerPrefs
{
    public interface IPlayerPrefsProvider
    {
    }
    
    public interface IPlayerPrefsProvider<T> : IPlayerPrefsProvider
    {
        T Get(string key, T defaultValue = default, PlayerPrefsEncryption encryption = default);
        void Set(string key, T value, PlayerPrefsEncryption encryption = default);
    }
}