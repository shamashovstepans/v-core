using Core.Widgets.NavButtons;
using VContainer;

namespace Features.TestFeature.NavButton
{
    internal class TestPopupNavButtonInstaller : INavButtonInstaller
    {
        public string Id => NavButtonIds.TestPopup;

        public string Group => "left";

        public void Install(IContainerBuilder builder)
        {
            builder.Register<TestPopupNavButtonPresenter>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}
