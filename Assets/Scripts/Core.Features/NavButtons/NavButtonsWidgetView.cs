using UnityEngine;

namespace Core.Widgets.NavButtons
{
    public class NavButtonsWidgetView : MonoBehaviour, IWidgetView
    {
        [SerializeField] private RectTransform container;
        [SerializeField] private string group;

        public RectTransform Container => container;
        public string Group => group;
    }
}
