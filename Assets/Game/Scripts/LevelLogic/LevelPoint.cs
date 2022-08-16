using System;
using System.Collections;
using Scripts.Enemy;
using UnityEngine;

namespace Game.Scripts.LevelLogic
{
	public class LevelPoint : MonoBehaviour
	{
		[SerializeField] private TriggerObserver triggerObserver;
		[SerializeField] private ParticleSystem particle;
		[SerializeField] private ParticleSystem completeParticle;

		private IEnumerator _waitCoroutine;

		public event Action OnPointComplete;

		public bool IsComplete { get; private set; }

		private void OnEnable()
		{
			triggerObserver.TriggerEnter += PlayerEnter;
			triggerObserver.TriggerExit += PlayerExit;
		}

		private void OnDisable()
		{
			triggerObserver.TriggerEnter -= PlayerEnter;
			triggerObserver.TriggerExit -= PlayerExit;
		}

		private void PlayerExit(Collider obj) =>
			StopCoroutine(_waitCoroutine);

		private void PlayerEnter(Collider obj)
		{
			var beer = obj.GetComponentInParent<Beer>();

			_waitCoroutine = WaitToStopPlayer(beer);
			StartCoroutine(_waitCoroutine);
		}

		private IEnumerator WaitToStopPlayer(Beer beer)
		{
			while (true)
			{
				yield return null;

				if (beer.IsIdle)
				{
					Debug.Log(2);
					Complete();

					break;
				}
			}
		}

		private void Complete()
		{
			IsComplete = true;

			triggerObserver.gameObject.SetActive(false);
			particle.Stop();
			completeParticle.Play();

			OnPointComplete?.Invoke();
		}
	}
}