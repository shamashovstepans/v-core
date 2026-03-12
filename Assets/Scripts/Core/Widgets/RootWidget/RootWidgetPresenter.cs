using Core.Widgets.Popups;
using Core.Widgets.Screens;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Core.Widgets.RootWidget
{
    internal class RootWidgetPresenter : IInitializable
    {
        private const string PopupManagerPrefabPath = "Prefabs/popup_manager_widget";
        private const string ScreenManagerPrefabPath = "Prefabs/screen_manager_widget";

        private readonly IWidgetFactory _widgetFactory;
        private readonly RootWidgetView _view;

        public RootWidgetPresenter(IWidgetFactory widgetFactory, RootWidgetView view)
        {
            _widgetFactory = widgetFactory;
            _view = view;
        }

        public void Initialize()
        {
            var screenPrefab = Resources.Load<GameObject>(ScreenManagerPrefabPath);
            var screenInstance = Object.Instantiate(screenPrefab, _view.transform);
            var screenView = screenInstance.GetComponent<ScreenManagerView>();
            _widgetFactory.Attach(WidgetIds.ScreenManager, screenView);

            var popupPrefab = Resources.Load<GameObject>(PopupManagerPrefabPath);
            var popupInstance = Object.Instantiate(popupPrefab, _view.transform);
            var popupView = popupInstance.GetComponent<PopupManagerView>();
            _widgetFactory.Attach(WidgetIds.PopupManager, popupView);

            SetupCameraStack(screenView, popupView);
        }

        private static void SetupCameraStack(ScreenManagerView screenView, PopupManagerView popupView)
        {
            var baseCamera = screenView.CanvasRoot.GetComponent<Canvas>().worldCamera;
            var overlayCamera = popupView.CanvasRoot.GetComponent<Canvas>().worldCamera;

            var baseData = baseCamera.GetUniversalAdditionalCameraData();
            var overlayData = overlayCamera.GetUniversalAdditionalCameraData();
            overlayData.renderType = CameraRenderType.Overlay;

            baseData.cameraStack.Add(overlayCamera);
        }
    }
}
