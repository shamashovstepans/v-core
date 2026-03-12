using System;
using Core.Scopes.Tooling;
using VContainer.Unity;

namespace Core.Scopes.Cheats
{
    internal class ScopeCheatsHandler : IScopeCheatsHandler, IInitializable, IDisposable
    {
        private readonly ScopeNode _scopeNode;
        private readonly ScopeCheatsRegistry _registry;
        private readonly ScopeCheatViewModel _viewModel = new();

        public ScopeCheatsHandler(ScopeNode scopeNode, ScopeCheatsRegistry registry)
        {
            _scopeNode = scopeNode;
            _registry = registry;
        }

        public IDisposable AddAction(CheatAction action)
        {
            return _viewModel.AddAction(action);
        }

        public IDisposable AddInfo(ICheatInfo info)
        {
            return _viewModel.AddInfo(info);
        }

        public void Initialize()
        {
            _registry.Register(_scopeNode, _viewModel);
        }

        public void Dispose()
        {
            _registry.Unregister(_scopeNode);
        }
    }
}
