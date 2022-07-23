using DG.Tweening;
using UnityEngine;

namespace Additions.Effects
{
    public class Particles : Effect
    {
        [SerializeField] private ParticleSystem particles;
        
        protected override void EffectLogic()
        {
            Sequence.Append(DOTween.To(PlayParticles, 0, 1, particles.main.duration).OnComplete(Finish));
        }

        private void PlayParticles (float f)
        {
            if(!particles.isPlaying)
                particles.Play();
        }

    }
}