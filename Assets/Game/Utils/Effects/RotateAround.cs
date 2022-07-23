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

            Vector3 newVec = transform.rotation.eulerAngles;
            newVec.y += 180;

            Sequence.Append(transform.DORotate(NewRotationAngle(), duration / 2f)
                .SetEase(Ease.Linear)
                .OnComplete(
                () => transform.DORotate(NewRotationAngle(), duration / 2f)
                    .SetEase(Ease.Linear)
                    .OnComplete(Finish)));
            
        }

        private Vector3 NewRotationAngle()
        {
            Vector3 newAngle = transform.rotation.eulerAngles;
            newAngle.y += 180;

            return newAngle;
        }
    }
}