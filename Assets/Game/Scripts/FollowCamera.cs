using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
	[SerializeField] private Transform target;
	[SerializeField] private Transform overviewPosition;
	[SerializeField] private float zoomModifierSpeed = 0.1f;
	[SerializeField] private float speed;

	private Vector2 _firstTouchPrevPos;
	private Vector2 _secondTouchPrevPos;
	private float _touchesPrevPosDifference;
	private float _touchesCurPosDifference;
	private float _zoomModifier;
	private bool _overview;

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
				y = targetPosition.y + 0.2f,
				z = position.z,
			};

			var pos = Vector3.Lerp(position, targetPos, speed * Time.deltaTime);
			position = pos;
			transform.position = position;
		}

		if (Input.touchCount == 2)
		{
			Touch firstTouch = Input.GetTouch(0);
			Touch secondTouch = Input.GetTouch(1);

			_firstTouchPrevPos = firstTouch.position - firstTouch.deltaPosition;
			_secondTouchPrevPos = secondTouch.position - secondTouch.deltaPosition;

			_touchesPrevPosDifference = (_firstTouchPrevPos - _secondTouchPrevPos).magnitude;
			_touchesCurPosDifference = (firstTouch.position - secondTouch.position).magnitude;

			_zoomModifier = (firstTouch.deltaPosition - secondTouch.deltaPosition).magnitude * zoomModifierSpeed;

			if (_touchesPrevPosDifference > _touchesCurPosDifference && transform.position.z > -11.5f)
				transform.position = new Vector3(transform.position.x, transform.position.y,
					transform.position.z - _zoomModifier);
			if (_touchesPrevPosDifference < _touchesCurPosDifference && transform.position.z < -9.5f)
				transform.position = new Vector3(transform.position.x, transform.position.y,
					transform.position.z + _zoomModifier);
		}
	}

	public void SetTarget(Transform targetPos) =>
		target = targetPos;

	public async void WaitAndOverview(int seconds, Transform pointToMove)
	{
		_overview = true;

		var position = overviewPosition.position;
		transform.position = new Vector3(position.x, position.y + 0.2f, position.z - 2.5f);

		transform.DOMove(new Vector3(pointToMove.position.x, transform.position.y, transform.position.z), seconds);

		StartCoroutine(Wait(seconds));
	}

	private IEnumerator Wait(int sec)
	{
		yield return new WaitForSeconds(sec);

		_overview = false;
	}
}