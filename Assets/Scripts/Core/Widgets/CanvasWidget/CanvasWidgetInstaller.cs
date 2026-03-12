using VContainer;

namespace Core.Widgets.CanvasWidget
{
    public abstract class CanvasWidgetInstaller : IWidgetInstaller
    {
        public abstract void Install(IContainerBuilder builder);
        public abstract string Id { get; }
    }
}
