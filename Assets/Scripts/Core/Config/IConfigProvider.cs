namespace Core.Config
{
    public interface IConfigProvider
    {
        string GetConfigJson(string key);
    }
}
