using System;
using VContainer.Unity;

namespace Core.FeatureManager
{
    public interface IFeatureInstaller : IInstaller
    {
        string Id { get; }

        /// <summary>
        ///     State type for IRepository&lt;TState&gt;. Override to return the type; default is null (no state).
        /// </summary>
        Type StateType => null;

        IInstaller GetConfigInstaller(string json);
    }
}
