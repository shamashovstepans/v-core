using System.Collections.Generic;
using VContainer;
using VContainer.Unity;

namespace Core.Utils
{
    internal class CompositeInstaller : IInstaller
    {
        private readonly List<IInstaller> _installers = new();

        public CompositeInstaller(params IInstaller[] installers)
        {
            _installers.AddRange(installers);
        }

        public void Add(IInstaller installer)
        {
            _installers.Add(installer);
        }

        public void Install(IContainerBuilder builder)
        {
            foreach (var installer in _installers) installer.Install(builder);
        }
    }
}
