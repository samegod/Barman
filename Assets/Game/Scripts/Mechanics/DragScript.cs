using System;
using System.Collections;
using Game;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DragScript : MonoBehaviour
{
	[SerializeField] private Beer beer;
	[SerializeField] private float minForceToPush;
	[SerializeField] private float pushForce;

	private Trajectory _trajectory;

	private Vector3 _startPoint;
	private Vector3 _endpoint;
	private Vector3 _force;
	private float _distance;

	private bool _isDragging;

	public event Action OnDragStop;

	public static event Action<Vector3> OnForceUpdate;

	public void Update()
	{
		if (_isDragging)
			OnDrag();
	}

	public void Init(Trajectory trajectory) =>
		_trajectory = trajectory;

	private void OnMouseDown()
	{
		_isDragging = true;
		OnDragStart();
	}

	private void OnMouseUp()
	{
		_isDragging = false;
		OnDragEnd();

		OnDragStop?.Invoke();
	}

	private void OnDragStart()
	{
		_startPoint = Input.mousePosition;

		_trajectory.Show();
	}

	private void OnDrag()
	{
		_endpoint = Input.mousePosition;
		_distance = Vector2.Distance(_startPoint, _endpoint);
		_force = (_startPoint - _endpoint).normalized * (_distance * -pushForce);

		OnForceUpdate?.Invoke(_force);

		_trajectory.UpdateDots(transform.position, _force);
	}

	private void OnDragEnd()
	{
		_isDragging = false;
		if (_force.x > minForceToPush)
			beer.AddForce(_force.x * Vector3.right);

		_force = Vector3.zero;
		OnForceUpdate?.Invoke(_force);
		_trajectory.Hide();
	}
}