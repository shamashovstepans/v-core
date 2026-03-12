using System;
using Core.Scopes;
using Core.Scopes.Cheats;
using Core.Scopes.Tooling;
using UnityEngine;
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
            var registration = builder.RegisterInstance(_view).AsSelf().As<IWidgetView>();
            foreach (var iface in _view.GetType().GetInterfaces())
            {
                if (iface == typeof(IWidgetView))
                    continue;
                if (iface.Assembly == typeof(UnityEngine.Object).Assembly)
                    continue;
                registration = registration.As(iface);
            }
            builder.RegisterMainScopeTag(_widgetId, ScopeGroup.Widget);
            builder.RegisterScopeTag($"View: {_view.GetType().Name}", ScopeGroup.General);
            builder.RegisterBuildCallback(_ =>
            {
                if (_view is MonoBehaviour mb)
                    HierarchyScopeMarker.AddTo(mb.gameObject, ScopeGroup.Widget);
            });
            _installer.Install(builder);
        }
    }
}
