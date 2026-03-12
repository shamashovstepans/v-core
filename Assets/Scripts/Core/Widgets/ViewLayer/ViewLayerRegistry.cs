using System;
using System.Collections.Generic;
using Core.Utils;

namespace Core.Widgets.ViewLayer
{
    internal class ViewLayerRegistry : IViewLayerRegistry
    {
        private readonly List<IViewLayerInstaller> _installers = new();

        public event Action<IViewLayerInstaller> Registered;
        public event Action<IViewLayerInstaller> Removed;

        public IDisposable Register(IViewLayerInstaller installer)
        {
            _installers.Add(installer);
            Registered?.Invoke(installer);
            return new DisposableToken(() =>
            {
                _installers.Remove(installer);
                Removed?.Invoke(installer);
            });
        }

        public IReadOnlyList<IViewLayerInstaller> GetAll() => _installers;
    }
}
