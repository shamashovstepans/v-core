using System;
using System.Collections.Generic;
using Core.Utils;

namespace Core.Scopes.Cheats
{
    internal class ScopeCheatViewModel : IScopeCheatViewModel
    {
        private readonly List<CheatAction> _actions = new();
        private readonly List<ICheatInfo> _infos = new();
        public event Action Changed;

        public IReadOnlyList<CheatAction> Actions => _actions;
        public IReadOnlyList<ICheatInfo> Infos => _infos;

        public IDisposable AddAction(CheatAction action)
        {
            _actions.Add(action);
            Changed?.Invoke();
            return new DisposableToken(() =>
            {
                _actions.Remove(action);
                Changed?.Invoke();
            });
        }

        public IDisposable AddInfo(ICheatInfo info)
        {
            _infos.Add(info);
            info.Changed += OnInfoChanged;
            Changed?.Invoke();
            return new DisposableToken(() =>
            {
                info.Changed -= OnInfoChanged;
                _infos.Remove(info);
                Changed?.Invoke();
            });
        }

        private void OnInfoChanged() => Changed?.Invoke();
    }
}
