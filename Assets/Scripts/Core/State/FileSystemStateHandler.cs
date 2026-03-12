using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Core.State
{
    internal class FileSystemStateHandler : IStateHandler
    {
        private readonly string _basePath;
        private readonly JsonSerializerSettings _settings = new()
        {
            TypeNameHandling = TypeNameHandling.Auto,
            Formatting = Formatting.None
        };

        public FileSystemStateHandler()
        {
            _basePath = Path.Combine(Application.persistentDataPath, "State");
            Directory.CreateDirectory(_basePath);
        }

        public bool Exists(string key)
        {
            var path = GetFilePath(key);
            return File.Exists(path);
        }

        public void Set<T>(string key, T value)
        {
            var path = GetFilePath(key);
            var json = JsonConvert.SerializeObject(value, _settings);
            File.WriteAllText(path, json);
        }

        public T Get<T>(string key, T defaultValue = default)
        {
            var path = GetFilePath(key);
            if (!File.Exists(path))
                return defaultValue;

            var json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<T>(json, _settings) ?? defaultValue;
        }

        public void Clear()
        {
            ClearByPrefix(string.Empty);
        }

        public void ClearByPrefix(string prefix)
        {
            var safePrefix = string.IsNullOrEmpty(prefix)
                ? string.Empty
                : string.Join("_", prefix.Split(Path.GetInvalidFileNameChars()));

            foreach (var file in Directory.GetFiles(_basePath, "*.json"))
            {
                var fileName = Path.GetFileNameWithoutExtension(file);
                if (string.IsNullOrEmpty(safePrefix) || fileName.StartsWith(safePrefix, StringComparison.Ordinal))
                    File.Delete(file);
            }
        }

        private string GetFilePath(string key)
        {
            var safeKey = string.Join("_", key.Split(Path.GetInvalidFileNameChars()));
            return Path.Combine(_basePath, safeKey + ".json");
        }
    }
}
