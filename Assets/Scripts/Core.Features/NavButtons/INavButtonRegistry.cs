using System;
using System.Collections.Generic;
using VContainer.Unity;

namespace Core.Widgets.NavButtons
{
    public interface INavButtonRegistry
    {
        IDisposable Register(string group, string buttonId, IInstaller installer);
        IReadOnlyList<(string ButtonId, IInstaller Installer)> GetButtons(string group);

        /// <summary>Raised when a nav button is registered. Args: (group, buttonId, installer)</summary>
        event Action<string, string, IInstaller> ButtonRegistered;

        /// <summary>Raised when a nav button is unregistered. Args: (group, buttonId)</summary>
        event Action<string, string> ButtonRemoved;
    }
}
