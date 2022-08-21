using System;
using System.Collections;
using Pool;
using Scripts.Enemy.LevelLogic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Beer : MonoBehaviourPoolObject
{
	[SerializeField] private DragScript dragScript;
	[SerializeField, HideInInspector] private Rigidbody rb;

	public Vector3 CurrentVelocity => rb.velocity;
	public bool IsIdle => rb.velocity.magnitude == 0;

	public static event Action OnBeerStopped;

	private void OnEnable() =>
		dragScript.OnDragStop += StartMove;

	private void OnDisable() =>
		dragScript.OnDragStop -= StartMove;

	public void Init(Transform parent, Vector3 position, Trajectory trajectory)
	{
		transform.position = position;
		transform.parent = parent;

		dragScript.Init(trajectory);
	}

	public void AddForce(Vector3 force) =>
		rb.AddForce(force);

	private void StartMove() =>
		StartCoroutine(WaitToIdle());

	private IEnumerator WaitToIdle()
	{
		while (IsIdle)
			yield return null;

		while (IsIdle == false)
				yield return null;

		OnBeerStopped?.Invoke();
	}

	public override void Push() =>
		BeerPool.Instance.Push(this);

	#region Editor

	private void OnValidate()
	{
		if (rb == null)
			rb = GetComponent<Rigidbody>();
	}

	#endregion
}