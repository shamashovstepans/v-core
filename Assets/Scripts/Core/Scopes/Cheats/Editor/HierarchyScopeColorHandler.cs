using Core.Scopes.Tooling;
using UnityEditor;
using UnityEngine;

namespace Core.Scopes.Cheats.Editor
{
    [InitializeOnLoad]
    internal static class HierarchyScopeColorHandler
    {
        private static readonly Color GeneralColor = new(191f / 255f, 194f / 255f, 209f / 255f);
        private static readonly Color FeatureColor = new(102f / 255f, 230f / 255f, 128f / 255f);
        private static readonly Color WidgetColor = new(120f / 255f, 199f / 255f, 219f / 255f);
        private static readonly Color PopupColor = new(240f / 255f, 186f / 255f, 79f / 255f);
        private static readonly Color NavButtonColor = new(180f / 255f, 130f / 255f, 240f / 255f);

        private static readonly Color HierarchyBgDark = new(0.22f, 0.22f, 0.22f, 1f);
        private static readonly Color HierarchyBgLight = new(0.9f, 0.9f, 0.9f, 1f);
        private static readonly Color HierarchySelected = new(0.27f, 0.38f, 0.49f, 1f);

        static HierarchyScopeColorHandler()
        {
            EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyItemGUI;
        }

        private static void OnHierarchyItemGUI(int instanceID, Rect selectionRect)
        {
            if (!Application.isPlaying)
                return;

            var obj = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
            if (obj == null)
                return;

            var marker = obj.GetComponent<HierarchyScopeMarker>();
            if (marker == null)
                return;

            var color = marker.Group switch
            {
                ScopeGroup.General => GeneralColor,
                ScopeGroup.Feature => FeatureColor,
                ScopeGroup.Widget => WidgetColor,
                ScopeGroup.Screen => PopupColor,
                ScopeGroup.Popup => PopupColor,
                ScopeGroup.NavButton => NavButtonColor,
                _ => GeneralColor
            };

            var accentRect = new Rect(selectionRect.x, selectionRect.y, 4, selectionRect.height);
            EditorGUI.DrawRect(accentRect, color);

            var textRect = new Rect(selectionRect.x + 18, selectionRect.y, selectionRect.width - 18, selectionRect.height);
            var bgColor = Selection.Contains(obj)
                ? HierarchySelected
                : (EditorGUIUtility.isProSkin ? HierarchyBgDark : HierarchyBgLight);
            EditorGUI.DrawRect(textRect, bgColor);

            var style = new GUIStyle(EditorStyles.label)
            {
                normal = { textColor = color },
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleLeft,
                fontSize = 11
            };

            EditorGUI.LabelField(textRect, obj.name, style);
        }
    }
}
