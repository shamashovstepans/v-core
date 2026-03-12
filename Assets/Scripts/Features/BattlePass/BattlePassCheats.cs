using System;
using Core.Scopes.Cheats;
using Core.Utils;
using VContainer.Unity;

namespace Features.BattlePass
{
    internal class BattlePassCheats : IInitializable, IDisposable
    {
        private readonly IRepository<BattlePassState> _repository;
        private readonly BattlePassConfig _config;
        private readonly IScopeCheatsHandler _scopeCheatsHandler;
        private readonly CompositeDisposable _disposables = new();
        private readonly ReactiveProperty<int> _levelDisplay = new();
        private readonly ReactiveProperty<int> _xpDisplay = new();

        public BattlePassCheats(BattlePassConfig config, IScopeCheatsHandler scopeCheatsHandler,
            IRepository<BattlePassState> repository)
        {
            _config = config;
            _scopeCheatsHandler = scopeCheatsHandler;
            _repository = repository;
        }

        public void Initialize()
        {
            var state = _repository.Get(new BattlePassState());
            SyncDisplay(state);

            _repository.Updated += OnRepositoryUpdated;

            var addXpAction = new CheatAction("+XP", () => AddXp((int)(_config.xpPerLevel / 3f)));
            var addLevelAction = new CheatAction("+Level", () => AddLevel());
            var levelInfo = new CheatInfoInt("Level", _levelDisplay);
            var xpInfo = new CheatInfoInt("XP", _xpDisplay);

            _disposables.Add(_scopeCheatsHandler.AddAction(addXpAction));
            _disposables.Add(_scopeCheatsHandler.AddAction(addLevelAction));
            _disposables.Add(_scopeCheatsHandler.AddInfo(levelInfo));
            _disposables.Add(_scopeCheatsHandler.AddInfo(xpInfo));
        }

        public void Dispose()
        {
            _repository.Updated -= OnRepositoryUpdated;
            _disposables.Dispose();
        }

        private void AddXp(int amount)
        {
            var state = _repository.Get(new BattlePassState());
            state.xp += amount;
            TryLevelUp(state);
            _repository.Set(state);
        }

        private void AddLevel()
        {
            var state = _repository.Get(new BattlePassState());
            state.level = Math.Min(state.level + 1, _config.maxLevel);
            state.xp = 0;
            _repository.Set(state);
        }

        private void TryLevelUp(BattlePassState state)
        {
            while (state.xp >= _config.xpPerLevel && state.level < _config.maxLevel)
            {
                state.xp -= _config.xpPerLevel;
                state.level++;
            }
        }

        private void OnRepositoryUpdated(BattlePassState state)
        {
            SyncDisplay(state);
        }

        private void SyncDisplay(BattlePassState state)
        {
            _levelDisplay.Value = state.level;
            _xpDisplay.Value = state.xp;
        }
    }
}
