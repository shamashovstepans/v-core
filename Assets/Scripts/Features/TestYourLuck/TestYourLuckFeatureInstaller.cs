using System;
using Core.FeatureManager;
using Core.Widgets.Popups;
using Features.TestYourLuck.Popup;
using Newtonsoft.Json;
using VContainer;
using VContainer.Unity;

namespace Features.TestYourLuck
{
    internal class TestYourLuckFeatureInstaller : IFeatureInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<TestYourLuckService>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<TestYourLuckCheats>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterPopup<TestYourLuckPopupInstaller>();
        }

        public string Id => FeatureIds.TestYourLuck;

        public Type StateType => typeof(TestYourLuckState);

        public IInstaller GetConfigInstaller(string json)
        {
            var config = JsonConvert.DeserializeObject<TestYourLuckFeatureConfig>(json);
            return new ActionInstaller(builder => builder.RegisterInstance(config).AsSelf());
        }
    }
}
