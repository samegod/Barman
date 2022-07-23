using DG.Tweening;
using UnityEngine;

namespace Additions.Effects
{
    public class RotateAround : Effect
    {
        [SerializeField] private float duration;
        
        protected override void EffectLogic()
        {
            Sequence = DOTween.Sequence();
            /*
	        Sequence.Append(transform.DORotateAround(duration / 2f)
                .SetEase(Ease.InBack)
                .OnComplete(
                () => transform.DORotateAround(duration / 2f)
                    .SetEase(Ease.OutBack)
                    .OnComplete(Finish)));
            */
        }
    }
}