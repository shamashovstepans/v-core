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

        private readonly List<IObjectResolver> _scopes = new();
        private readonly List<GameObject> _instances = new();

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
            var buttons = _navButtonRegistry.GetButtons(_view.Group);
            foreach (var (buttonId, installer) in buttons)
            {
                var path = $"{NavButtonPrefabPath}/{buttonId}";
                var prefab = Resources.Load<GameObject>(path);
                var instance = Object.Instantiate(prefab, _view.Container);

                var buttonView = instance.GetComponent<INavButtonView>();
                var scopeInstaller = new NavButtonScopeInstaller(buttonId, buttonView, installer);

                var scope = _scopeFactory.CreateScope(scopeInstaller);
                _scopes.Add(scope);
                _instances.Add(instance);
            }
        }

        public void Dispose()
        {
            for (var i = _instances.Count - 1; i >= 0; i--)
            {
                Object.Destroy(_instances[i]);
                _scopes[i].Dispose();
            }
            _instances.Clear();
            _scopes.Clear();
        }
    }
}
