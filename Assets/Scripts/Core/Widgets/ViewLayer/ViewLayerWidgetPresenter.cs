using System;
using Core.Widgets.Popups;
using Core.Widgets.Screens;
using UnityEngine;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Core.Widgets.ViewLayer
{
    internal class ViewLayerWidgetPresenter : IInitializable, IDisposable
    {
        private const string PopupManagerPrefabPath = "Prefabs/popup_manager_widget";
        private const string ScreenManagerPrefabPath = "Prefabs/screen_manager_widget";

        private readonly ICameraStack _cameraStack;
        private readonly IWidgetFactory _widgetFactory;
        private readonly ViewLayerView _view;

        public ViewLayerWidgetPresenter(ICameraStack cameraStack, IWidgetFactory widgetFactory, ViewLayerView view)
        {
            _cameraStack = cameraStack;
            _widgetFactory = widgetFactory;
            _view = view;
        }

        public void Initialize()
        {
            _cameraStack.Register(_view.Camera);
            CreateScreenAndPopupManagers();
        }

        public void Dispose()
        {
            _cameraStack.Unregister(_view.Camera);
        }

        private void CreateScreenAndPopupManagers()
        {
            var screenPrefab = Resources.Load<ScreenManagerView>(ScreenManagerPrefabPath);
            var screenInstance = Object.Instantiate(screenPrefab, _view.ContentRoot);
            var screenView = screenInstance.GetComponent<ScreenManagerView>();
            _widgetFactory.Attach(WidgetIds.ScreenManager, screenView);

            var popupPrefab = Resources.Load<PopupManagerView>(PopupManagerPrefabPath);
            var popupInstance = Object.Instantiate(popupPrefab, _view.ContentRoot);
            var popupView = popupInstance.GetComponent<PopupManagerView>();
            _widgetFactory.Attach(WidgetIds.PopupManager, popupView);
        }
    }
}
