using Core.Widgets.CanvasWidget;
using VContainer;

namespace Core.Widgets.Popups
{
    internal class PopupManagerWidgetInstaller : CanvasWidgetInstaller
    {
        public override void Install(IContainerBuilder builder)
        {
            builder.Register<PopupManagerWidgetPresenter>(Lifetime.Singleton).AsImplementedInterfaces();
        }

        public override string Id => WidgetIds.PopupManager;
    }
}
