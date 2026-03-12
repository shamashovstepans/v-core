using System;
using VContainer.Unity;

namespace Core.Widgets.Popups
{
    public interface IPopupRegistry
    {
        IDisposable Register(string popupId, IInstaller installer);
        IInstaller Get(string popupId);
    }
}
