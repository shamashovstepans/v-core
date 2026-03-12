using System;
using System.Collections.Generic;
using Core.Scopes;
using Core.Utils;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Core.Widgets.NavButtons
{
    internal class NavButtonsWidgetPresenter : IInitializable, IDisposable
    {
        private const string NavButtonPrefabPath = "Prefabs/NavButtons";

        private readonly IScopeFactory _scopeFactory;
        private readonly INavButtonRegistry _navButtonRegistry;
        private readonly NavButtonsWidgetView _view;

        private readonly Dictionary<string, (IObjectResolver Scope, GameObject Instance)> _buttons = new();
        private readonly CompositeDisposable _subscriptions = new();

        public NavButtonsWidgetPresenter(
            IScopeFactory scopeFactory,
            INavButtonRegistry navButtonRegistry,
            NavButtonsWidgetView view)
        {
            _scopeFactory = scopeFactory;
            _navButtonRegistry = navButtonRegistry;
            _view = view;
        }

        public void Initialize()
        {
            _navButtonRegistry.ButtonRegistered += OnButtonRegistered;
            _navButtonRegistry.ButtonRemoved += OnButtonRemoved;
            _subscriptions.Add(new DisposableToken(() => _navButtonRegistry.ButtonRegistered -= OnButtonRegistered));
            _subscriptions.Add(new DisposableToken(() => _navButtonRegistry.ButtonRemoved -= OnButtonRemoved));

            var buttons = _navButtonRegistry.GetButtons(_view.Group);
            foreach (var (buttonId, installer) in buttons)
                AddButton(buttonId, installer);
        }

        public void Dispose()
        {
            _subscriptions.Dispose();
            foreach (var (scope, instance) in _buttons.Values)
            {
                scope.Dispose();
                Object.Destroy(instance);
            }
            _buttons.Clear();
        }

        private void OnButtonRegistered(string group, string buttonId, IInstaller installer)
        {
            if (group != _view.Group)
                return;
            if (_buttons.TryGetValue(buttonId, out var existing))
            {
                _buttons.Remove(buttonId);
                existing.Scope.Dispose();
                Object.Destroy(existing.Instance);
            }
            AddButton(buttonId, installer);
        }

        private void OnButtonRemoved(string group, string buttonId)
        {
            if (group != _view.Group || !_buttons.TryGetValue(buttonId, out var entry))
                return;
            _buttons.Remove(buttonId);
            entry.Scope.Dispose();
            Object.Destroy(entry.Instance);
        }

        private void AddButton(string buttonId, IInstaller installer)
        {
            var path = $"{NavButtonPrefabPath}/{buttonId}";
            var prefab = Resources.Load<GameObject>(path);
            if (prefab == null)
                return;
            var instance = Object.Instantiate(prefab, _view.Container);
            var buttonView = instance.GetComponent<INavButtonView>();
            var scopeInstaller = new NavButtonScopeInstaller(buttonId, buttonView, installer);
            var scope = _scopeFactory.CreateScope(scopeInstaller);
            _buttons[buttonId] = (scope, instance);
        }
    }
}
