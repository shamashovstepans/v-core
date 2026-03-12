using VContainer.Unity;

namespace Core.Widgets.ViewLayer
{
    public interface IViewLayerInstaller : IWidgetInstaller
    {
        string PrefabPath { get; }
    }
}
