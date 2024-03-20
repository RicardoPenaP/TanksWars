using Gameplay.Combat.Damage;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Gameplay.Projectiles
{
    public class ProjectileAttack : MonoBehaviour, IDamageDealer
    {
        [Header("Projectile Attack")]
        [Header("Settings")]
        [SerializeField] private int damage = 25;

        private ulong ownerClientId;

        public void DealDamage(IDamageTaker damageTaker)
        {
            damageTaker.TakeDamage(damage);
        }

        public void SetDamageOwnerId(ulong ownerId)
        {
            ownerClientId = ownerId;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.attachedRigidbody.TryGetComponent(out IDamageTaker damageTaker))
            {
                if (ownerClientId == damageTaker.GetClientOwnerID())
                {
                    return;
                }
                DealDamage(damageTaker);
            }
        }
    }
}
