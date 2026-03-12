using System;

namespace Core.Scopes.Tooling
{
    internal class ScopeTree : IScopeTree
    {
        public ScopeNode Root { get; }

        public event Action<ScopeNode> ScopeRegistered;
        public event Action<ScopeNode> ScopeRemoved;

        public ScopeTree(ScopeNode root)
        {
            Root = root;
        }

        internal void NotifyRegistered(ScopeNode node)
        {
            ScopeRegistered?.Invoke(node);
        }

        internal void NotifyRemoved(ScopeNode node)
        {
            ScopeRemoved?.Invoke(node);
        }
    }
}
