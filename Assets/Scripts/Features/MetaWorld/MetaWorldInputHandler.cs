using Core.Widgets.ViewLayer;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Features.MetaWorld
{
    public class MetaWorldInputHandler : MonoBehaviour
    {
        [SerializeField] private ViewLayerView viewLayerView;
        [SerializeField] private LayerMask raycastLayers = -1;

        private CameraTiltController _cameraTilt;
        private bool _isDragging;
        private Vector2 _dragStartPosition;

        private void Awake()
        {
            if (viewLayerView == null)
                viewLayerView = GetComponent<ViewLayerView>();
            _cameraTilt = GetComponentInChildren<CameraTiltController>();
        }

        private void Update()
        {
            var camera = viewLayerView?.Camera;
            if (camera == null || _cameraTilt == null)
                return;

            if (GetPointerDown(out var pos))
            {
                if (IsOverUI(pos))
                    return;

                if (RaycastBunny(camera, pos, out var bunny))
                {
                    bunny.Bounce();
                }
                else if (RaycastEnvironment(camera, pos))
                {
                    _isDragging = true;
                    _dragStartPosition = pos;
                    _cameraTilt.BeginDrag();
                }
            }
            else if (_isDragging && GetPointerPosition(out pos))
            {
                _cameraTilt.ApplyDragFromStart(_dragStartPosition, pos);
            }

            if (GetPointerUp())
            {
                _isDragging = false;
                _cameraTilt.EndDrag();
            }
        }

        private bool RaycastBunny(Camera cam, Vector2 screenPos, out BunnyBounce bunny)
        {
            bunny = null;
            var ray = cam.ScreenPointToRay(screenPos);
            if (!Physics.Raycast(ray, out var hit, 1000f, raycastLayers))
                return false;

            bunny = hit.collider.GetComponentInParent<BunnyBounce>();
            return bunny != null;
        }

        private bool RaycastEnvironment(Camera cam, Vector2 screenPos)
        {
            var ray = cam.ScreenPointToRay(screenPos);
            return Physics.Raycast(ray, 1000f, raycastLayers);
        }

        private static bool GetPointerDown(out Vector2 pos)
        {
            var mouse = Mouse.current;
            if (mouse != null && mouse.leftButton.wasPressedThisFrame)
            {
                pos = mouse.position.ReadValue();
                return true;
            }
            var touch = Touchscreen.current;
            if (touch != null && touch.primaryTouch.press.wasPressedThisFrame)
            {
                pos = touch.primaryTouch.position.ReadValue();
                return true;
            }
            pos = default;
            return false;
        }

        private static bool GetPointerPosition(out Vector2 pos)
        {
            var mouse = Mouse.current;
            if (mouse != null && mouse.leftButton.isPressed)
            {
                pos = mouse.position.ReadValue();
                return true;
            }
            var touch = Touchscreen.current;
            if (touch != null && touch.primaryTouch.press.isPressed)
            {
                pos = touch.primaryTouch.position.ReadValue();
                return true;
            }
            pos = default;
            return false;
        }

        private static bool GetPointerUp()
        {
            var mouse = Mouse.current;
            if (mouse != null && mouse.leftButton.wasReleasedThisFrame)
                return true;
            var touch = Touchscreen.current;
            if (touch != null && touch.primaryTouch.press.wasReleasedThisFrame)
                return true;
            return false;
        }

        private static bool IsOverUI(Vector2 screenPosition)
        {
            if (EventSystem.current == null)
                return false;
            return EventSystem.current.IsPointerOverGameObject();
        }
    }
}
