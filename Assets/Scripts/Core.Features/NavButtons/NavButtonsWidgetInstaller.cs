using VContainer;

namespace Core.Widgets.NavButtons
{
    internal class NavButtonsWidgetInstaller : IWidgetInstaller
    {
        public string Id => WidgetIds.NavButtons;

        public void Install(IContainerBuilder builder)
        {
            builder.Register<NavButtonsWidgetPresenter>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}
