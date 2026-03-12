using System;
using Core.Utils;
using Core.Widgets.Utils;
using Features.TestFeature;
using VContainer.Unity;

namespace Features.TestFeature.Popup
{
    internal class TestPopupPresenter : IInitializable, IDisposable
    {
        private readonly TestPopupView _view;
        private readonly ICloseHandler _closeHandler;
        private readonly IRepository<TestFeatureState> _repository;

        public TestPopupPresenter(TestPopupView view, ICloseHandler closeHandler,
            IRepository<TestFeatureState> repository)
        {
            _view = view;
            _closeHandler = closeHandler;
            _repository = repository;
        }

        public void Initialize()
        {
            _view.SetCloseHandler(_closeHandler);
            _view.CounterButton.onClick.AddListener(IncrementCounter);
            UpdateCounterLabel();
            _repository.Updated += OnRepositoryUpdated;
        }

        public void Dispose()
        {
            _repository.Updated -= OnRepositoryUpdated;
        }

        private void OnRepositoryUpdated(TestFeatureState state)
        {
            UpdateCounterLabel();
        }

        private void IncrementCounter()
        {
            var state = _repository.Get(new TestFeatureState());
            state.value++;
            _repository.Set(state);
            UpdateCounterLabel();
        }

        private void UpdateCounterLabel()
        {
            var state = _repository.Get(new TestFeatureState());
            _view.CounterLabel.text = $"Count: {state.value}";
        }
    }
}
