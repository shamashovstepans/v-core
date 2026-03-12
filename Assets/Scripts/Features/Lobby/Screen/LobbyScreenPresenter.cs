using Core.Widgets;
using VContainer.Unity;

namespace Features.Lobby.Screen
{
    internal class LobbyScreenPresenter : IInitializable
    {
        private readonly LobbyScreenView _view;
        private readonly IWidgetFactory _widgetFactory;

        public LobbyScreenPresenter(LobbyScreenView view, IWidgetFactory widgetFactory)
        {
            _view = view;
            _widgetFactory = widgetFactory;
        }

        public void Initialize()
        {
            _widgetFactory.Attach(WidgetIds.NavButtons, _view.LeftNavButtons);
            _widgetFactory.Attach(WidgetIds.NavButtons, _view.RightNavButtons);
        }
    }
}
