using Core.Widgets.NavButtons;
using UnityEngine;
using UnityEngine.UI;

namespace Features.TestYourLuck.NavButton
{
    public class TestYourLuckNavButtonView : MonoBehaviour, INavButtonView
    {
        [SerializeField] private Button button;

        public Button Button => button;
    }
}
