using Core.Widgets.Popups;
using Core.Widgets.Screens;
using UnityEngine;
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
            CreateScreenAndPopupManagers();
        }

        private void CreateScreenAndPopupManagers()
        {
            var screenPrefab = Resources.Load<ScreenManagerView>(ScreenManagerPrefabPath);
            var screenInstance = Object.Instantiate(screenPrefab, _view.ContentRoot);
            screenInstance.name = $"canvas: {WidgetIds.ScreenManager}";
            var screenView = screenInstance.GetComponent<ScreenManagerView>();
            _widgetFactory.Attach(WidgetIds.ScreenManager, screenView);

            var popupPrefab = Resources.Load<PopupManagerView>(PopupManagerPrefabPath);
            var popupInstance = Object.Instantiate(popupPrefab, _view.ContentRoot);
            popupInstance.name = $"canvas: {WidgetIds.PopupManager}";
            var popupView = popupInstance.GetComponent<PopupManagerView>();
            _widgetFactory.Attach(WidgetIds.PopupManager, popupView);
        }
    }
}
