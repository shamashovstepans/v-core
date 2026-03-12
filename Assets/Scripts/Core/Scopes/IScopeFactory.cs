using VContainer;
using VContainer.Unity;

namespace Core.Scopes
{
    public interface IScopeFactory
    {
        /// <summary>
        ///     Create new child scope. It will be destroyed when parent is destroyed
        /// </summary>
        void AttachScope(IInstaller installer);

        /// <summary>
        ///     Create new child scope. It must be disposed manually
        /// </summary>
        IObjectResolver CreateScope(IInstaller installer);
    }
}