using UnityEngine;

namespace PavloSuhak.Cameras
{
    [RequireComponent(typeof(Camera))]
    public class CameraFollow2D : MonoBehaviour
    {

        [SerializeField] private Transform target = null;

        [Tooltip("")] public Vector2 Offset = Vector2.zero;

        [SerializeField, Tooltip("")] private SpriteRenderer _fitSprite = null;

        [SerializeField] private bool followY = true;
        
        private Camera _camera = null;

        private (Vector3 min, Vector3 max) _bounds;
        
        public void SetTarget(Transform target)
        {
            this.target = target;
        }

        private void Start()
        {
            _camera = GetComponent<Camera>();
            CalculateBounds();
        }

        private void LateUpdate()
        {
            if (target == null) return;

            var targetPoint = new Vector3(target.position.x + Offset.x,
                (followY ? target.position.y + Offset.y : transform.position.y),
                transform.position.z);

            transform.position = Vector3.Lerp(transform.position, targetPoint,
                1 / Vector3.Distance(transform.position, targetPoint));

            FitInSpriteBounds();
        }

        private void CalculateBounds()
        {
            if (!_fitSprite)
                return;

            _bounds = (_fitSprite.bounds.min, _fitSprite.bounds.max);
        }

        private void FitInSpriteBounds()
        {
            if (!_fitSprite)
                return;

            Vector3 leftBottom = _camera.ViewportToWorldPoint(Vector3.zero);
            Vector3 rightTop = _camera.ViewportToWorldPoint(Vector3.one);

            float xPos = transform.position.x;
            float yPos = transform.position.y;

            if (leftBottom.x < _bounds.min.x)
                xPos = transform.position.x + (_bounds.min.x - leftBottom.x);
            else if (rightTop.x > _bounds.max.x)
                xPos = transform.position.x + (_bounds.max.x - rightTop.x);

            if (leftBottom.y < _bounds.min.y)
                yPos = transform.position.y + (_bounds.min.y - leftBottom.y);
            else if (rightTop.y > _bounds.max.y)
                yPos = transform.position.y + (_bounds.max.y - rightTop.y);

            transform.position = new Vector3(xPos, yPos, transform.position.z);
        }
    }
}