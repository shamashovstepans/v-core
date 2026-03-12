using UnityEngine;
using VContainer;

namespace Core.Widgets.ViewLayer
{
    internal class ViewLayerFactory : IViewLayerFactory
    {
        private readonly IWidgetFactory _widgetFactory;
        private readonly ICameraStack _cameraStack;

        public ViewLayerFactory(IWidgetFactory widgetFactory, ICameraStack cameraStack)
        {
            _widgetFactory = widgetFactory;
            _cameraStack = cameraStack;
        }

        public IObjectResolver Create(string widgetId, ViewLayerView view)
        {
            var scope = _widgetFactory.Create(widgetId, view);
            _cameraStack.Register(view.Camera);
            return scope;
        }
    }
}
