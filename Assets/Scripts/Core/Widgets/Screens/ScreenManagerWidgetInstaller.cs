using Core.Widgets.CanvasWidget;
using VContainer;

namespace Core.Widgets.Screens
{
    internal class ScreenManagerWidgetInstaller : CanvasWidgetInstaller
    {
        public override void Install(IContainerBuilder builder)
        {
            builder.Register<ScreenManagerWidgetPresenter>(Lifetime.Singleton).AsImplementedInterfaces();
        }

        public override string Id => WidgetIds.ScreenManager;
    }
}
