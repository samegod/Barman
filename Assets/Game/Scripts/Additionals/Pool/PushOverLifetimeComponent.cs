using System.Collections;
using UnityEngine;

namespace Pool
{
	[RequireComponent(typeof(MonoBehaviourPoolObject))]
	public class PushOverLifetimeComponent : MonoBehaviour
	{
		[SerializeField, HideInInspector] MonoBehaviourPoolObject obj;
		[SerializeField] private float lifeTime;

		private void OnEnable()
		{
			if (obj != null)
				StartCoroutine(PushOverLifetime(obj));
			else
				Debug.LogError("PushOverLifetimeComponent attached to non-MonoBehaviourPoolObject GameObject");
		}

		private IEnumerator PushOverLifetime(MonoBehaviourPoolObject poolObject)
		{
			yield return new WaitForSeconds(lifeTime);
			poolObject.Push();
		}

		private void OnValidate()
		{
			if (obj == null)
				obj = GetComponent<MonoBehaviourPoolObject>();
		}
	}
}