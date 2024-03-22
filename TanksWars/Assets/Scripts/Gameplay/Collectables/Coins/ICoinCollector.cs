using System;

namespace Gameplay.Collectables.Coins
{
    public interface ICoinCollector
    {
        public event Action<Coin> OnCoinCollected;
    }
}