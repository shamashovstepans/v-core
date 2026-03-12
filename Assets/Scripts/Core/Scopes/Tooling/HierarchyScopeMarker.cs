using UnityEngine;

namespace Core.Scopes.Tooling
{
    /// <summary>
    /// Marks a GameObject with its scope group for hierarchy coloring in the editor.
    /// Added at runtime when scopes are created.
    /// </summary>
    public class HierarchyScopeMarker : MonoBehaviour
    {
        [SerializeField] private ScopeGroup _group;

        public ScopeGroup Group => _group;

        public static void AddTo(GameObject gameObject, ScopeGroup group)
        {
            var marker = gameObject.GetComponent<HierarchyScopeMarker>();
            if (marker == null)
                marker = gameObject.AddComponent<HierarchyScopeMarker>();

            marker.SetGroup(group);
        }

        internal void SetGroup(ScopeGroup group) => _group = group;
    }
}
