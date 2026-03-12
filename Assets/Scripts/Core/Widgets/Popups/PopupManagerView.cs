using Core.Widgets.CanvasWidget;
using UnityEngine;

namespace Core.Widgets.Popups
{
    public class PopupManagerView : CanvasWidgetView
    {
        [SerializeField] private RectTransform popupContainer;

        public RectTransform PopupContainer => popupContainer;
    }
}
