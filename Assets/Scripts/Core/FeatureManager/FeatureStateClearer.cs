using System;

namespace Core.State
{
    internal class FeatureStateClearer
    {
        private readonly IStateHandler _stateHandler;
        private readonly string _keyPrefix;

        public FeatureStateClearer(IStateHandler stateHandler, string featureId)
        {
            _stateHandler = stateHandler;
            _keyPrefix = featureId + "_";
        }

        public event Action Cleared;

        public void Clear()
        {
            _stateHandler.ClearByPrefix(_keyPrefix);
            Cleared?.Invoke();
        }
    }
}
