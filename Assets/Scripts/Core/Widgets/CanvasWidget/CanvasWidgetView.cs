using UnityEngine;

namespace Core.Widgets.CanvasWidget
{
    public abstract class CanvasWidgetView : MonoBehaviour, IWidgetView
    {
        [SerializeField] private RectTransform canvasRoot;

        public RectTransform CanvasRoot => canvasRoot;
    }
}
