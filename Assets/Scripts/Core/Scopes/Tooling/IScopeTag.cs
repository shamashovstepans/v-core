namespace Core.Scopes.Tooling
{
    public enum ScopeGroup
    {
        General,
        Feature,
        Widget,
        Screen,
        Popup,
        NavButton,
    }

    public interface IScopeTag
    {
        string Name { get; }
        ScopeGroup Group { get; }
        bool IsMain { get; }
    }
}
