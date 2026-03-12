using VContainer;

namespace Core.Widgets.RootWidget
{
    internal class RootWidgetInstaller : IWidgetInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<RootWidgetPresenter>(Lifetime.Singleton).AsImplementedInterfaces();
        }

        public string Id => WidgetIds.Root;
    }
}
