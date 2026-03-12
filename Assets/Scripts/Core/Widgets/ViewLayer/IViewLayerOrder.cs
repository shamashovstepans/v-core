using System.Collections.Generic;

namespace Core.Widgets.ViewLayer
{
    public interface IViewLayerOrder
    {
        /// <summary>
        /// Returns view layer ids in stack order (first = base camera, rest = overlay).
        /// </summary>
        IReadOnlyList<string> GetOrderedIds();
    }
}
