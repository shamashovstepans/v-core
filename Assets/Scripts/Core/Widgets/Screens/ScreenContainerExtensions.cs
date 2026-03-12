using Core.Utils;
using VContainer;

namespace Core.Widgets.Screens
{
    public static class ScreenContainerExtensions
    {
        public static void RegisterScreen<TInstaller>(this IContainerBuilder builder)
            where TInstaller : IScreenInstaller
        {
            builder.Register<TInstaller>(Lifetime.Singleton);
            var disposable = new CompositeDisposable();
            builder.RegisterBuildCallback(resolver =>
            {
                var installer = resolver.Resolve<TInstaller>();
                disposable.Add(resolver.Resolve<IScreenRegistry>().Register(installer.Id, installer));
            });
            builder.RegisterDisposeCallback(_ => disposable.Dispose());
        }
    }
}
