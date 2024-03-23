using System;
using UnityEngine;

namespace Gameplay.VFX
{
    public class OnDestroyVFX : MonoBehaviour, IVfxPlayer
    {
        [Header("On Destroy VFX")]
        [SerializeField] private new ParticleSystem particleSystem;

        private Action onStopPlayingCallback;

        public void PlayVFX(Action onStopPlayingCallback = null)
        {
            particleSystem.Play();

            this.onStopPlayingCallback = onStopPlayingCallback;
        }

        private void OnParticleSystemStopped()
        {
            onStopPlayingCallback?.Invoke();
        }
    }
}
