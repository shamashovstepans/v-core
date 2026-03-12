using Core.Widgets.Popups;
using VContainer;

namespace Features.TestYourLuck.Popup
{
    internal class TestYourLuckPopupInstaller : IPopupInstaller
    {
        private readonly TestYourLuckService _service;

        public TestYourLuckPopupInstaller(TestYourLuckService service)
        {
            _service = service;
        }

        public void Install(IContainerBuilder builder)
        {
            builder.RegisterInstance(_service);
            builder.Register<TestYourLuckPopupPresenter>(Lifetime.Singleton).AsImplementedInterfaces();
        }

        public string Id => PopupIds.TestYourLuck;
    }
}
