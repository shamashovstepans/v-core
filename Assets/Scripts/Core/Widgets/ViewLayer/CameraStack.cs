using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Core.Widgets.ViewLayer
{
    internal class CameraStack : ICameraStack
    {
        private readonly List<Camera> _cameras = new();

        public void Register(Camera camera)
        {
            var data = camera.GetUniversalAdditionalCameraData();

            if (_cameras.Count == 0)
            {
                _cameras.Add(camera);
                data.renderType = CameraRenderType.Base;
            }
            else
            {
                _cameras.Add(camera);
                data.renderType = CameraRenderType.Overlay;
                var baseData = _cameras[0].GetUniversalAdditionalCameraData();
                baseData.cameraStack.Add(camera);
            }
        }

        public void Unregister(Camera camera)
        {
            var index = _cameras.IndexOf(camera);
            if (index < 0)
                return;

            var baseCamera = _cameras[0];
            var baseData = baseCamera.GetUniversalAdditionalCameraData();

            if (index == 0)
            {
                _cameras.RemoveAt(0);

                if (_cameras.Count > 0)
                {
                    var newBase = _cameras[0];
                    var newBaseData = newBase.GetUniversalAdditionalCameraData();
                    newBaseData.renderType = CameraRenderType.Base;
                    newBaseData.cameraStack.Clear();

                    for (var i = 1; i < _cameras.Count; i++)
                    {
                        var overlay = _cameras[i];
                        var overlayData = overlay.GetUniversalAdditionalCameraData();
                        overlayData.renderType = CameraRenderType.Overlay;
                        newBaseData.cameraStack.Add(overlay);
                    }
                }
            }
            else
            {
                baseData.cameraStack.Remove(camera);
                _cameras.RemoveAt(index);
            }
        }

        public void Rebuild(IReadOnlyList<Camera> camerasInOrder)
        {
            foreach (var camera in _cameras.ToArray())
                Unregister(camera);

            foreach (var camera in camerasInOrder)
                Register(camera);
        }
    }
}
