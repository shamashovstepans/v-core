using System;

namespace Core.Scopes.Cheats
{
    public interface ICheatInfo
    {
        string Label { get; }
        string DisplayValue { get; }
        event Action Changed;
    }
}
