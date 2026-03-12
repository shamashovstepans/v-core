using Core.Scopes;
using Core.Scopes.Tooling;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core.Widgets.Screens
{
    internal class ScreenScopeInstaller : IInstaller
    {
        private readonly string _screenId;
        private readonly IScreenView _view;
        private readonly IInstaller _installer;

        public ScreenScopeInstaller(string screenId, IScreenView view, IInstaller installer)
        {
            _screenId = screenId;
            _view = view;
            _installer = installer;
        }

        public void Install(IContainerBuilder builder)
        {
            builder.RegisterInstance(_view).AsSelf().As<IWidgetView>().As<IScreenView>();
            builder.RegisterMainScopeTag(_screenId, ScopeGroup.Screen);
            builder.RegisterScopeTag($"View: {_view.GetType().Name}", ScopeGroup.General);
            builder.RegisterBuildCallback(_ =>
            {
                if (_view is MonoBehaviour mb)
                    HierarchyScopeMarker.AddTo(mb.gameObject, ScopeGroup.Screen);
            });
            _installer.Install(builder);
        }
    }
}
