using System.Collections.Generic;
using Core.Config;
using Newtonsoft.Json;

namespace Core.Widgets.ViewLayer
{
    internal class ViewLayerOrder : IViewLayerOrder
    {
        private const string ConfigKey = "view_layer_order";

        private readonly IReadOnlyList<string> _orderedIds;

        public ViewLayerOrder(IConfigProvider configProvider)
        {
            var json = configProvider.GetConfigJson(ConfigKey);
            var config = JsonConvert.DeserializeObject<ViewLayerOrderConfig>(json);
            _orderedIds = config.Order;
        }

        public IReadOnlyList<string> GetOrderedIds() => _orderedIds;
    }
}
