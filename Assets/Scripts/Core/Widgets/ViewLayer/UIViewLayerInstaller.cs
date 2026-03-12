using Core.Widgets.Popups;
using Core.Widgets.Screens;
using VContainer;

namespace Core.Widgets.ViewLayer
{
    internal class UIViewLayerInstaller : IViewLayerInstaller
    {
        public string Id => ViewLayerIds.UI;
        public string PrefabPath => "Prefabs/view_layer_widget";

        public void Install(IContainerBuilder builder)
        {
            builder.Register<ViewLayerWidgetPresenter>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}
