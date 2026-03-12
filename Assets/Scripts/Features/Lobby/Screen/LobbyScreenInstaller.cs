using Core.Widgets.Screens;
using VContainer;
using VContainer.Unity;

namespace Features.Lobby.Screen
{
    internal class LobbyScreenInstaller : IScreenInstaller
    {
        public string Id => "lobby";

        public void Install(IContainerBuilder builder)
        {
            builder.Register<LobbyScreenPresenter>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}
