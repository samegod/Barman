using System.Collections.Generic;
using Additions.Extensions;
using UnityEngine;

namespace Pool
{
	public abstract class GenericPool<T, TA> : MonoSingleton<TA>
		where T : IPoolObject
		where TA : MonoSingleton<TA>
	{
		private readonly Dictionary<T, Queue<T>> _entries = new Dictionary<T, Queue<T>>();

		public void Preload(T origin, int targetCount = 0)
		{
			if (origin == null)
			{
				Debug.LogWarningFormat("Empty Pool Object On Preload");
				return;
			}

			CheckEntries(origin);

			var count = targetCount > 0 ? targetCount : origin.PreloadCount;
			count -= _entries[origin].Count;

			for (var i = 0; i < count; i++)
			{
				var loadObj = (T) origin.LoadObject(origin);
				_entries[origin].Enqueue(loadObj);
				InitObj(loadObj);
			}
		}

		protected virtual void InitObj(T obj)
		{
		}

		public virtual T Pop(T origin)
		{
			return PopInternal(origin);
		}

		protected T PopInternal(T origin)
		{
			CheckEntries(origin);

			var obj = _entries[origin].Count > 0 ? _entries[origin].Dequeue() : (T) origin.LoadObject(origin);
			obj.OnPop();

			return obj;
		}

		public virtual void Push(T obj)
		{
			PushInternal(obj);
		}

		protected T PushInternal(T obj)
		{
			CheckEntries((T) obj.Origin);

			obj.OnPush();
			_entries[(T) obj.Origin].Enqueue(obj);

			return obj;
		}

		private void CheckEntries(T origin)
		{
			if (!_entries.ContainsKey(origin))
				_entries.Add(origin, new Queue<T>());
		}
	}
}