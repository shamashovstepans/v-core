using Core.Scopes;
using VContainer;

namespace Core.Widgets
{
    internal class WidgetFactory : IWidgetFactory
    {
        private readonly IScopeFactory _scopeFactory;
        private readonly IWidgetRegistry _registry;

        public WidgetFactory(IScopeFactory scopeFactory, IWidgetRegistry registry)
        {
            _scopeFactory = scopeFactory;
            _registry = registry;
        }

        public IObjectResolver Create(string widgetId, IWidgetView view)
        {
            var installer = _registry.Get(widgetId);
            var scopeInstaller = new WidgetScopeInstaller(widgetId, view, installer);
            return _scopeFactory.CreateScope(scopeInstaller);
        }

        public void Attach(string widgetId, IWidgetView view)
        {
            var installer = _registry.Get(widgetId);
            var scopeInstaller = new WidgetScopeInstaller(widgetId, view, installer);
            _scopeFactory.AttachScope(scopeInstaller);
        }
    }
}
