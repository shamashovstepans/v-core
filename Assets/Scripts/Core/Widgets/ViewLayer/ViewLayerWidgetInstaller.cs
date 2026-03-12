using VContainer;

namespace Core.Widgets.ViewLayer
{
    internal class ViewLayerWidgetInstaller : IWidgetInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<ViewLayerWidgetPresenter>(Lifetime.Singleton).AsImplementedInterfaces();
        }

        public string Id => WidgetIds.ViewLayer;
    }
}
