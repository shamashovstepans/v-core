namespace Core.Widgets.Screens
{
    internal class ScreenManager : IScreenManager
    {
        private readonly ScreenManagerProvider _provider;

        public ScreenManager(ScreenManagerProvider provider)
        {
            _provider = provider;
        }

        public void SwitchScreen(string screenId)
        {
            _provider.ScreenManagerWidget.SwitchScreen(screenId);
        }
    }
}
