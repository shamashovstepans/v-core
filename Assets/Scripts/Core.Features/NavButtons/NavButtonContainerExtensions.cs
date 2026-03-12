using Core.Utils;
using VContainer;

namespace Core.Widgets.NavButtons
{
    public static class NavButtonContainerExtensions
    {
        public static void RegisterNavButton<TInstaller>(this IContainerBuilder builder)
            where TInstaller : INavButtonInstaller
        {
            builder.Register<TInstaller>(Lifetime.Singleton);
            var disposable = new CompositeDisposable();
            builder.RegisterBuildCallback(resolver =>
            {
                var installer = resolver.Resolve<TInstaller>();
                disposable.Add(resolver.Resolve<INavButtonRegistry>().Register(installer.Group, installer.Id, installer));
            });
            builder.RegisterDisposeCallback(_ => disposable.Dispose());
        }
    }
}
