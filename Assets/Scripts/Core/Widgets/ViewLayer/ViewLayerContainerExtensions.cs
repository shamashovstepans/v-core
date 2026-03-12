using Core.Utils;
using VContainer;

namespace Core.Widgets.ViewLayer
{
    public static class ViewLayerContainerExtensions
    {
        public static void RegisterViewLayer<TInstaller>(this IContainerBuilder builder)
            where TInstaller : IViewLayerInstaller
        {
            builder.Register<TInstaller>(Lifetime.Singleton);
            var disposable = new CompositeDisposable();
            builder.RegisterBuildCallback(resolver =>
            {
                var installer = resolver.Resolve<TInstaller>();
                disposable.Add(resolver.Resolve<IWidgetRegistry>().Register(installer.Id, installer));
                disposable.Add(resolver.Resolve<IViewLayerRegistry>().Register(installer));
            });
            builder.RegisterDisposeCallback(_ => disposable.Dispose());
        }
    }
}
