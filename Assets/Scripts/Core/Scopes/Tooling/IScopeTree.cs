using System;

namespace Core.Scopes.Tooling
{
    public interface IScopeTree
    {
        ScopeNode Root { get; }
        event Action<ScopeNode> ScopeRegistered;
        event Action<ScopeNode> ScopeRemoved;
    }
}
