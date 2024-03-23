using System;

namespace Gameplay.VFX
{
    public interface IVfxPlayer
    {
        public void PlayVFX(Action onStopPlayingCallback = null);
    }
}
