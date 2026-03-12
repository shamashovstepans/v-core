using Core.Widgets.Popups;
using Core.Widgets.Screens;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Core.Widgets.ViewLayer
{
    internal class ViewLayerWidgetPresenter : IInitializable
    {
        private const string PopupManagerPrefabPath = "Prefabs/popup_manager_widget";
        private const string ScreenManagerPrefabPath = "Prefabs/screen_manager_widget";

        private readonly IWidgetFactory _widgetFactory;
        private readonly ViewLayerView _view;

        public ViewLayerWidgetPresenter(IWidgetFactory widgetFactory, ViewLayerView view)
        {
            _widgetFactory = widgetFactory;
            _view = view;
        }

        public void Initialize()
        {
            PushCameraToStack();
            CreateScreenAndPopupManagers();
        }

        private void PushCameraToStack()
        {
            var viewLayerCamera = _view.Camera;
            var viewLayerData = viewLayerCamera.GetUniversalAdditionalCameraData();

            var baseCamera = FindBaseCamera(viewLayerCamera);
            if (baseCamera == null)
            {
                viewLayerData.renderType = CameraRenderType.Base;
            }
            else
            {
                viewLayerData.renderType = CameraRenderType.Overlay;
                var baseData = baseCamera.GetUniversalAdditionalCameraData();
                baseData.cameraStack.Add(viewLayerCamera);
            }
        }

        private static Camera FindBaseCamera(Camera exclude)
        {
            var cameras = Object.FindObjectsByType<Camera>(FindObjectsSortMode.None);
            foreach (var cam in cameras)
            {
                if (cam == exclude)
                    continue;
                if (!cam.TryGetComponent<UniversalAdditionalCameraData>(out var data))
                    continue;
                if (data.renderType == CameraRenderType.Base)
                    return cam;
            }
            return null;
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
