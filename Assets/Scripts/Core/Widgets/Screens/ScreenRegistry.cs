using System;
using System.Collections.Generic;
using Core.Utils;
using VContainer.Unity;

namespace Core.Widgets.Screens
{
    internal class ScreenRegistry : IScreenRegistry
    {
        private readonly Dictionary<string, IInstaller> _installers = new();

        public IDisposable Register(string screenId, IInstaller installer)
        {
            _installers[screenId] = installer;
            return new DisposableToken(() => _installers.Remove(screenId));
        }

        public IInstaller Get(string screenId)
        {
            if (_installers.TryGetValue(screenId, out var installer))
                return installer;
            throw new InvalidOperationException($"Screen '{screenId}' is not registered.");
        }
    }
}
