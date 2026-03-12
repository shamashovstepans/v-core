using System;
using Core.Scopes.Cheats;
using Core.Utils;
using VContainer.Unity;

namespace Features.TestFeature
{
    internal class TestFeatureCheats : IInitializable, IDisposable
    {
        private readonly IRepository<TestFeatureState> _repository;
        private readonly TestFeatureConfig _config;
        private readonly IScopeCheatsHandler _scopeCheatsHandler;
        private readonly CompositeDisposable _disposables = new();
        private readonly ReactiveProperty<int> _value = new();

        public TestFeatureCheats(TestFeatureConfig config, IScopeCheatsHandler scopeCheatsHandler,
            IRepository<TestFeatureState> repository)
        {
            _config = config;
            _scopeCheatsHandler = scopeCheatsHandler;
            _repository = repository;
        }

        public void Initialize()
        {
            var cheatAction = new CheatAction("test", () =>
            {
                var state = _repository.Get(new TestFeatureState());
                state.value++;
                _repository.Set(state);
            });

            var state = _repository.Get(new TestFeatureState());
            _value.Value = state.value;
            _repository.Updated += OnRepositoryUpdated;


            var cheatInfo = new CheatInfoInt("test_info", _value);
            _disposables.Add(_scopeCheatsHandler.AddAction(cheatAction));
            _disposables.Add(_scopeCheatsHandler.AddInfo(cheatInfo));
        }

        public void Dispose()
        {
            _repository.Updated -= OnRepositoryUpdated;
            _disposables.Dispose();
        }

        private void OnRepositoryUpdated(TestFeatureState obj)
        {
            _value.Value = obj.value;
        }
    }
}
