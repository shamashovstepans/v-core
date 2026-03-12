using System.Collections.Generic;
using Core.Config;
using Core.Scopes;
using VContainer.Unity;

namespace Core.FeatureManager
{
    internal class FeaturesStarter : IInitializable
    {
        private readonly IScopeFactory _scopeFactory;
        private readonly IConfigProvider _configProvider;
        private readonly IReadOnlyList<IFeatureInstaller> _installers;

        public FeaturesStarter(
            IScopeFactory scopeFactory,
            IConfigProvider configProvider,
            IReadOnlyList<IFeatureInstaller> installers)
        {
            _scopeFactory = scopeFactory;
            _configProvider = configProvider;
            _installers = installers;
        }

        public void Initialize()
        {
            foreach (var installer in _installers)
            {
                _scopeFactory.AttachScope(new FeatureScopeInstaller(installer, _configProvider));
            }
        }
    }
}
