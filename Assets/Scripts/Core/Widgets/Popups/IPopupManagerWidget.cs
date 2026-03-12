using System.Threading;
using Cysharp.Threading.Tasks;

namespace Core.Widgets.Popups
{
    internal interface IPopupManagerWidget
    {
        UniTask ShowPopupAsync(string popupId, CancellationToken token = default);
    }
}
