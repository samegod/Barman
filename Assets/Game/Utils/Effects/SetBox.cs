using DG.Tweening;
using UnityEngine;

namespace Additions.Effects
{
    public class SetBox : Effect
    {
        [SerializeField] private float duration;
        [Range(0, 1f)]
        [SerializeField] private float shakingForce = 0.5f;

        private Vector3 _currentScale;
		private void OnEnable()
		{
            _currentScale = transform.localScale;

        }
		protected override void EffectLogic()
        {
            transform.DOScale(shakingForce, duration * 0.25f)
                .OnComplete(() =>
				{
                    transform.DOScaleY(_currentScale.y, duration).SetEase(Ease.InElastic)
                        .OnComplete(() =>
                        transform.DOScaleX(_currentScale.x, duration).SetEase(Ease.OutElastic).SetEase(Ease.OutBounce));
                });
        }
    }
}