using UnityEngine;

namespace Core.Widgets.ViewLayer
{
    public interface ICameraStack
    {
        void Register(Camera camera);
        void Unregister(Camera camera);
    }
}
