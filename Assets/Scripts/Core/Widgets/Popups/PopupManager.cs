using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Core.Widgets.Popups
{
    internal class PopupManager : IPopupManager, IDisposable
    {
        private readonly PopupManagerProvider _provider;
        private readonly CancellationTokenSource _lifetimeToken = new();

        public PopupManager(PopupManagerProvider provider)
        {
            _provider = provider;
        }

        public void ShowPopup(string popupId)
        {
            ShowPopupAsync(popupId, _lifetimeToken.Token).Forget();
        }

        public UniTask ShowPopupAsync(string popupId, CancellationToken token = default)
        {
            return _provider.PopupManagerWidget.ShowPopupAsync(popupId, token);
        }

        public void Dispose()
        {
            _lifetimeToken.Cancel();
            _lifetimeToken.Dispose();
        }
    }
}
