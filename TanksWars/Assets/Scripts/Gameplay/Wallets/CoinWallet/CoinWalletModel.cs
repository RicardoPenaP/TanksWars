using Unity.Netcode;
using UnityEngine;

namespace Gameplay.Wallets.CoinWallet
{       
    public class CoinWalletModel : NetworkVariable<int>
    {        
        public void AddCoins(int value)
        {
            Value += Mathf.Abs(value);
        }
    }
}