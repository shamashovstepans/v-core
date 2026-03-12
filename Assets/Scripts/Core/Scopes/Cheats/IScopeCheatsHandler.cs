using System;

namespace Core.Scopes.Cheats
{
    /// <summary>
    ///     Injected into scopes that register cheat actions. Use <see cref="ScopesContainerExtensions.RegisterScopeCheats" />
    ///     to enable cheat integration for a scope.
    /// </summary>
    public interface IScopeCheatsHandler
    {
        IDisposable AddAction(CheatAction action);
        IDisposable AddInfo(ICheatInfo info);
    }
}
