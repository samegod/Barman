using System.Numerics;
using DG.Tweening;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Additions.Effects
{
	public class ShipEffect : Effect
	{
		[SerializeField] private float duration;
		[SerializeField] private float height;
		[SerializeField] private float angle;

		public float Angle { get => angle; set => angle = value; }
		public float Height { get => height; set => height = value; }
		public float Duration { get => duration; set => duration = value; }
		

		protected override void EffectLogic()
		{
			float currentHeight = transform.position.y;

			Sequence = DOTween.Sequence();

			Sequence.Append(transform.DOMoveY(currentHeight + height, duration).SetEase(Ease.InOutSine));
			Sequence.Join(
				transform
					.DORotate(Vector3.forward * angle, duration / 2f)
					.SetEase(Ease.OutSine)
					.OnComplete(() =>
						transform
							.DORotate(Vector3.zero, duration / 2f)
							.SetEase(Ease.InSine)
					)
			);

			Sequence.Append(transform.DOMoveY(currentHeight, duration).SetEase(Ease.InOutSine));
			Sequence.Join
			(
				transform
					.DORotate(Vector3.back * angle, duration / 2f)
					.SetEase(Ease.OutSine)
					.OnComplete(() =>
						transform
							.DORotate(Vector3.zero, duration / 2f)
							.SetEase(Ease.InSine)
							.OnComplete(Finish)
					)
			);
		}
	}
}