using System;
using Core.FeatureManager;
using Core.Scopes;
using Core.Scopes.Tooling;
using VContainer;
using VContainer.Unity;

namespace Features.Puzzles
{
    internal class PuzzlesInstaller : IFeatureInstaller
    {
        public void Install(IContainerBuilder builder)
        {
        }

        public string Id => FeatureIds.Puzzles;

        public Type StateType => null;

        public IInstaller GetConfigInstaller(string json)
        {
            return new ActionInstaller(_ => { });
        }
    }
}
