using System;
using System.Collections.Generic;
using Core.Widgets.ViewLayer;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Core.Widgets.RootWidget
{
    internal class RootWidgetPresenter : IInitializable, IDisposable
    {
        private readonly IWidgetFactory _widgetFactory;
        private readonly IViewLayerRegistry _viewLayerRegistry;
        private readonly RootWidgetView _view;

        private readonly Dictionary<string, (IObjectResolver Scope, GameObject Instance)> _viewLayers = new();

        public RootWidgetPresenter(IWidgetFactory widgetFactory, IViewLayerRegistry viewLayerRegistry, RootWidgetView view)
        {
            _widgetFactory = widgetFactory;
            _viewLayerRegistry = viewLayerRegistry;
            _view = view;
        }

        public void Initialize()
        {
            _viewLayerRegistry.Registered += OnViewLayerRegistered;
            _viewLayerRegistry.Removed += OnViewLayerRemoved;

            foreach (var installer in _viewLayerRegistry.GetAll())
                AddViewLayer(installer);
        }

        public void Dispose()
        {
            _viewLayerRegistry.Registered -= OnViewLayerRegistered;
            _viewLayerRegistry.Removed -= OnViewLayerRemoved;

            foreach (var (scope, instance) in _viewLayers.Values)
            {
                scope.Dispose();
                Object.Destroy(instance);
            }
            _viewLayers.Clear();
        }

        private void OnViewLayerRegistered(IViewLayerInstaller installer)
        {
            AddViewLayer(installer);
        }

        private void OnViewLayerRemoved(IViewLayerInstaller installer)
        {
            if (!_viewLayers.Remove(installer.Id, out var entry))
                return;

            entry.Scope.Dispose();
            Object.Destroy(entry.Instance);
        }

        private void AddViewLayer(IViewLayerInstaller installer)
        {
            var prefab = Resources.Load<GameObject>(installer.PrefabPath);
            var instance = Object.Instantiate(prefab, _view.transform);
            var viewLayerView = instance.GetComponent<ViewLayerView>();
            var scope = _widgetFactory.Create(installer.Id, viewLayerView);
            _viewLayers[installer.Id] = (scope, instance);
        }
    }
}
