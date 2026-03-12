using System.IO;
using UnityEngine;

namespace Core.Config
{
    internal class ResourcesConfigProvider : IConfigProvider
    {
        private const string CONFIG_PATH = "Configs";

        public string GetConfigJson(string key)
        {
            var path = Path.Combine(CONFIG_PATH, key);
            var json = Resources.Load<TextAsset>(path).text;
            return json;
        }
    }
}
