using System;
using Core.Widgets.RootWidget;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Core.Widgets
{
    internal class WidgetsStarter : IInitializable, IDisposable
    {
        private const string RootWidgetPrefabPath = "Prefabs/root_widget";

        private readonly IWidgetFactory _widgetFactory;

        private GameObject _rootWidget;
        private IObjectResolver _rootWidgetScope;

        public WidgetsStarter(IWidgetFactory widgetFactory)
        {
            _widgetFactory = widgetFactory;
        }

        public void Initialize()
        {
            var prefab = Resources.Load<GameObject>(RootWidgetPrefabPath);
            _rootWidget = Object.Instantiate(prefab);
            var rootView = _rootWidget.GetComponent<RootWidgetView>();
            _rootWidgetScope = _widgetFactory.Create(WidgetIds.Root, rootView);
        }

        public void Dispose()
        {
            _rootWidgetScope.Dispose();
            Object.Destroy(_rootWidget);
        }
    }
}
