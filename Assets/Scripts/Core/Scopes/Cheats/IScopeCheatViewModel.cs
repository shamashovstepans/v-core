using System;
using System.Collections.Generic;

namespace Core.Scopes.Cheats
{
    internal interface IScopeCheatViewModel
    {
        IReadOnlyList<CheatAction> Actions { get; }
        IReadOnlyList<ICheatInfo> Infos { get; }
        event Action Changed;
    }
}
