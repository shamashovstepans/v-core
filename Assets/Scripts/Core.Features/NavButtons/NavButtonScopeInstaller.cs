using Core.Scopes;
using Core.Scopes.Tooling;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core.Widgets.NavButtons
{
    internal class NavButtonScopeInstaller : IInstaller
    {
        private readonly string _buttonId;
        private readonly INavButtonView _view;
        private readonly IInstaller _installer;

        public NavButtonScopeInstaller(string buttonId, INavButtonView view, IInstaller installer)
        {
            _buttonId = buttonId;
            _view = view;
            _installer = installer;
        }

        public void Install(IContainerBuilder builder)
        {
            builder.RegisterInstance(_view).AsSelf().As<IWidgetView>().As<INavButtonView>();
            builder.RegisterMainScopeTag(_buttonId, ScopeGroup.NavButton);
            builder.RegisterScopeTag($"View: {_view.GetType().Name}", ScopeGroup.General);
            builder.RegisterBuildCallback(_ =>
            {
                if (_view is MonoBehaviour mb)
                    HierarchyScopeMarker.AddTo(mb.gameObject, ScopeGroup.NavButton);
            });
            _installer.Install(builder);
        }
    }
}
