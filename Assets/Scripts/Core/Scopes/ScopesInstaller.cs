using Core.Scopes.Cheats;
using Core.Scopes.Tooling;
using VContainer;
using VContainer.Unity;

namespace Core.Scopes
{
    internal class ScopesInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<ScopeFactory>(Lifetime.Singleton).As<IScopeFactory>();

            var rootNode = new ScopeNode(null);
            rootNode.AddTag(new ScopeTag("Root"));

            builder.RegisterInstance(rootNode);
            builder.Register<ScopeTree>(Lifetime.Singleton).As<IScopeTree>().AsSelf();
            builder.Register<ScopeCheatsRegistry>(Lifetime.Singleton);
            builder.Register<ScopeCheatsHandler>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<ScopeTreeCheatsTabHandler>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}
