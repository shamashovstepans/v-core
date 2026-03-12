using VContainer;
using VContainer.Unity;

namespace Core.Utils
{
    public static class ContainerUtils
    {
        public static IContainerBuilder Install<T>(this IContainerBuilder builder) where T : IInstaller, new()
        {
            var installer = new T();
            installer.Install(builder);
            return builder;
        }
    }
}
