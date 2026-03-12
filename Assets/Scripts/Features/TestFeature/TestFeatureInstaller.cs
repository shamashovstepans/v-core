using System;
using Core.FeatureManager;
using Core.Scopes;
using Core.Scopes.Tooling;
using Core.Widgets.NavButtons;
using Core.Widgets.Popups;
using Features.TestFeature.NavButton;
using Features.TestFeature.Popup;
using Newtonsoft.Json;
using VContainer;
using VContainer.Unity;

namespace Features.TestFeature
{
    internal class TestFeatureInstaller : IFeatureInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<TestFeatureCheats>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterPopup<TestPopupInstaller>();
            builder.RegisterNavButton<TestPopupNavButtonInstaller>();
        }

        public string Id => FeatureIds.TestFeature;

        public Type StateType => typeof(TestFeatureState);

        public IInstaller GetConfigInstaller(string json)
        {
            var config = JsonConvert.DeserializeObject<TestFeatureConfig>(json);
            return new ActionInstaller(builder => builder.RegisterInstance(config).AsSelf().AsImplementedInterfaces());
        }
    }
}
