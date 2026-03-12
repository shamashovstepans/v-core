using System;
using Core.Scopes;
using Core.Widgets;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Core.Widgets.Screens
{
    internal class ScreenManagerWidgetPresenter : IInitializable, IDisposable, IScreenManagerWidget
    {
        private const string ScreenPrefabPath = "Prefabs/Screens";

        private readonly ScreenManagerProvider _provider;
        private readonly IScopeFactory _scopeFactory;
        private readonly IScreenRegistry _screenRegistry;
        private readonly ScreenManagerView _view;

        private IObjectResolver _currentScope;
        private GameObject _currentInstance;

        public ScreenManagerWidgetPresenter(
            ScreenManagerProvider provider,
            IScopeFactory scopeFactory,
            IScreenRegistry screenRegistry,
            ScreenManagerView view)
        {
            _provider = provider;
            _scopeFactory = scopeFactory;
            _screenRegistry = screenRegistry;
            _view = view;
        }

        public void Initialize()
        {
            _provider.ScreenManagerWidget = this;
        }

        public void Dispose()
        {
            _provider.ScreenManagerWidget = null;
            ClearCurrentScreen();
        }

        public void SwitchScreen(string screenId)
        {
            ClearCurrentScreen();

            var path = $"{ScreenPrefabPath}/{screenId}";
            var prefab = Resources.Load<GameObject>(path);
            _currentInstance = Object.Instantiate(prefab, _view.ScreenContainer);

            var screenView = _currentInstance.GetComponent<IScreenView>();
            var installer = _screenRegistry.Get(screenId);
            var scopeInstaller = new ScreenScopeInstaller(screenId, screenView, installer);

            _currentScope = _scopeFactory.CreateScope(scopeInstaller);
        }

        private void ClearCurrentScreen()
        {
            if (_currentScope == null)
                return;

            Object.Destroy(_currentInstance);
            _currentScope.Dispose();
            _currentScope = null;
            _currentInstance = null;
        }
    }
}
