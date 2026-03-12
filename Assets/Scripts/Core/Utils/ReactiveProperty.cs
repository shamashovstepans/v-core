using System;
using System.Collections.Generic;

namespace Core.Utils
{
    public class ReactiveProperty<T> : IReadonlyReactiveProperty<T>
    {
        private static readonly IEqualityComparer<T> EqualityComparer = EqualityComparer<T>.Default;

        private T _value;
        public event Action<T> Changed;

        public ReactiveProperty(T initialValue = default)
        {
            _value = initialValue;
        }

        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                Changed?.Invoke(_value);
            }
        }

        public static implicit operator T(ReactiveProperty<T> reactiveProperty)
        {
            return reactiveProperty.Value;
        }
    }
}