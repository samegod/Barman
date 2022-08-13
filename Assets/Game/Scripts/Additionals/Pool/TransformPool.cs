using UnityEngine;

namespace Pool
{
	public class TransformPool<T, A> : GenericPool<T, A>
		where T : MonoBehaviour, IPoolObject
		where A : TransformPool<T, A>
	{
		protected override void InitObj(T obj)
		{
			obj.transform.SetParent(transform);
		}

		public override T Pop(T origin)
		{
			var obj = PopInternal(origin);
			obj.transform.SetParent(null);
			obj.gameObject.SetActive(true);
			return obj;
		}

		public T Pop(T origin, Transform parent)
		{
			var obj = PopInternal(origin);
			obj.transform.SetParent(parent);
			obj.gameObject.SetActive(true);
			return obj;
		}

		public override void Push(T obj)
		{
			var pushedObj = PushInternal(obj);
			pushedObj.transform.SetParent(transform);
		}
	}
}