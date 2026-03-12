using System;
using System.Collections.Generic;
using Core.Scopes.Tooling;

namespace Core.Scopes.Cheats
{
    internal class ScopeCheatsRegistry
    {
        private readonly Dictionary<int, IScopeCheatViewModel> _viewModels = new();
        private readonly Dictionary<string, IScopeCheatViewModel> _byMainTag = new();

        public event Action Changed;

        public void Register(ScopeNode node, IScopeCheatViewModel viewModel)
        {
            _viewModels[node.Id] = viewModel;
            var mainTagName = node.MainTag?.Name;
            if (mainTagName != null)
            {
                _byMainTag[mainTagName] = viewModel;
            }

            viewModel.Changed += OnViewModelChanged;
            Changed?.Invoke();
        }

        public void Unregister(ScopeNode node)
        {
            if (_viewModels.Remove(node.Id, out var viewModel))
            {
                var mainTagName = node.MainTag?.Name;
                if (mainTagName != null && _byMainTag.GetValueOrDefault(mainTagName) == viewModel)
                {
                    _byMainTag.Remove(mainTagName);
                }

                viewModel.Changed -= OnViewModelChanged;
            }

            Changed?.Invoke();
        }

        public IScopeCheatViewModel GetViewModel(ScopeNode node)
        {
            return _viewModels.GetValueOrDefault(node.Id);
        }

        public IScopeCheatViewModel GetViewModelByMainTag(string mainTagName)
        {
            return _byMainTag.GetValueOrDefault(mainTagName);
        }

        private void OnViewModelChanged()
        {
            Changed?.Invoke();
        }
    }
}
