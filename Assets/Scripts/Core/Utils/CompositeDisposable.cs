using System;
using System.Collections.Generic;

namespace Core.Utils
{
    public sealed class CompositeDisposable : IDisposable
    {
        private readonly List<IDisposable> _disposables = new();

        public void Dispose()
        {
            foreach (var d in _disposables) d.Dispose();
        }

        public void Add(IDisposable disposable)
        {
            _disposables.Add(disposable);
        }

        public void Remove(IDisposable disposable)
        {
            _disposables.Remove(disposable);
        }
    }
}
