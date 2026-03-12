using System.Threading;
using Cysharp.Threading.Tasks;

namespace Core.Widgets.Popups
{
    public interface IPopupManager
    {
        void ShowPopup(string popupId);
        UniTask ShowPopupAsync(string popupId, CancellationToken token = default);
    }
}
