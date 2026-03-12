using Core.Utils;
using VContainer;

namespace Core.Widgets
{
    public static class WidgetContainerExtensions
    {
        public static void RegisterWidget<TInstaller>(this IContainerBuilder builder)
            where TInstaller : IWidgetInstaller
        {
            builder.Register<TInstaller>(Lifetime.Singleton);
            var disposable = new CompositeDisposable();
            builder.RegisterBuildCallback(resolver =>
            {
                var installer = resolver.Resolve<TInstaller>();
                disposable.Add(resolver.Resolve<IWidgetRegistry>().Register(installer.Id, installer));
            });
            builder.RegisterDisposeCallback(_ => disposable.Dispose());
        }
    }
}
