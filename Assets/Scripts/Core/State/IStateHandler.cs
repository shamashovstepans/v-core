namespace Core.State
{
    public interface IStateHandler
    {
        bool Exists(string key);
        void Set<T>(string key, T value);
        T Get<T>(string key, T defaultValue = default);
        void Clear();
        void ClearByPrefix(string prefix);
    }
}
