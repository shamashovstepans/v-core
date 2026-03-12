using System;
using System.Collections.Generic;
using Core.Utils;
using VContainer.Unity;

namespace Core.Widgets.Popups
{
    internal class PopupRegistry : IPopupRegistry
    {
        private readonly Dictionary<string, IInstaller> _installers = new();

        public IDisposable Register(string popupId, IInstaller installer)
        {
            _installers[popupId] = installer;
            return new DisposableToken(() => _installers.Remove(popupId));
        }

        public IInstaller Get(string popupId)
        {
            if (_installers.TryGetValue(popupId, out var installer))
                return installer;
            throw new InvalidOperationException($"Popup '{popupId}' is not registered.");
        }
    }
}
