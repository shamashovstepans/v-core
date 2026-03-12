using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Core.Scopes.Tooling
{
    public class ScopeNode
    {
        private static int _nextId;

        private IScopeTag _mainTag;
        private readonly List<IScopeTag> _additionalTags = new();
        private readonly List<ScopeNode> _children = new();

        public int Id { get; }
        public ScopeNode Parent { get; }

        /// <summary>
        ///     The single main tag that defines this scope (color and primary label). One scope = one main tag.
        /// </summary>
        public IScopeTag MainTag => _mainTag;

        /// <summary>
        ///     Additional tags bound to this scope by other features. Shown as a list inside the scope card.
        /// </summary>
        public IReadOnlyList<IScopeTag> AdditionalTags => _additionalTags;

        /// <summary>
        ///     All tags (main + additional) for backward compatibility.
        /// </summary>
        public IReadOnlyList<IScopeTag> Tags => _mainTag != null
            ? _additionalTags.Prepend(_mainTag).ToList()
            : (IReadOnlyList<IScopeTag>)_additionalTags;

        public IReadOnlyList<ScopeNode> Children => _children;

        public ScopeNode(ScopeNode parent)
        {
            Id = Interlocked.Increment(ref _nextId);
            Parent = parent;
            parent?._children.Add(this);
        }

        internal void AddTag(IScopeTag tag)
        {
            if (tag.IsMain)
            {
                _mainTag = tag;
            }
            else
            {
                _additionalTags.Add(tag);
            }
        }

        internal void RemoveChild(ScopeNode child)
        {
            _children.Remove(child);
        }
    }
}
