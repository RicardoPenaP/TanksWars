using Gameplay.Collectables.Coins;
using Unity.Netcode;
using UnityEngine;

namespace Gameplay.Wallets.CoinWallet
{
    public class CoinWalletController : NetworkBehaviour
    {        
        private ICoinCollector coinCollector;

        //Public only for testing        
        public CoinWalletModel coinWalletModel = new CoinWalletModel();

        public override void OnNetworkSpawn()
        {
            coinCollector = GetComponentInChildren<ICoinCollector>();
            coinCollector.OnCoinCollected += CoinCollector_OnCoinCollected;
        }

        public override void OnNetworkDespawn()
        {
            coinCollector.OnCoinCollected -= CoinCollector_OnCoinCollected;
        }

        private void CoinCollector_OnCoinCollected(Coin coin)
        {
            int coinValue = coin.Collect();

            if (!IsServer)
            {
                return;
            }

            coinWalletModel.AddCoins(coinValue);
        }
    }
}
