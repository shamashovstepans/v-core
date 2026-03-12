using Core.Widgets.Popups;
using Core.Widgets.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Features.TestFeature.Popup
{
    public class TestPopupView : MonoBehaviour, IPopupView
    {
        [SerializeField] private Button closeButton;
        [SerializeField] private Button counterButton;
        [SerializeField] private TextMeshProUGUI counterLabel;

        public Button CounterButton => counterButton;
        public TextMeshProUGUI CounterLabel => counterLabel;

        public void SetCloseHandler(ICloseHandler closeHandler)
        {
            closeButton.onClick.AddListener(closeHandler.Close);
        }
    }
}
