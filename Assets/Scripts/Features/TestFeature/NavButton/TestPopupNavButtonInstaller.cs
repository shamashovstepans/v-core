using Core.Widgets.NavButtons;
using Core.Widgets.Popups;
using VContainer;

namespace Features.TestFeature.NavButton
{
    internal class TestPopupNavButtonInstaller : INavButtonInstaller
    {
        public string Id => PopupIds.TestPopup;

        public string Group => "left";

        public void Install(IContainerBuilder builder)
        {
            builder.Register<TestPopupNavButtonPresenter>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}
