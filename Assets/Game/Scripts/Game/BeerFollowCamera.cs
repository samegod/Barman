using System.Collections;
using DG.Tweening;
using Mechanics;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game
{
	public class BeerFollowCamera : MonoBehaviour
	{
		[TitleGroup("Transforms to Move")]
		[SerializeField] private Transform target;
		[SerializeField] private Transform overviewPosition;

		[TitleGroup("OnFollow")]
		[HorizontalGroup("OnFollow/Stats"), LabelWidth(65)]
		[SerializeField] private float speed;

		[HorizontalGroup("OnFollow/Stats"), LabelWidth(65)]
		[SerializeField] private float offSetY = 1f;

		[TitleGroup("Beer settings")]
		[HorizontalGroup("Beer settings/Split")]
		[VerticalGroup("Beer settings/Split/Left"), LabelWidth(65)]
		[BoxGroup("Beer settings/Split/Left/ForcePush")]
		[SerializeField] private float maxForce;

		[BoxGroup("Beer settings/Split/Left/ForcePush"), LabelWidth(65)]
		[SerializeField] private float maxAngle;

		[VerticalGroup("Beer settings/Split/Right")]
		[BoxGroup("Beer settings/Split/Right/ZPos"), LabelWidth(80)]
		[SerializeField] private float zPositionOnFollow;

		[BoxGroup("Beer settings/Split/Right/ZPos"), LabelWidth(80)]
		[SerializeField] private float zPositionOnOverview;

		[BoxGroup("Rotation On Follow")]
		[SerializeField] private Vector3 rotationOnFollow;


		private Vector2 _firstTouchPrevPos;
		private Vector2 _secondTouchPrevPos;
		private float _touchesPrevPosDifference;
		private float _touchesCurPosDifference;
		private float _zoomModifier;
		private bool _overview;

		private const float ZoomModifierSpeed = 0.001f;

		private void OnEnable() =>
			DragScript.OnForceUpdate += CheckToRotateCam;

		private void OnDisable() =>
			DragScript.OnForceUpdate -= CheckToRotateCam;

		private void Update()
		{
			if(target == null) return;

			if (!_overview)
			{
				var position = transform.position;
				var targetPosition = target.position;
				var targetPos = new Vector3()
				{
					x = targetPosition.x,
					y = targetPosition.y + offSetY,
					z = zPositionOnFollow,
				};

				var pos = Vector3.Lerp(position, targetPos, speed * Time.deltaTime);
				position = pos;
				transform.position = position;
			}

			if (Input.touchCount == 2)
			{
				var transformPosition = transform.position;

				Touch firstTouch = Input.GetTouch(0);
				Touch secondTouch = Input.GetTouch(1);

				_firstTouchPrevPos = firstTouch.position - firstTouch.deltaPosition;
				_secondTouchPrevPos = secondTouch.position - secondTouch.deltaPosition;

				_touchesPrevPosDifference = (_firstTouchPrevPos - _secondTouchPrevPos).magnitude;
				_touchesCurPosDifference = (firstTouch.position - secondTouch.position).magnitude;

				_zoomModifier = (firstTouch.deltaPosition - secondTouch.deltaPosition).magnitude * ZoomModifierSpeed;

				if (_touchesPrevPosDifference > _touchesCurPosDifference && transformPosition.z > -11.5f)
					transform.position = new Vector3(transformPosition.x, transformPosition.y,
						transformPosition.z - _zoomModifier);
				if (_touchesPrevPosDifference < _touchesCurPosDifference && transformPosition.z < -9.5f)
					transform.position = new Vector3(transformPosition.x, transformPosition.y,
						transformPosition.z + _zoomModifier);
			}
		}

		public void WaitAndOverview(int seconds, Transform pointToMove)
		{
			_overview = true;

			var position = overviewPosition.position;
			transform.position = new Vector3(position.x, position.y + 0.2f, zPositionOnOverview);

			var sequence = DOTween.Sequence();
			sequence.Append(
				transform
					.DOMove(new Vector3(pointToMove.position.x, transform.position.y, transform.position.z), seconds)
			);
			sequence.Append(
				transform
					.DORotate(rotationOnFollow, 3)
			);
			StartCoroutine(Wait(seconds));
		}

		private void CheckToRotateCam(Vector3 force) =>
			transform.DORotate(
				new Vector3(transform.rotation.eulerAngles.x,
					rotationOnFollow.y + ((Mathf.Clamp(force.x, 0, maxForce) * maxAngle) / maxForce),
					transform.rotation.eulerAngles.z), 3f);

		public void SetTarget(Transform targetPos) =>
			target = targetPos;

		private IEnumerator Wait(int sec)
		{
			yield return new WaitForSeconds(sec);

			_overview = false;
		}
	}
}