using System;
using Core.Utils;
using Random = UnityEngine.Random;
using VContainer.Unity;

namespace Features.TestYourLuck
{
    internal class TestYourLuckService : IInitializable, IDisposable
    {
        private readonly IRepository<TestYourLuckState> _repository;
        private readonly TestYourLuckFeatureConfig _config;
        private readonly ReactiveProperty<int?> _pendingDiceResult = new();
        private readonly ReactiveProperty<int?> _pendingReward = new();

        public IReadonlyReactiveProperty<int?> PendingDiceResult => _pendingDiceResult;
        public IReadonlyReactiveProperty<int?> PendingReward => _pendingReward;

        public TestYourLuckService(IRepository<TestYourLuckState> repository, TestYourLuckFeatureConfig config)
        {
            _repository = repository;
            _config = config;
        }

        public void Initialize()
        {
            SyncFromRepository();
            _repository.Updated += OnRepositoryUpdated;
        }

        public void Dispose()
        {
            _repository.Updated -= OnRepositoryUpdated;
        }

        public void ThrowDice()
        {
            var diceFace = Random.Range(1, 7);
            var reward = _config.Rewards[diceFace - 1];
            var state = _repository.Get(new TestYourLuckState());
            state.PendingDiceResult = diceFace;
            state.PendingReward = reward;
            _repository.Set(state);
        }

        public void Claim()
        {
            var state = _repository.Get(new TestYourLuckState());
            if (!state.PendingReward.HasValue)
            {
                throw new Exception("No pending reward");
            }

            state.PendingDiceResult = null;
            state.PendingReward = null;

            _repository.Set(state);
        }

        private void OnRepositoryUpdated(TestYourLuckState state)
        {
            SyncFromRepository();
        }

        private void SyncFromRepository()
        {
            var state = _repository.Get(new TestYourLuckState());
            _pendingDiceResult.Value = state.PendingDiceResult;
            _pendingReward.Value = state.PendingReward;
        }
    }
}
