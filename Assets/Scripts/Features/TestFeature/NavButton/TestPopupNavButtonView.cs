using Core.Widgets.NavButtons;
using UnityEngine;
using UnityEngine.UI;

namespace Features.TestFeature.NavButton
{
    public class TestPopupNavButtonView : MonoBehaviour, INavButtonView
    {
        [SerializeField] private Button button;

        public Button Button => button;
    }
}
