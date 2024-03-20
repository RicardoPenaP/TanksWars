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
            if (!IsServer)
            {
                return;
            }

            healthModel.SetMaxHealth(maxHealth);
            healthModel.OnValueChanged += HealthModel_OnValueChanged;
            healthModel.OnValueReachedZero += () => OnDie?.Invoke(this);
            HealthModel_OnValueChanged(0, maxHealth);

        }

        private void HealthModel_OnValueChanged(int previousValue, int newValue)
        {
            if (IsClient)
            {
                float normalizedHealthValue = newValue / maxHealth;
                healthView?.UpdateHealthBar(normalizedHealthValue);
            }            
        }

        public void TakeDamage(int value) => healthModel.TakeDamage(value);        

        public void RestoreHealth(int value) => healthModel.RestoreHealth(value);

        public ulong GetClientOwnerID()
        {
            return OwnerClientId;
        }
    }
}
