namespace Core.Scopes.Tooling
{
    internal class ScopeTag : IScopeTag
    {
        public string Name { get; }
        public ScopeGroup Group { get; }
        public bool IsMain { get; }

        public ScopeTag(string name, bool isMain = false, ScopeGroup group = ScopeGroup.General)
        {
            Name = name;
            Group = group;
            IsMain = isMain;
        }
    }
}
