using System;
using System.Collections.Generic;

namespace Core.Widgets
{
    internal class WidgetRegistry : IWidgetRegistry
    {
        private readonly Dictionary<string, IWidgetInstaller> _installers = new();

        public IDisposable Register(string widgetId, IWidgetInstaller installer)
        {
            _installers[widgetId] = installer;
            return new UnregisterDisposable(() => _installers.Remove(widgetId));
        }

        public IWidgetInstaller Get(string widgetId) => _installers[widgetId];

        private sealed class UnregisterDisposable : IDisposable
        {
            private readonly Action _onDispose;

            public UnregisterDisposable(Action onDispose) => _onDispose = onDispose;

            public void Dispose() => _onDispose();
        }
    }
}
