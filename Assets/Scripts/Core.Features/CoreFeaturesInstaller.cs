using Core.Utils;
using Core.Widgets.NavButtons;
using VContainer;
using VContainer.Unity;

namespace Core.Features
{
    public class CoreFeaturesInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Install<NavButtonsFeatureInstaller>();
        }
    }
}
