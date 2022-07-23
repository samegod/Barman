using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Additions.Effects
{
	public class Scale : Effect
	{
		[SerializeField] private float statScele;
		[SerializeField] private float targetScale;
		[SerializeField] private float duration;
		[SerializeField, Min(1)] private int delay;

		protected override async void EffectLogic()
		{
			Sequence = DOTween.Sequence();
			Sequence.Append(transform.DOScale(statScele, 0));

			await Task.Delay(delay);
			Sequence.Append(transform.DOScale(targetScale, duration).OnComplete(Finish));
		}
	}
}