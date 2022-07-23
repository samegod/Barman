using DG.Tweening;
using UnityEngine;

namespace Additions.Effects
{
    public class JellyBounce : Effect
    {
        [SerializeField] private float squizeStrength;
        [SerializeField] private float squizeTime;
        [SerializeField] private float releaseTime;
        
        
        protected override void EffectLogic()
        {
            Sequence = DOTween.Sequence();

            Vector3 initialScale = transform.localScale;
            
            Sequence.Append(
                transform.DOScaleX(initialScale.x + squizeStrength, squizeTime)
                    .SetEase(Ease.OutSine));
            
            Sequence.Join(
                transform.DOScaleY(initialScale.y - squizeStrength, squizeTime)
                    .SetEase(Ease.OutSine));

            Sequence.Append(
                transform.DOScale(initialScale, releaseTime)
                    .SetEase(Ease.OutElastic)
                    .OnComplete(Finish));
        }
    }
}