using Core.Scopes;
using Core.Scopes.Tooling;
using Core.Widgets.Utils;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core.Widgets.Popups
{
    internal class PopupScopeInstaller : IInstaller
    {
        private readonly string _popupId;
        private readonly IPopupView _view;
        private readonly IInstaller _installer;

        public PopupScopeInstaller(string popupId, IPopupView view, IInstaller installer)
        {
            _popupId = popupId;
            _view = view;
            _installer = installer;
        }

        public void Install(IContainerBuilder builder)
        {
            builder.RegisterInstance(_view).AsSelf().As<IWidgetView>().As<IPopupView>();
            builder.Register<CloseHandler>(Lifetime.Singleton).As<ICloseHandler>().AsSelf();
            builder.RegisterMainScopeTag(_popupId, ScopeGroup.Popup);
            builder.RegisterScopeTag($"View: {_view.GetType().Name}", ScopeGroup.General);
            builder.RegisterBuildCallback(_ =>
            {
                if (_view is MonoBehaviour mb)
                    HierarchyScopeMarker.AddTo(mb.gameObject, ScopeGroup.Popup);
            });
            _installer.Install(builder);
        }
    }
}
