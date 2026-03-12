using UnityEngine;

namespace Core.Widgets.RootWidget
{
    public class RootWidgetView : MonoBehaviour, IWidgetView
    {
        [SerializeField] private float _viewLayerOffsetX = 10f;

        public float ViewLayerOffsetX => _viewLayerOffsetX;
    }
}
