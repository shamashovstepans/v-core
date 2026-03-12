using System;
using Core.Utils;

namespace Core.Scopes.Cheats
{
    public class CheatInfoInt : ICheatInfo
    {
        private readonly IReadonlyReactiveProperty<int> _value;

        public CheatInfoInt(string label, IReadonlyReactiveProperty<int> value)
        {
            Label = label;
            _value = value;
            _value.Changed += OnValueChanged;
        }

        public string Label { get; }
        public string DisplayValue => _value.Value.ToString();

        public event Action Changed;

        private void OnValueChanged(int _) => Changed?.Invoke();
    }
}
