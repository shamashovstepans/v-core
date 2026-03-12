using System;
using Core.Utils;

namespace Core.State
{
    internal class FeatureStateRepository<TState> : IRepository<TState>
    {
        private const string StateKey = "state";

        private readonly IStateHandler _stateHandler;
        private readonly string _keyPrefix;

        public FeatureStateRepository(IStateHandler stateHandler, string featureId, FeatureStateClearer clearer)
        {
            _stateHandler = stateHandler;
            _keyPrefix = featureId + "_";
            clearer.Cleared += () =>
                Updated?.Invoke((TState)System.Activator.CreateInstance(typeof(TState))!);
        }

        public TState Get(TState defaultState)
        {
            var key = MakeKey();
            if (!_stateHandler.Exists(key))
            {
                _stateHandler.Set(key, defaultState);
                return defaultState;
            }
            return _stateHandler.Get(key, defaultState);
        }

        public void Set(TState state)
        {
            _stateHandler.Set(MakeKey(), state);
            Updated?.Invoke(state);
        }

        public void Clear()
        {
            _stateHandler.ClearByPrefix(_keyPrefix);
        }

        public event Action<TState> Updated;

        private string MakeKey() => _keyPrefix + StateKey;
    }
}
