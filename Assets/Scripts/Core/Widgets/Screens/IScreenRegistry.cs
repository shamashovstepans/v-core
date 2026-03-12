using System;
using VContainer.Unity;

namespace Core.Widgets.Screens
{
    public interface IScreenRegistry
    {
        IDisposable Register(string screenId, IInstaller installer);
        IInstaller Get(string screenId);
    }
}
