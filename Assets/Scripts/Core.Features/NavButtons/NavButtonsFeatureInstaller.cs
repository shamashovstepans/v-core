using VContainer;
using VContainer.Unity;

namespace Core.Widgets.NavButtons
{
    internal class NavButtonsFeatureInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<NavButtonRegistry>(Lifetime.Singleton).As<INavButtonRegistry>();
            builder.RegisterWidget<NavButtonsWidgetInstaller>();
        }
    }
}
