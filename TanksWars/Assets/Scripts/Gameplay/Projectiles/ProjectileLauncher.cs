using Gameplay.Input;
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

        private Timer timer;

        private bool canFire = true;
        private bool isFiring = false;

        public override void OnNetworkSpawn()
        {
            if (!IsOwner)
            {
                return;
            }

            timer = new Timer(1 / projectileLauncherSettings.FireRate, () => canFire = true, TimerMode.Decremental);

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
            GameObject clientProjectile = Instantiate(projectileLauncherSettings.ClientProjectilePrefab,
                                                    shootingPositionTransform.position, shootingPositionTransform.rotation);            
        }

        [ServerRpc]
        private void SpawnProjectileServerRpc()
        {
            GameObject serverProjectile = Instantiate(projectileLauncherSettings.ServerProjectilePrefab,
                                                    shootingPositionTransform.position, shootingPositionTransform.rotation);
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
    }
}
