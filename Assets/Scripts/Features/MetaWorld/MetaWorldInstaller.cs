using Core.FeatureManager;
using Core.Widgets.ViewLayer;
using Newtonsoft.Json;
using VContainer;
using VContainer.Unity;

namespace Features.MetaWorld
{
    internal class MetaWorldInstaller : IFeatureInstaller
    {
        public string Id => FeatureIds.MetaWorld;

        public IInstaller GetConfigInstaller(string json)
        {
            var config = JsonConvert.DeserializeObject<MetaWorldConfig>(json);
            return new ActionInstaller(b => b.RegisterInstance(config).AsSelf().AsImplementedInterfaces());
        }

        public void Install(IContainerBuilder builder)
        {
            builder.RegisterViewLayer<MetaWorldViewLayerInstaller>();
        }
    }
}
