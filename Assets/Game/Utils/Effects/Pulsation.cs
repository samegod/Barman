using DG.Tweening;
using UnityEngine;

namespace Additions.Effects
{
	public class Pulsation : Effect
	{
		[SerializeField] private float targetScale;
		[SerializeField] private float duration;

		protected override void EffectLogic()
		{
			Sequence = DOTween.Sequence();
			Sequence.Append(transform.DOScale(targetScale, duration).SetLoops(2, LoopType.Yoyo).OnComplete(Finish));
		}
	}
}