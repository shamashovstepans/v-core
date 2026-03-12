using VContainer.Unity;

namespace Core.Widgets.NavButtons
{
    public interface INavButtonInstaller : IInstaller
    {
        string Id { get; }
        string Group { get; }
    }
}
