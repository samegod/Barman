using System;
using System.Collections.Generic;
using System.Linq;
using Additions.Extensions;
using DG.Tweening;
using UnityEngine;

namespace Additions.CameraUtils
{
    [RequireComponent(typeof(Camera))]
    public class CameraController : MonoBehaviour
    {
        [SerializeField, HideInInspector] private Camera cam;
        [SerializeField] private float defaultSpeed;

        private Tween _moveTween;
        private Vector3 _startPosition;
        private float _startSize;
        private Transform _followingTarget;
        private bool _following;

        private Action _moveCallback;

        private float Progress { get; set; }

        public Camera Cam => cam;

        public float StartSize => _startSize;

        private void Start()
        {
            Progress = 0;
            _startPosition = cam.gameObject.transform.position;
            _startSize = cam.orthographicSize;
        }

        public void ReturnToStartPosition() =>
            MoveTo(_startPosition, StartSize);

        private void Update()
        {
            if (_following)
            {
                if (_followingTarget != null)
                {
                    Vector3 newPosition = Vector3.Lerp(transform.position, _followingTarget.position,
                        Time.deltaTime * defaultSpeed);
                    newPosition.z = _startPosition.z;
                    transform.position = newPosition;
                }
            }
        }

        public CameraController PathMoveByTime(List<Transform> target, float speed = 0, float orthoSize = -1,
            Ease functionOfMove = Ease.Linear)
        {
            PathMove(target, speed, orthoSize, functionOfMove, false);

            return this;
        }

        public CameraController PathMoveBySpeed(List<Transform> target, float speed = 0, float orthoSize = -1,
            Ease functionOfMove = Ease.Linear)
        {
            PathMove(target, speed, orthoSize, functionOfMove, true);

            return this;
        }

        private void PathMove(List<Transform> target,  float speed, float orthoSize,
            Ease functionOfMove, bool speedBased)
        {
            var speedAnimation = speed == 0 ? defaultSpeed : speed;

            Sequence sequence = DOTween.Sequence();
            sequence.Append(Cam
                .transform
                .DOPath(target.LocalPositions().ToArray(), speedAnimation)
                .SetEase(functionOfMove)
                .SetSpeedBased(speedBased)
                .OnComplete(delegate
                {
                    _moveCallback?.Invoke();
                    _moveCallback = null;
                }));

            if (orthoSize >= 0)
            {
                sequence.Join(
                    Cam
                        .DOOrthoSize(orthoSize, speed)
                        .SetEase(functionOfMove)
                        .SetSpeedBased(speedBased));
            }
        }

        public CameraController MoveToByTime(Vector3 pos,  float speed = 0, float orthoSize = -1, Ease functionOfMove = Ease.Linear)
        {
            MoveTo(pos, speed, orthoSize, functionOfMove, false);
            
            return this;
        }
        
        public CameraController MoveToBySpeed(Vector3 pos,  float speed = 0, float orthoSize = -1, Ease functionOfMove = Ease.Linear)
        {
            MoveTo(pos, speed, orthoSize, functionOfMove, true);
            
            return this;
        }
        
        public CameraController MoveTo(Vector3 pos, float speed, float orthoSize, Ease functionOfMove, bool speedBased)
        {
            var speedAnimation = speed == 0 ? defaultSpeed : speed;

            Sequence sequence = DOTween.Sequence();
            sequence.Append(
                    Cam
                        .transform
                        .DOMove(new Vector3(pos.x, pos.y, Cam.transform.position.z), speedAnimation)
                        .SetEase(functionOfMove)
                        .SetSpeedBased(false))
                .OnComplete(delegate
                {
                    _moveCallback?.Invoke();
                    _moveCallback = null;
                });
            sequence.Join(
                Cam
                    .DOOrthoSize(orthoSize, speedAnimation)
                    .SetEase(functionOfMove)
                    .SetSpeedBased(false));

            return this;
        }

        public CameraController SetOrthoSize(float size, float time, Ease functionOfMove = Ease.Linear,
            Action completed = null)
        {
            float speed = defaultSpeed <= 0 ? time : defaultSpeed;

            _moveTween =
                Cam
                    .DOOrthoSize(size, speed)
                    .SetEase(functionOfMove)
                    .OnComplete(delegate
                    {
                        _moveCallback?.Invoke();
                        _moveCallback = null;
                        completed?.Invoke();
                    })
                    .SetSpeedBased(false);

            return this;
        }

        public CameraController SetOrthoSizeByTime(float size, float time, Ease functionOfMove = Ease.Linear)
        {
            _moveTween =
                Cam
                    .DOOrthoSize(size, time)
                    .SetEase(functionOfMove)
                    .OnComplete(delegate
                    {
                        _moveCallback?.Invoke();
                        _moveCallback = null;
                    })
                    .SetSpeedBased(false);

            return this;
        }

        public CameraController Follow(Transform target)
        {
            _following = true;
            _followingTarget = target;

            return this;
        }

        public CameraController StopFollowing()
        {
            _following = false;

            return this;
        }

        public CameraController SetPosition(Vector3 position, float orthoSize)
        {
            Cam.transform.position = position;
            Cam.orthographicSize = orthoSize;

            return this;
        }

        public CameraController OnMoveComplete(Action callback)
        {
            _moveCallback += callback;
            return this;
        }

        #region Editor

        private void OnValidate() =>
            cam = Cam ?? Camera.main;

        #endregion
    }
}