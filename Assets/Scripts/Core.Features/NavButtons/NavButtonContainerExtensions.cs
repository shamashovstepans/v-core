using System;
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

        /// <summary>Register a nav button at runtime. Call Dispose on the return value to remove it.</summary>
        public static IDisposable RegisterNavButton(this INavButtonRegistry registry, INavButtonInstaller installer)
        {
            return registry.Register(installer.Group, installer.Id, installer);
        }
    }
}
