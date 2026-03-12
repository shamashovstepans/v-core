using Core.Widgets.ViewLayer;
using UnityEngine;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Core.Widgets.RootWidget
{
    internal class RootWidgetPresenter : IInitializable
    {
        private const string ViewLayerPrefabPath = "Prefabs/view_layer_widget";

        private readonly IWidgetFactory _widgetFactory;
        private readonly RootWidgetView _view;

        public RootWidgetPresenter(IWidgetFactory widgetFactory, RootWidgetView view)
        {
            _widgetFactory = widgetFactory;
            _view = view;
        }

        public void Initialize()
        {
            var prefab = Resources.Load<GameObject>(ViewLayerPrefabPath);
            var instance = Object.Instantiate(prefab, _view.transform);
            var viewLayerView = instance.GetComponent<ViewLayerView>();
            _widgetFactory.Attach(WidgetIds.ViewLayer, viewLayerView);
        }
    }
}
