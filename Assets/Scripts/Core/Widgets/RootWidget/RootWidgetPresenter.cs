using System;
using System.Collections.Generic;
using System.Linq;
using Core.Widgets.ViewLayer;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Core.Widgets.RootWidget
{
    internal class RootWidgetPresenter : IInitializable, IDisposable
    {
        private readonly IViewLayerFactory _viewLayerFactory;
        private readonly IViewLayerRegistry _viewLayerRegistry;
        private readonly IViewLayerOrder _viewLayerOrder;
        private readonly ICameraStack _cameraStack;
        private readonly RootWidgetView _view;

        private readonly Dictionary<string, (IObjectResolver Scope, GameObject Instance)> _viewLayers = new();

        public RootWidgetPresenter(IViewLayerFactory viewLayerFactory, IViewLayerRegistry viewLayerRegistry, IViewLayerOrder viewLayerOrder, ICameraStack cameraStack, RootWidgetView view)
        {
            _viewLayerFactory = viewLayerFactory;
            _viewLayerRegistry = viewLayerRegistry;
            _viewLayerOrder = viewLayerOrder;
            _cameraStack = cameraStack;
            _view = view;
        }

        public void Initialize()
        {
            _viewLayerRegistry.Registered += OnViewLayerRegistered;
            _viewLayerRegistry.Removed += OnViewLayerRemoved;

            var orderedIds = _viewLayerOrder.GetOrderedIds();
            var installers = _viewLayerRegistry.GetAll()
                .OrderBy(i => GetOrderIndex(i.Id, orderedIds))
                .ToList();

            foreach (var installer in installers)
                AddViewLayer(installer);

            RebuildCameraStack();
            RebuildSiblingOrder();
        }

        private static int GetOrderIndex(string id, IReadOnlyList<string> orderedIds)
        {
            for (var i = 0; i < orderedIds.Count; i++)
            {
                if (orderedIds[i] == id)
                    return i;
            }
            return orderedIds.Count;
        }

        public void Dispose()
        {
            _viewLayerRegistry.Registered -= OnViewLayerRegistered;
            _viewLayerRegistry.Removed -= OnViewLayerRemoved;

            foreach (var (scope, instance) in _viewLayers.Values)
            {
                if (instance.TryGetComponent<ViewLayerView>(out var viewLayerView))
                    _cameraStack.Unregister(viewLayerView.Camera);
                scope.Dispose();
                Object.Destroy(instance);
            }
            _viewLayers.Clear();
        }

        private void OnViewLayerRegistered(IViewLayerInstaller installer)
        {
            AddViewLayer(installer);
            RebuildCameraStack();
            RebuildSiblingOrder();
        }

        private void OnViewLayerRemoved(IViewLayerInstaller installer)
        {
            if (!_viewLayers.Remove(installer.Id, out var entry))
                return;

            if (entry.Instance.TryGetComponent<ViewLayerView>(out var viewLayerView))
                _cameraStack.Unregister(viewLayerView.Camera);
            entry.Scope.Dispose();
            Object.Destroy(entry.Instance);
            RebuildCameraStack();
            RebuildSiblingOrder();
        }

        private void AddViewLayer(IViewLayerInstaller installer)
        {
            var prefab = Resources.Load<GameObject>(installer.PrefabPath);
            var instance = Object.Instantiate(prefab, _view.transform);
            instance.name = $"view_layer: {installer.Id}";
            var viewLayerView = instance.GetComponent<ViewLayerView>();
            var scope = _viewLayerFactory.Create(installer.Id, viewLayerView);
            _viewLayers[installer.Id] = (scope, instance);
        }

        private void RebuildCameraStack()
        {
            var orderedIds = _viewLayerOrder.GetOrderedIds();
            var cameras = _viewLayers
                .OrderBy(kv => GetOrderIndex(kv.Key, orderedIds))
                .Select(kv => kv.Value.Instance.GetComponent<ViewLayerView>()?.Camera)
                .Where(c => c != null)
                .ToList();
            _cameraStack.Rebuild(cameras);
        }

        private void RebuildSiblingOrder()
        {
            var orderedIds = _viewLayerOrder.GetOrderedIds();
            var ordered = _viewLayers
                .OrderBy(kv => GetOrderIndex(kv.Key, orderedIds))
                .Select(kv => kv.Value.Instance.transform)
                .ToList();

            for (var i = 0; i < ordered.Count; i++)
                ordered[i].SetSiblingIndex(i);
        }
    }
}
