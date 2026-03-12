using Core.Widgets.Screens;
using VContainer.Unity;

namespace Features.Lobby
{
    internal class LobbyScreenStarter : IInitializable
    {
        private readonly IScreenManager _screenManager;

        public LobbyScreenStarter(IScreenManager screenManager)
        {
            _screenManager = screenManager;
        }

        public void Initialize()
        {
            _screenManager.SwitchScreen("lobby");
        }
    }
}
