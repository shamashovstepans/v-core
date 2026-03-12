using System;
using Core.Utils;

namespace Core.Scopes.Cheats
{
    public class CheatInfoString : ICheatInfo
    {
        private readonly IReadonlyReactiveProperty<string> _value;

        public CheatInfoString(string label, IReadonlyReactiveProperty<string> value)
        {
            Label = label;
            _value = value;
            _value.Changed += OnValueChanged;
        }

        public string Label { get; }
        public string DisplayValue => _value.Value;

        public event Action Changed;

        private void OnValueChanged(string _) => Changed?.Invoke();
    }
}
