using System;

namespace Core.Widgets
{
    public interface IWidgetRegistry
    {
        IDisposable Register(string widgetId, IWidgetInstaller installer);
        IWidgetInstaller Get(string widgetId);
    }
}
