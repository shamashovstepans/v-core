using System;
using Core.Widgets.Utils;
using VContainer.Unity;

namespace Features.TestYourLuck.Popup
{
    internal class TestYourLuckPopupPresenter : IInitializable, IDisposable
    {
        private readonly TestYourLuckPopupView _view;
        private readonly ICloseHandler _closeHandler;
        private readonly TestYourLuckService _service;

        public TestYourLuckPopupPresenter(TestYourLuckPopupView view, ICloseHandler closeHandler,
            TestYourLuckService service)
        {
            _view = view;
            _closeHandler = closeHandler;
            _service = service;
        }

        public void Initialize()
        {
            _view.CloseButton.onClick.AddListener(OnClose);
            _view.TestYourLuckButton.onClick.AddListener(OnTestYourLuck);
            _view.ClaimButton.onClick.AddListener(OnClaim);

            _service.PendingDiceResult.Changed += OnPendingStateChanged;
            _service.PendingReward.Changed += OnPendingStateChanged;
            UpdateState();
        }

        public void Dispose()
        {
            _view.CloseButton.onClick.RemoveListener(OnClose);
            _view.TestYourLuckButton.onClick.RemoveListener(OnTestYourLuck);
            _view.ClaimButton.onClick.RemoveListener(OnClaim);
            _service.PendingDiceResult.Changed -= OnPendingStateChanged;
            _service.PendingReward.Changed -= OnPendingStateChanged;
        }

        private void OnClose()
        {
            _closeHandler.Close();
        }

        private void OnTestYourLuck()
        {
            _service.ThrowDice();
        }

        private void OnClaim()
        {
            _service.Claim();
        }

        private void OnPendingStateChanged(int? _)
        {
            UpdateState();
        }

        private void UpdateState()
        {
            var hasPendingReward = _service.PendingReward.Value.HasValue;
            _view.ClaimButton.interactable = hasPendingReward;
            _view.TestYourLuckButton.interactable = !hasPendingReward;

            var diceResult = _service.PendingDiceResult.Value;
            _view.DiceResultLabel.text = diceResult.HasValue ? $"Dice: {diceResult.Value}" : "Dice: —";

            var reward = _service.PendingReward.Value;
            _view.RewardAmountLabel.text = reward.HasValue ? $"Reward: {reward.Value}" : "Reward: —";
        }
    }
}
