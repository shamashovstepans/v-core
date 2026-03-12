using System;

namespace Core.Utils
{
    public interface IReadonlyReactiveProperty<T>
    {
        event Action<T> Changed;
        T Value { get; }
    }
}