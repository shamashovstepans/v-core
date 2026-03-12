using UnityEngine;

namespace Features.MetaWorld
{
    [RequireComponent(typeof(Collider))]
    public class BunnyBounce : MonoBehaviour
    {
        [SerializeField] private float bounceHeight = 0.5f;
        [SerializeField] private float bounceDuration = 0.25f;

        private bool _isBouncing;

        public void Bounce()
        {
            if (_isBouncing)
                return;
            StartCoroutine(AnimatedBounce());
        }

        private System.Collections.IEnumerator AnimatedBounce()
        {
            _isBouncing = true;
            var startPos = transform.position;
            var elapsed = 0f;

            while (elapsed < bounceDuration)
            {
                elapsed += Time.deltaTime;
                var t = elapsed / bounceDuration;
                var height = Mathf.Sin(t * Mathf.PI);
                transform.position = startPos + transform.up * (height * bounceHeight);
                yield return null;
            }

            transform.position = startPos;
            _isBouncing = false;
        }
    }
}
