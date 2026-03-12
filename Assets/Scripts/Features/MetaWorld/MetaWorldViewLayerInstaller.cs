using Core.Widgets.ViewLayer;
using VContainer;

namespace Features.MetaWorld
{
    internal class MetaWorldViewLayerInstaller : IViewLayerInstaller
    {
        public string Id => ViewLayerIds.MetaWorld;
        public string PrefabPath => "Prefabs/meta_world_view_layer_widget";

        public void Install(IContainerBuilder builder)
        {
        }
    }
}