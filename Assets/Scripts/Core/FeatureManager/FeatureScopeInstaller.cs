using Core.Config;
using Core.Scopes;
using Core.Scopes.Cheats;
using Core.Scopes.Tooling;
using Core.State;
using Core.Utils;
using VContainer;
using VContainer.Unity;

namespace Core.FeatureManager
{
    internal class FeatureScopeInstaller : IInstaller
    {
        private readonly IFeatureInstaller _installer;
        private readonly IConfigProvider _configProvider;

        public FeatureScopeInstaller(IFeatureInstaller installer, IConfigProvider configProvider)
        {
            _installer = installer;
            _configProvider = configProvider;
        }

        public void Install(IContainerBuilder builder)
        {
            var json = _configProvider.GetConfigJson(_installer.Id);
            _installer.GetConfigInstaller(json).Install(builder);

            builder.RegisterMainScopeTag(_installer.Id, ScopeGroup.Feature);

            if (_installer.StateType != null)
            {
                builder.Register<FeatureStateClearer>(Lifetime.Singleton)
                    .WithParameter(_installer.Id);

                var repoType = typeof(FeatureStateRepository<>).MakeGenericType(_installer.StateType);
                var ifaceType = typeof(IRepository<>).MakeGenericType(_installer.StateType);
                builder.Register(repoType, Lifetime.Singleton)
                    .WithParameter(_installer.Id)
                    .As(ifaceType);
            }

            builder.RegisterBuildCallback(resolver =>
            {
                var scopeCheatsHandler = resolver.Resolve<IScopeCheatsHandler>();
                if (_installer.StateType != null)
                {
                    var clearer = resolver.Resolve<FeatureStateClearer>();
                    var cheatAction = new CheatAction("Clear State", () => clearer.Clear());
                    scopeCheatsHandler.AddAction(cheatAction);
                }
            });

            _installer.Install(builder);
        }
    }
}
