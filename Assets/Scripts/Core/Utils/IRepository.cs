using System;

namespace Core.Utils
{
    /// <summary>
    ///     Repository for a feature's single state. Each feature has one state, keyed by feature id.
    /// </summary>
    public interface IRepository<TState>
    {
        event Action<TState> Updated;
        TState Get(TState defaultState);
        void Set(TState state);
        void Clear();
    }
}
