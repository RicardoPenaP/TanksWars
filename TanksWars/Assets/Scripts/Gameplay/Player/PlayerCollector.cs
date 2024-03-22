using Gameplay.Collectables;
using Gameplay.Collectables.Coins;
using System;
using Unity.Netcode;

namespace Gameplay.Player
{
    public class PlayerCollector : NetworkBehaviour, ICoinCollector
    {
        public event Action<Coin> OnCoinCollected;

        private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
        {
            if (collision.TryGetComponent(out ICollectable collectable))
            {
                switch (collectable)
                {
                    case Coin coin:
                        OnCoinCollected?.Invoke(coin);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
