using System.Threading;
using Cysharp.Threading.Tasks;

namespace Core.Widgets.Utils
{
    internal class CloseHandler : ICloseHandler
    {
        private readonly UniTaskCompletionSource _tcs = new();

        public void Close()
        {
            _tcs.TrySetResult();
        }

        public UniTask WaitForCloseAsync(CancellationToken cancellationToken)
        {
            return _tcs.Task.AttachExternalCancellation(cancellationToken);
        }
    }
}
