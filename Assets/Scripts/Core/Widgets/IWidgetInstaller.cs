using VContainer.Unity;

namespace Core.Widgets
{
    public interface IWidgetInstaller : IInstaller
    {
        string Id { get; }
    }
}
