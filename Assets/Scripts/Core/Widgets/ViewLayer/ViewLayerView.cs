using UnityEngine;

namespace Core.Widgets.ViewLayer
{
    public class ViewLayerView : MonoBehaviour, IWidgetView, ICameraProvider
    {
        [SerializeField] private Camera viewCamera;
        [SerializeField] private Transform contentRoot;

        public Camera Camera => viewCamera;
        public Transform ContentRoot => contentRoot;
    }
}
