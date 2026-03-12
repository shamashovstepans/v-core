using Core.FeatureManager;
using Features.BattlePass;
using Features.Lobby;
using Features.Puzzles;
using Features.TestFeature;
using Features.TestYourLuck;
using VContainer;
using VContainer.Unity;

namespace Features
{
    internal class FeaturesInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<LobbyInstaller>(Lifetime.Singleton).As<IFeatureInstaller>();
            builder.Register<TestFeatureInstaller>(Lifetime.Singleton).As<IFeatureInstaller>();
            builder.Register<BattlePassInstaller>(Lifetime.Singleton).As<IFeatureInstaller>();
            builder.Register<PuzzlesInstaller>(Lifetime.Singleton).As<IFeatureInstaller>();
            builder.Register<TestYourLuckFeatureInstaller>(Lifetime.Singleton).As<IFeatureInstaller>();
        }
    }
}
