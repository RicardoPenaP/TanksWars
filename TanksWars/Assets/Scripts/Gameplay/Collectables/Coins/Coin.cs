using Unity.Netcode;
using UnityEngine;

namespace Gameplay.Collectables.Coins
{
    public abstract class Coin : NetworkBehaviour, ICollectable
    {
        [Header("Coin")]

        [Header("References")]
        [SerializeField] private SpriteRenderer spriteRenderer;

        protected int value = 10;
        protected bool alreadyCollected;

        public abstract int Collect();

        public void SetValue(int value)
        {
            this.value = value;
        }

        protected void Show(bool state)
        {
            spriteRenderer.enabled = state;
        }
    }
}
