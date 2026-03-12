using VContainer.Unity;

namespace Core.Widgets.Screens
{
    public interface IScreenInstaller : IInstaller
    {
        string Id { get; }
    }
}
