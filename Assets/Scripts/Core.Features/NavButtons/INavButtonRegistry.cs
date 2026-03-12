using System;
using System.Collections.Generic;
using VContainer.Unity;

namespace Core.Widgets.NavButtons
{
    public interface INavButtonRegistry
    {
        IDisposable Register(string group, string buttonId, IInstaller installer);
        IReadOnlyList<(string ButtonId, IInstaller Installer)> GetButtons(string group);
    }
}
