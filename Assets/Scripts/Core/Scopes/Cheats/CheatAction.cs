using System;

namespace Core.Scopes.Cheats
{
    public class CheatAction
    {
        public CheatAction(string name, Action callback)
        {
            Name = name;
            Callback = callback;
        }

        public string Name { get; }
        public Action Callback { get; }
    }
}