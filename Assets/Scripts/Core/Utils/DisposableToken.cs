using System;

namespace Core.Utils
{
    public class DisposableToken : IDisposable
    {
        private readonly Action _disposeAction;

        public DisposableToken(Action disposeAction)
        {
            _disposeAction = disposeAction;
        }

        public void Dispose()
        {
            _disposeAction.Invoke();
        }
    }
}
