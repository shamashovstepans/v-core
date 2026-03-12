using Core.Utils;
using Core.Widgets.Popups;
using VContainer;

namespace Features.TestFeature.Popup
{
    internal class TestPopupInstaller : IPopupInstaller
    {
        public string Id => PopupIds.TestPopup;

        private readonly IRepository<TestFeatureState> _repository;

        public TestPopupInstaller(IRepository<TestFeatureState> repository)
        {
            _repository = repository;
        }

        public void Install(IContainerBuilder builder)
        {
            builder.RegisterInstance(_repository);
            builder.Register<TestPopupPresenter>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}
