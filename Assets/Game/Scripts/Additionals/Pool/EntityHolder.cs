using System.Collections.Generic;
using UnityEngine;

namespace Additions.Pool
{
	public abstract class EntityHolder<T> : MonoBehaviour
		where T : MonoBehaviour
	{
		[SerializeField] protected List<T> entities;
		private Queue<T> _queue = new Queue<T>();

		private T QueueEntity
		{
			get
			{
				if (_queue.Count <= 0)
					_queue = new Queue<T>(entities);
				return _queue.Dequeue();
			}
		}

		public T Pop()
		{
			var entity = QueueEntity;
			entity.gameObject.SetActive(true);
			entity.transform.parent = null;
			PopInternal(entity);
			return entity;
		}

		public virtual void Push(T entity)
		{
			entity.gameObject.SetActive(false);
			entity.transform.parent = transform;
		}

		protected virtual void PopInternal(T entity)
		{
		}
	}
}