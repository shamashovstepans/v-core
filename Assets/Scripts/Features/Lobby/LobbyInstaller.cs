using Core.FeatureManager;
using Core.Scopes;
using Core.Scopes.Tooling;
using Core.Widgets.Screens;
using Features.Lobby.Screen;
using Newtonsoft.Json;
using VContainer;
using VContainer.Unity;

namespace Features.Lobby
{
    internal class LobbyInstaller : IFeatureInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.RegisterScreen<LobbyScreenInstaller>();
            builder.Register<LobbyScreenStarter>(Lifetime.Singleton).AsImplementedInterfaces();
        }

        public string Id => FeatureIds.Lobby;

        public IInstaller GetConfigInstaller(string json)
        {
            var config = JsonConvert.DeserializeObject<LobbyConfig>(json);
            return new ActionInstaller(b => b.RegisterInstance(config).AsSelf().AsImplementedInterfaces());
        }
    }
}
