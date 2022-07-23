using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Additions.Effects
{
	public abstract class Effect : MonoBehaviour
	{
		[SerializeField] private bool startOnAwake = false;
		[SerializeField] private bool loop = true;
		[SerializeField, Min(0)] private float loopDelay;

		protected Sequence Sequence;

		private bool _isPlaying = false;
		private Coroutine _delayedStart;
		private Action _callback = null;

		private void Start()
		{
			if (startOnAwake)
			{
				StartEffect();
			}
		}

		public void StartEffect(Action callback = null)
		{
			if (!_isPlaying)
			{
				_callback = callback;
				EffectLogic();
			}
		}

		public void StopEffect (bool waitForEnd = true)
		{
			if (waitForEnd)
			{
				Sequence.OnComplete(() => StopEffect(false));
			}
			else
			{
				Sequence.Kill();
				Sequence = null;
				
				_callback?.Invoke();
				_callback = null;
			}
			
			_isPlaying = false;
			if (_delayedStart != null)
				StopCoroutine(_delayedStart);
		}

		protected void Finish()
		{
			if (loop)
			{
				_delayedStart = StartCoroutine(DelayedEffectStart());
			}
			else
			{
				StopEffect(false);
			}
		}

		protected abstract void EffectLogic();

		private IEnumerator DelayedEffectStart()
		{
			yield return new WaitForSeconds(loopDelay);
			EffectLogic();
		}
	}
}