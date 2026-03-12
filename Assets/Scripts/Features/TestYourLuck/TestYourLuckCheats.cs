using System;
using Core.Scopes.Cheats;
using Core.Utils;
using VContainer.Unity;

namespace Features.TestYourLuck
{
    internal class TestYourLuckCheats : IInitializable, IDisposable
    {
        private readonly TestYourLuckService _service;
        private readonly IRepository<TestYourLuckState> _repository;
        private readonly IScopeCheatsHandler _cheatsHandler;

        private readonly CompositeDisposable _disposables = new();
        private readonly ReactiveProperty<int> _pendingReward = new();

        public TestYourLuckCheats(
            IScopeCheatsHandler cheatsHandler, TestYourLuckService service, IRepository<TestYourLuckState> repository)
        {
            _cheatsHandler = cheatsHandler;
            _service = service;
            _repository = repository;
        }

        public void Initialize()
        {
            OnRepositoryUpdated(_repository.Get(new TestYourLuckState()));
            _repository.Updated += OnRepositoryUpdated;

            var throwDiceAction = new CheatAction("throw dice", ThrowDice);
            var rewardInfo = new CheatInfoInt("reward", _pendingReward);
            var claimAction = new CheatAction("claim", Claim);

            _disposables.Add(_cheatsHandler.AddAction(claimAction));
            _disposables.Add(_cheatsHandler.AddAction(throwDiceAction));
            _disposables.Add(_cheatsHandler.AddInfo(rewardInfo));
        }

        public void Dispose()
        {
            _repository.Updated -= OnRepositoryUpdated;

            _disposables.Dispose();
        }

        private void OnRepositoryUpdated(TestYourLuckState obj)
        {
            _pendingReward.Value = obj.PendingReward ?? -1;
        }

        private void Claim()
        {
            _service.Claim();
        }

        private void ThrowDice()
        {
            _service.ThrowDice();
        }
    }
}
