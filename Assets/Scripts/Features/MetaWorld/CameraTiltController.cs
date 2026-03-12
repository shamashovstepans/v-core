using UnityEngine;

namespace Features.MetaWorld
{
    public class CameraTiltController : MonoBehaviour
    {
        [SerializeField] private float sensitivity = 0.02f;
        [SerializeField] private float maxAngleFromCenter = 30f;
        [SerializeField] private float returnSpeed = 3f;

        private Quaternion _centerRotation;
        private Vector3 _dragStartEuler;
        private bool _isDragging;

        private void Awake()
        {
            _centerRotation = transform.localRotation;
        }

        public void BeginDrag()
        {
            _isDragging = true;
            _dragStartEuler = transform.localEulerAngles;
        }

        public void EndDrag()
        {
            _isDragging = false;
        }

        public void ApplyDragFromStart(Vector2 startScreenPos, Vector2 currentScreenPos)
        {
            var delta = (currentScreenPos - startScreenPos) * sensitivity;
            var pitch = NormalizeAngle(_dragStartEuler.x) + delta.y;
            var yaw = NormalizeAngle(_dragStartEuler.y) - delta.x;
            var newRot = Quaternion.Euler(pitch, yaw, _dragStartEuler.z);

            var angle = Quaternion.Angle(newRot, _centerRotation);
            if (angle > maxAngleFromCenter)
                newRot = Quaternion.Slerp(_centerRotation, newRot, maxAngleFromCenter / angle);

            transform.localRotation = newRot;
        }

        private void Update()
        {
            if (!_isDragging && Quaternion.Angle(transform.localRotation, _centerRotation) > 0.1f)
            {
                transform.localRotation = Quaternion.Slerp(transform.localRotation, _centerRotation, returnSpeed * Time.deltaTime);
            }
        }

        private static float NormalizeAngle(float a)
        {
            if (a > 180f) a -= 360f;
            return a;
        }
    }
}
