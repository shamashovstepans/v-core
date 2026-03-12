using Core.Widgets.Popups;
using Core.Widgets.RootWidget;
using Core.Widgets.Screens;
using Core.Widgets.ViewLayer;
using VContainer;
using VContainer.Unity;

namespace Core.Widgets
{
    internal class WidgetsInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<WidgetRegistry>(Lifetime.Singleton).As<IWidgetRegistry>();
            builder.Register<WidgetFactory>(Lifetime.Transient).As<IWidgetFactory>();
            builder.Register<WidgetsStarter>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<PopupRegistry>(Lifetime.Singleton).As<IPopupRegistry>();
            builder.Register<ScreenRegistry>(Lifetime.Singleton).As<IScreenRegistry>();
            builder.Register<ViewLayerRegistry>(Lifetime.Singleton).As<IViewLayerRegistry>();
            builder.Register<CameraStack>(Lifetime.Singleton).As<ICameraStack>();

            builder.RegisterWidget<RootWidgetInstaller>();
            builder.RegisterWidget<ScreenManagerWidgetInstaller>();
            builder.RegisterWidget<PopupManagerWidgetInstaller>();
        }
    }
}
