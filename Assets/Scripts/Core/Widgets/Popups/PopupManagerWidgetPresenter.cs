using System;
using System.Threading;
using Core.Scopes;
using Core.Utils;
using Core.Widgets.Utils;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Core.Widgets.Popups
{
    internal class PopupManagerWidgetPresenter : IInitializable, IDisposable, IPopupManagerWidget
    {
        private const string PopupPrefabPath = "Prefabs/Popups";

        private readonly PopupManagerProvider _provider;
        private readonly IScopeFactory _scopeFactory;
        private readonly IPopupRegistry _popupRegistry;
        private readonly PopupManagerView _view;
        private readonly ICameraProvider _cameraProvider;
        private readonly CancellationTokenSource _lifetimeTokenSource = new();

        public PopupManagerWidgetPresenter(
            PopupManagerProvider provider,
            IScopeFactory scopeFactory,
            IPopupRegistry popupRegistry,
            PopupManagerView view,
            ICameraProvider cameraProvider)
        {
            _provider = provider;
            _scopeFactory = scopeFactory;
            _popupRegistry = popupRegistry;
            _view = view;
            _cameraProvider = cameraProvider;
        }

        public void Initialize()
        {
            var canvas = _view.CanvasRoot.GetComponent<Canvas>();
            canvas.worldCamera = _cameraProvider.Camera;
            canvas.sortingOrder = 1;
            _provider.PopupManagerWidget = this;
        }

        public void Dispose()
        {
            _provider.PopupManagerWidget = null;
            _lifetimeTokenSource.Cancel();
            _lifetimeTokenSource.Dispose();
        }

        public async UniTask ShowPopupAsync(string popupId, CancellationToken token)
        {
            var path = $"{PopupPrefabPath}/{popupId}";
            var prefab = Resources.Load<GameObject>(path);
            var instance = Object.Instantiate(prefab, _view.PopupContainer);
            using var viewDisposing = new DisposableToken(() => Object.Destroy(instance));

            var popupView = instance.GetComponent<IPopupView>();
            var installer = _popupRegistry.Get(popupId);
            var scopeInstaller = new PopupScopeInstaller(popupId, popupView, installer);

            using var scope = _scopeFactory.CreateScope(scopeInstaller);
            var closeHandler = scope.Resolve<CloseHandler>();
            await closeHandler.WaitForCloseAsync(token);
        }
    }
}
