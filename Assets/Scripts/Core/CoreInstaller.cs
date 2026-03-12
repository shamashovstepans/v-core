using Core.Config;
using Core.FeatureManager;
using Core.Scopes;
using Core.Scopes.Tooling;
using Core.State;
using Core.Utils;
using Core.Widgets;
using Core.Widgets.Popups;
using Core.Widgets.Screens;
using VContainer;
using VContainer.Unity;

namespace Core
{
    public class CoreInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.RegisterMainScopeTag("app", ScopeGroup.General);
            builder.Install<ScopesInstaller>();
            builder.Install<WidgetsInstaller>();

            builder.Register<ResourcesConfigProvider>(Lifetime.Singleton).As<IConfigProvider>();
            builder.Register<FileSystemStateHandler>(Lifetime.Singleton).As<IStateHandler>();
            builder.Register<PopupManagerProvider>(Lifetime.Singleton);
            builder.Register<PopupManager>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<ScreenManagerProvider>(Lifetime.Singleton);
            builder.Register<ScreenManager>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<FeaturesStarter>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}
