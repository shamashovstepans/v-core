using System.Linq;
using Core.Scopes.Tooling;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Core.Scopes.Cheats.Editor
{
    internal class ScopeCheatsEditorWindow : EditorWindow
    {
        private ScopeTreeCheatsTabHandler _handler;
        private ScrollView _scrollView;

        [MenuItem("Window/Core cheats")]
        private static void Open()
        {
            GetWindow<ScopeCheatsEditorWindow>("Core cheats");
        }

        private void OnEnable()
        {
            EditorApplication.playModeStateChanged += OnPlayModeChanged;
            EditorApplication.update += OnUpdate;
            TryAttach();
        }

        private void OnDisable()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeChanged;
            EditorApplication.update -= OnUpdate;
            Detach();
        }

        private void OnUpdate()
        {
            if (!Application.isPlaying)
            {
                if (_handler != null)
                    Detach();
                return;
            }

            if (_handler != null && ScopeTreeCheatsTabHandler.Current != _handler)
            {
                Detach();
                return;
            }

            if (_handler == null && ScopeTreeCheatsTabHandler.Current != null)
                TryAttach();
        }

        private void OnPlayModeChanged(PlayModeStateChange state)
        {
            switch (state)
            {
                case PlayModeStateChange.EnteredPlayMode:
                    EditorApplication.delayCall += TryAttach;
                    break;
                case PlayModeStateChange.ExitingPlayMode:
                    Detach();
                    break;
            }
        }

        private void TryAttach()
        {
            if (!Application.isPlaying)
            {
                if (_handler != null) Detach();
                ShowPlaceholder();
                return;
            }

            if (_handler != null && ScopeTreeCheatsTabHandler.Current != _handler)
            {
                Detach();
            }

            if (_handler != null) return;

            var handler = ScopeTreeCheatsTabHandler.Current;
            if (handler == null)
            {
                ShowPlaceholder();
                return;
            }

            _handler = handler;
            _handler.Changed += RebuildTree;

            rootVisualElement.Clear();

            var styleSheet = Resources.Load<StyleSheet>("ScopeTreeCheatsStyle");
            if (styleSheet != null)
                rootVisualElement.styleSheets.Add(styleSheet);

            rootVisualElement.style.backgroundColor = new Color(0.12f, 0.12f, 0.15f);

            _scrollView = new ScrollView(ScrollViewMode.Vertical);
            _scrollView.AddToClassList("scope-tree-scroll");
            rootVisualElement.Add(_scrollView);

            RebuildTree();
        }

        private void Detach()
        {
            if (_handler == null) return;

            _handler.Changed -= RebuildTree;
            _handler = null;
            _scrollView = null;

            rootVisualElement.Clear();
            ShowPlaceholder();
        }

        private void ShowPlaceholder()
        {
            rootVisualElement.Clear();

            var label = new Label("Enter Play Mode to view Scope Cheats");
            label.style.unityTextAlign = TextAnchor.MiddleCenter;
            label.style.fontSize = 14;
            label.style.color = new Color(0.6f, 0.6f, 0.6f);
            label.style.flexGrow = 1;
            rootVisualElement.Add(label);
        }

        private void RebuildTree()
        {
            if (_scrollView == null || _handler == null) return;
            if (ScopeTreeCheatsTabHandler.Current != _handler)
            {
                Detach();
                return;
            }

            _scrollView.Clear();
            var rootElement = RenderNode(_handler.ScopeTree.Root);
            _scrollView.Add(rootElement);
        }

        private VisualElement RenderNode(ScopeNode node)
        {
            var registry = _handler.CheatsRegistry;
            var mainTag = node.MainTag;
            var typeSuffix = mainTag != null ? TypeSuffix(mainTag.Group) : "general";

            var container = new VisualElement();
            container.AddToClassList("scope-tree-node");
            container.AddToClassList($"scope-tree-panel--{typeSuffix}");

            var titleText = mainTag != null ? mainTag.Name : "(no main tag)";
            var titleLabel = new Label(titleText);
            titleLabel.AddToClassList("scope-tree-node-title");
            titleLabel.AddToClassList($"scope-tree-title--{typeSuffix}");
            container.Add(titleLabel);

            if (node.AdditionalTags.Count > 0)
            {
                var tagsRow = new VisualElement();
                tagsRow.AddToClassList("scope-tree-additional-tags");

                foreach (var tag in node.AdditionalTags)
                {
                    var badge = new Label(tag.Name);
                    badge.AddToClassList("scope-tree-badge");
                    badge.AddToClassList($"scope-tree-badge--{TypeSuffix(tag.Group)}");
                    tagsRow.Add(badge);
                }

                container.Add(tagsRow);
            }

            var viewModel = registry.GetViewModel(node);
            if (viewModel != null && (viewModel.Actions.Count > 0 || viewModel.Infos.Count > 0))
            {
                var cheatsRow = new VisualElement();
                cheatsRow.AddToClassList("scope-tree-cheats");

                foreach (var action in viewModel.Actions)
                {
                    var button = new Button(() => action.Callback?.Invoke()) { text = action.Name };
                    button.AddToClassList("scope-tree-cheat-button");
                    cheatsRow.Add(button);
                }

                foreach (var info in viewModel.Infos)
                {
                    var row = new VisualElement();
                    row.AddToClassList("scope-tree-cheat-info");
                    row.Add(new Label(info.Label));
                    row.Add(new Label(info.DisplayValue));
                    cheatsRow.Add(row);
                }

                container.Add(cheatsRow);
            }

            if (node.Children.Count > 0)
            {
                var childrenContainer = new VisualElement();
                childrenContainer.AddToClassList("scope-tree-children");

                var sortedChildren = node.Children
                    .OrderBy(c => SortOrder(c.MainTag?.Group ?? ScopeGroup.General));

                foreach (var child in sortedChildren)
                {
                    var childElement = RenderNode(child);
                    childrenContainer.Add(childElement);
                }

                container.Add(childrenContainer);
            }

            return container;
        }

        private static string TypeSuffix(ScopeGroup group)
        {
            return group switch
            {
                ScopeGroup.Feature => "feature",
                ScopeGroup.Widget => "widget",
                ScopeGroup.Screen => "screen",
                ScopeGroup.Popup => "popup",
                ScopeGroup.NavButton => "nav-button",
                _ => "general"
            };
        }

        private static int SortOrder(ScopeGroup group)
        {
            return group switch
            {
                ScopeGroup.Feature => 0,
                ScopeGroup.Widget => 1,
                ScopeGroup.Screen => 2,
                ScopeGroup.Popup => 3,
                ScopeGroup.NavButton => 4,
                _ => 5
            };
        }
    }
}
