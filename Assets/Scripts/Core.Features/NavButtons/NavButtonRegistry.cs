using System;
using System.Collections.Generic;
using Core.Utils;
using VContainer.Unity;

namespace Core.Widgets.NavButtons
{
    internal class NavButtonRegistry : INavButtonRegistry
    {
        private readonly Dictionary<string, Dictionary<string, IInstaller>> _installers = new();

        public event Action<string, string, IInstaller> ButtonRegistered;
        public event Action<string, string> ButtonRemoved;

        public IDisposable Register(string group, string buttonId, IInstaller installer)
        {
            if (!_installers.TryGetValue(group, out var groupInstallers))
            {
                groupInstallers = new Dictionary<string, IInstaller>();
                _installers[group] = groupInstallers;
            }
            groupInstallers[buttonId] = installer;
            ButtonRegistered?.Invoke(group, buttonId, installer);
            return new DisposableToken(() =>
            {
                if (_installers.TryGetValue(group, out var installers) && installers.Remove(buttonId))
                    ButtonRemoved?.Invoke(group, buttonId);
            });
        }

        public IReadOnlyList<(string ButtonId, IInstaller Installer)> GetButtons(string group)
        {
            if (!_installers.TryGetValue(group, out var groupInstallers))
                return Array.Empty<(string, IInstaller)>();

            var list = new List<(string, IInstaller)>(groupInstallers.Count);
            foreach (var kvp in groupInstallers)
                list.Add((kvp.Key, kvp.Value));
            return list;
        }
    }
}
