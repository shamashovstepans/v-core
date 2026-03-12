using System;
using Core.Scopes.Cheats;
using Core.Scopes.Tooling;
using Newtonsoft.Json;
using VContainer;
using VContainer.Unity;

namespace Core.Scopes
{
    public static class ScopesContainerExtensions
    {
        /// <summary>
        ///     Binds an additional tag to the scope. Does not affect the main tag or scope color.
        /// </summary>
        public static void RegisterScopeTag(this IContainerBuilder builder, string name, ScopeGroup group)
        {
            builder.RegisterBuildCallback(resolver =>
            {
                resolver.Resolve<ScopeNode>().AddTag(new ScopeTag(name, false, group));
            });
        }

        /// <summary>
        ///     Sets the main tag for the scope. One scope = one main tag; defines color and primary label in the tree.
        /// </summary>
        public static void RegisterMainScopeTag(this IContainerBuilder builder, string name, ScopeGroup group)
        {
            builder.RegisterBuildCallback(resolver =>
            {
                resolver.Resolve<ScopeNode>().AddTag(new ScopeTag(name, true, group));
            });
        }

        /// <summary>
        ///     Adds a tag with serialized args. Shown in the Scope Tree cheat tab.
        /// </summary>
        /// <param name="tagName">Name/label for the tag (e.g. "Arg", "Config", "Params").</param>
        /// <param name="args">Object to serialize and add to the tag.</param>
        public static void RegisterScopeTag(this IContainerBuilder builder, string tagName, object args)
        {
            var serialized = JsonConvert.SerializeObject(args);
            var display = serialized.Length > 80 ? serialized.Substring(0, 80) + "…" : serialized;
            builder.RegisterScopeTag($"{tagName}: {display}", ScopeGroup.General);
        }
    }
}
