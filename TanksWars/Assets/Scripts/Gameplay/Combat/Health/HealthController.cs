using Gameplay.Combat.Damage;
using System;
using Unity.Netcode;
using UnityEngine;

namespace Gameplay.Combat.Health
{
    public class HealthController : NetworkBehaviour, IDamageTaker
    {
        [Header("Health Controller")]

        [Header("References")]
        [SerializeField] private HealthView healthView;

        [Header("Settings")]
        [SerializeField] private int maxHealth = 100;

        public event Action<HealthController> OnDie;

        private HealthModel healthModel = new HealthModel();

        public override void OnNetworkSpawn()
        {
            ServerInitialization();
            ClientInitialization();            
        }

        private void HealthModel_OnValueChanged(int previousValue, int newValue)
        {
            float normalizedHealthValue = (float)newValue / maxHealth;
            healthView.UpdateHealthBar(normalizedHealthValue);
        }

        private void ServerInitialization()
        {
            if (!IsServer)
            {
                return;
            }
            healthModel.SetMaxHealth(maxHealth);
            healthModel.OnValueReachedZero += () => OnDie?.Invoke(this);
            HealthModel_OnValueChanged(0, maxHealth);
        }

        private void ClientInitialization()
        {
            if (!IsClient)
            {
                return;
            }
            healthModel.OnValueChanged += HealthModel_OnValueChanged;
        }

        public void TakeDamage(int value) => healthModel.TakeDamage(value);

        public void RestoreHealth(int value) => healthModel.RestoreHealth(value);

        public ulong GetClientOwnerID()
        {
            return OwnerClientId;
        }

    }
}
