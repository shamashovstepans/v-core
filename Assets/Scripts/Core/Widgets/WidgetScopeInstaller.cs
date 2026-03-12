using Core.Scopes;
using Core.Scopes.Cheats;
using Core.Scopes.Tooling;
using VContainer;
using VContainer.Unity;

namespace Core.Widgets
{
    internal class WidgetScopeInstaller : IInstaller
    {
        private readonly string _widgetId;
        private readonly IWidgetView _view;
        private readonly IWidgetInstaller _installer;

        public WidgetScopeInstaller(string widgetId, IWidgetView view, IWidgetInstaller installer)
        {
            _widgetId = widgetId;
            _view = view;
            _installer = installer;
        }

        public void Install(IContainerBuilder builder)
        {
            builder.RegisterInstance(_view).AsSelf().As<IWidgetView>();
            builder.RegisterMainScopeTag(_widgetId, ScopeGroup.Widget);
            builder.RegisterScopeTag($"View: {_view.GetType().Name}", ScopeGroup.General);
            _installer.Install(builder);
        }
    }
}
