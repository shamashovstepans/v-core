using VContainer;

namespace Core.Widgets
{
    public interface IWidgetFactory
    {
        IObjectResolver Create(string widgetId, IWidgetView view);
        void Attach(string widgetId, IWidgetView view);
    }
}
