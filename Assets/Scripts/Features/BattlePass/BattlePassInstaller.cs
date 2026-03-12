using System;
using Core.FeatureManager;
using Newtonsoft.Json;
using VContainer;
using VContainer.Unity;

namespace Features.BattlePass
{
    internal class BattlePassInstaller : IFeatureInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<BattlePassCheats>(Lifetime.Singleton).AsImplementedInterfaces();
        }

        public string Id => FeatureIds.BattlePass;

        public Type StateType => typeof(BattlePassState);

        public IInstaller GetConfigInstaller(string json)
        {
            var config = JsonConvert.DeserializeObject<BattlePassConfig>(json);
            return new ActionInstaller(builder => builder.RegisterInstance(config).AsSelf().AsImplementedInterfaces());
        }
    }
}
