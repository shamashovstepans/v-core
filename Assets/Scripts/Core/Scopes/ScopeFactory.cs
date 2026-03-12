using System;
using Core.Scopes.Cheats;
using Core.Scopes.Tooling;
using Core.Utils;
using Cysharp.Threading.Tasks;
using VContainer;
using VContainer.Unity;

namespace Core.Scopes
{
    internal class ScopeFactory : IScopeFactory, IDisposable
    {
        private readonly IObjectResolver _objectResolver;
        private readonly ScopeNode _scopeNode;
        private readonly ScopeTree _scopeTree;
        private readonly CompositeDisposable _disposables = new();

        public ScopeFactory(
            IObjectResolver objectResolver,
            ScopeNode scopeNode,
            ScopeTree scopeTree)
        {
            _objectResolver = objectResolver;
            _scopeNode = scopeNode;
            _scopeTree = scopeTree;
        }

        public void AttachScope(IInstaller installer)
        {
            var childScope = BuildChildScope(installer);
            _disposables.Add(childScope);
        }

        public IObjectResolver CreateScope(IInstaller installer)
        {
            return BuildChildScope(installer);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }

        private IObjectResolver BuildChildScope(IInstaller installer)
        {
            var childNode = new ScopeNode(_scopeNode);

            var childScope = _objectResolver.CreateScope(builder =>
            {
                builder.ApplicationOrigin = this;
                builder.RegisterInstance(childNode);
                builder.RegisterInstance(_scopeTree);
                builder.Register<ScopeFactory>(Lifetime.Singleton).As<IScopeFactory>().As<IDisposable>();

                builder.Register<ScopeCheatsHandler>(Lifetime.Singleton).AsImplementedInterfaces();
                installer.Install(builder);

                EntryPointsBuilder.EnsureDispatcherRegistered(builder);

                builder.RegisterEntryPointExceptionHandler(ex =>
                {
                    if (ex is not OperationCanceledException)
                    {
                        //log
                    }
                });

                builder.RegisterDisposeCallback(_ =>
                {
                    childNode.Parent.RemoveChild(childNode);
                    _scopeTree.NotifyRemoved(childNode);
                });
            });

            _scopeTree.NotifyRegistered(childNode);

            return childScope;
        }
    }
}
