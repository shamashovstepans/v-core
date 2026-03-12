using Core.Widgets.Popups;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Features.TestYourLuck.Popup
{
    internal class TestYourLuckPopupView : MonoBehaviour, IPopupView
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _testYourLuckButton;
        [SerializeField] private Button _claim;
        [SerializeField] private TextMeshProUGUI _diceResultLabel;
        [SerializeField] private TextMeshProUGUI _rewardAmountLabel;

        public Button CloseButton => _closeButton;
        public Button TestYourLuckButton => _testYourLuckButton;
        public Button ClaimButton => _claim;
        public TextMeshProUGUI DiceResultLabel => _diceResultLabel;
        public TextMeshProUGUI RewardAmountLabel => _rewardAmountLabel;
    }
}
