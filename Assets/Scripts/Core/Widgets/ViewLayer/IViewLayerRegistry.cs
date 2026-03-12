using System;
using System.Collections.Generic;

namespace Core.Widgets.ViewLayer
{
    public interface IViewLayerRegistry
    {
        event Action<IViewLayerInstaller> Registered;
        event Action<IViewLayerInstaller> Removed;

        IDisposable Register(IViewLayerInstaller installer);
        IReadOnlyList<IViewLayerInstaller> GetAll();
    }
}
