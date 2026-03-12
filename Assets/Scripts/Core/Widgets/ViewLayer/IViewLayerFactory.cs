using UnityEngine;
using VContainer;

namespace Core.Widgets.ViewLayer
{
    public interface IViewLayerFactory
    {
        IObjectResolver Create(string widgetId, ViewLayerView view);
    }
}
