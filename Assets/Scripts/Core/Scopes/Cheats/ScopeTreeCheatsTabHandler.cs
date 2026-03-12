using System;
using Core.Scopes.Tooling;
using VContainer.Unity;

namespace Core.Scopes.Cheats
{
    internal class ScopeTreeCheatsTabHandler : IInitializable, IDisposable
    {
        public static ScopeTreeCheatsTabHandler Current { get; private set; }

        private readonly IScopeTree _scopeTree;
        private readonly ScopeCheatsRegistry _scopeCheatsRegistry;

        public IScopeTree ScopeTree => _scopeTree;
        public ScopeCheatsRegistry CheatsRegistry => _scopeCheatsRegistry;

        public event Action Changed;

        public ScopeTreeCheatsTabHandler(
            IScopeTree scopeTree,
            ScopeCheatsRegistry scopeCheatsRegistry)
        {
            _scopeTree = scopeTree;
            _scopeCheatsRegistry = scopeCheatsRegistry;
        }

        public void Initialize()
        {
            Current = this;
            _scopeTree.ScopeRegistered += OnScopeChanged;
            _scopeTree.ScopeRemoved += OnScopeChanged;
            _scopeCheatsRegistry.Changed += OnRegistryChanged;
        }

        public void Dispose()
        {
            if (Current == this) Current = null;

            _scopeTree.ScopeRegistered -= OnScopeChanged;
            _scopeTree.ScopeRemoved -= OnScopeChanged;
            _scopeCheatsRegistry.Changed -= OnRegistryChanged;
        }

        private void OnScopeChanged(ScopeNode _) => Changed?.Invoke();
        private void OnRegistryChanged() => Changed?.Invoke();
    }
}
