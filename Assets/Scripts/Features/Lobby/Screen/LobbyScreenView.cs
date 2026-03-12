using Core.Widgets.NavButtons;
using Core.Widgets.Screens;
using UnityEngine;

namespace Features.Lobby.Screen
{
    public class LobbyScreenView : MonoBehaviour, IScreenView
    {
        [SerializeField] private NavButtonsWidgetView leftNavButtons;
        [SerializeField] private NavButtonsWidgetView rightNavButtons;

        public NavButtonsWidgetView LeftNavButtons => leftNavButtons;
        public NavButtonsWidgetView RightNavButtons => rightNavButtons;
    }
}
