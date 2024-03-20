using Gameplay.Combat.Damage;
using Gameplay.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using Tools.TimeCounters;
using Unity.Netcode;
using UnityEngine;

namespace Gameplay.Projectiles
{
    public class ProjectileLauncher : NetworkBehaviour
    {
        [Header("Projectile Launcher")]
        [Header("References")]
        [SerializeField] private InputReader inputReader;
        [SerializeField] private ProjectileLauncherSettings projectileLauncherSettings;
        [SerializeField] private Transform shootingPositionTransform;
        [SerializeField] private Collider2D playerCollider;

        public event Action OnProjectileSpawned;

        private Timer timer;

        private bool canFire = true;
        private bool isFiring = false;

        private void Awake()
        {
            timer = new Timer(1 / projectileLauncherSettings.FireRate, () => canFire = true, TimerMode.Decremental);
        }

        public override void OnNetworkSpawn()
        {
            if (!IsOwner)
            {
                return;
            }            

            inputReader.OnFireInputUpdated += InputReader_OnFireInputUpdated;
        }

        private void Update()
        {
            if (!IsOwner)
            {
                return;
            }

            timer.UpdateTimer();
            if (isFiring)
            {
                FireProjectile();
            }
        }

        public override void OnNetworkDespawn()
        {
            if (!IsOwner)
            {
                return;
            }
        }

        private void InputReader_OnFireInputUpdated(bool state)
        {
            isFiring = state;
        }

        private void FireProjectile()
        {
            if (!canFire)
            {
                return;
            }
            canFire = false;
            timer.StartTimer();
            SpawnProjectileServerRpc();
            SpawnClientProjectile();
        }

        private void SpawnClientProjectile()
        {
            InstantiateProjectile(projectileLauncherSettings.ClientProjectilePrefab);
            OnProjectileSpawned?.Invoke();
        }

        [ServerRpc]
        private void SpawnProjectileServerRpc()
        {
            InstantiateProjectile(projectileLauncherSettings.ServerProjectilePrefab);
            SpawnProjectileClientRpc();
        }

        [ClientRpc]
        private void SpawnProjectileClientRpc()
        {
            if (IsOwner)
            {
                return;
            }
            SpawnClientProjectile();
        }

        private void InstantiateProjectile(GameObject prefab)
        {
            GameObject instantiatedProjectile = Instantiate(prefab, shootingPositionTransform.position, shootingPositionTransform.rotation);
            Collider2D projectileCollider = instantiatedProjectile.GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(playerCollider, projectileCollider);
            if (instantiatedProjectile.TryGetComponent(out IDamageDealer damageDealer))
            {
                damageDealer.SetDamageOwnerId(OwnerClientId);
            }
        }
    }
}
