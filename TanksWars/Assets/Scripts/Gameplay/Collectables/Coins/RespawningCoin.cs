using System;

namespace Gameplay.Collectables.Coins
{
    public class RespawningCoin : Coin
    {
        public event Action<RespawningCoin> OnCollected;

        public override int Collect()
        {
            if (!IsServer)
            {
                Show(false);
                
                return 0;
            } 
            
            if (alreadyCollected)
            {
                return 0;
            }

            alreadyCollected = true;
            OnCollected?.Invoke(this);

            return value;

        }

        public void ResetCoin()
        {
            alreadyCollected = false;
            if (!IsClient)
            {
                return;
            }
            Show(true);
        }

    }
}
