using Core.Widgets.NavButtons;
using VContainer;

namespace Features.TestYourLuck.NavButton
{
    internal class TestYourLuckNavButtonInstaller : INavButtonInstaller
    {
        public string Id => NavButtonIds.TestYourLuck;

        public string Group => "right";

        public void Install(IContainerBuilder builder)
        {
            builder.Register<TestYourLuckNavButtonPresenter>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}
