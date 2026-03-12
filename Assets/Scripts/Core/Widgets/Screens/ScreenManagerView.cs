using Core.Widgets.CanvasWidget;
using UnityEngine;

namespace Core.Widgets.Screens
{
    public class ScreenManagerView : CanvasWidgetView
    {
        [SerializeField] private RectTransform screenContainer;

        public RectTransform ScreenContainer => screenContainer;
    }
}
