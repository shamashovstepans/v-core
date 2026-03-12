using System.Collections.Generic;
using UnityEngine;

namespace Core.Widgets.ViewLayer
{
    public interface ICameraStack
    {
        void Register(Camera camera);
        void Unregister(Camera camera);
        void Rebuild(IReadOnlyList<Camera> camerasInOrder);
    }
}
