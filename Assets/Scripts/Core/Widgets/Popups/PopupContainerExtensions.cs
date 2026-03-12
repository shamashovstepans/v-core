using System.Linq;
using Core.Scopes.Cheats;
using Core.Utils;
using VContainer;

namespace Core.Widgets.Popups
{
    public static class PopupContainerExtensions
    {
        public static void RegisterPopup<TInstaller>(this IContainerBuilder builder)
            where TInstaller : IPopupInstaller
        {
            builder.Register<TInstaller>(Lifetime.Transient);
            var disposable = new CompositeDisposable();
            builder.RegisterBuildCallback(resolver =>
            {
                var installer = resolver.Resolve<TInstaller>();
                disposable.Add(resolver.Resolve<IPopupRegistry>().Register(installer.Id, installer));

                var popupManager = resolver.Resolve<IPopupManager>();
                var scopeCheatsHandler = resolver.Resolve<IScopeCheatsHandler>();
                var cheatName = "Show " + FormatPopupId(installer.Id);
                var cheatAction = new CheatAction(cheatName, () => popupManager.ShowPopup(installer.Id));
                disposable.Add(scopeCheatsHandler.AddAction(cheatAction));
            });
            builder.RegisterDisposeCallback(_ => disposable.Dispose());
        }

        private static string FormatPopupId(string popupId)
        {
            return string.Join(" ", popupId.Split('_').Select(s =>
                s.Length > 0 ? char.ToUpperInvariant(s[0]) + s.Substring(1).ToLowerInvariant() : s));
        }
    }
}
