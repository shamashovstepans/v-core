using System;
using Core.Widgets.Popups;
using VContainer.Unity;

namespace Features.TestFeature.NavButton
{
    internal class TestPopupNavButtonPresenter : IInitializable, IDisposable
    {
        private readonly TestPopupNavButtonView _view;
        private readonly IPopupManager _popupManager;

        public TestPopupNavButtonPresenter(TestPopupNavButtonView view, IPopupManager popupManager)
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
            _popupManager.ShowPopup(PopupIds.TestPopup);
        }
    }
}
