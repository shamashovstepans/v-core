using System;
using Core.Widgets.Popups;
using VContainer.Unity;

namespace Features.TestYourLuck.NavButton
{
    internal class TestYourLuckNavButtonPresenter : IInitializable, IDisposable
    {
        private readonly TestYourLuckNavButtonView _view;
        private readonly IPopupManager _popupManager;

        public TestYourLuckNavButtonPresenter(TestYourLuckNavButtonView view, IPopupManager popupManager)
        {
            _view = view;
            _popupManager = popupManager;
        }

        public void Initialize()
        {
            _view.Button.onClick.AddListener(OnClick);
        }

        public void Dispose()
        {
            _view.Button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            _popupManager.ShowPopup(PopupIds.TestYourLuck);
        }
    }
}
