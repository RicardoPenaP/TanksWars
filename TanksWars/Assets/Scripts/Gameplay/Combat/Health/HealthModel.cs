using System;
using Unity.Netcode;
using UnityEngine;

namespace Gameplay.Combat.Health
{
    public class HealthModel : NetworkVariable<int>
    {
        public event Action OnValueReachedZero;

        private int maxHealth = 0;

        private bool isDead = false;

        public int MaxHealth => maxHealth;
        public bool IsDead => isDead;

        //public HealthModel(int maxHealth)
        //{            
        //    this.maxHealth = maxHealth;
        //    Value = this.maxHealth;
        //    isDead = false;
        //}

        public void SetMaxHealth(int maxHealth)
        {
            this.maxHealth = maxHealth;
            Value = this.maxHealth;
        }

        public void TakeDamage(int value)
        {
            ModifyHealth(-Mathf.Abs(value));
        }

        public void RestoreHealth(int value)
        {
            ModifyHealth(Mathf.Abs(value));
        }

        private void ModifyHealth(int value)
        {
            Value = Mathf.Clamp(Value + value, 0, maxHealth);
            if (Value == 0)
            {
                OnValueReachedZero?.Invoke();
            }
        }

    }
}
