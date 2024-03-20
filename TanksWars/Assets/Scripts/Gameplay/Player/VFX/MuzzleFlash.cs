using Gameplay.Projectiles;
using System.Collections;
using System.Collections.Generic;
using Tools.TimeCounters;
using UnityEngine;

namespace Gameplay.Player.VFX
{
    public class MuzzleFlash : MonoBehaviour
    {
        [Header("Muzzle Flash")]
        [Header("References")]
        [SerializeField] private ProjectileLauncher projectileLauncher;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private float activeTime = .2f;

        private Timer timer;

        private void Awake()
        {
            timer = new Timer(activeTime, DesactivateMuzzleFlash, TimerMode.Decremental);
        }

        private void Start()
        {
            projectileLauncher.OnProjectileSpawned += ProjectileLauncher_OnProjectileSpawned;
        }

        private void Update()
        {
            timer.UpdateTimer();
        }

        private void OnDestroy()
        {
            projectileLauncher.OnProjectileSpawned -= ProjectileLauncher_OnProjectileSpawned;
        }

        private void ProjectileLauncher_OnProjectileSpawned()
        {
            ActivateMuzzleFlash();
            timer.StartTimer();
        }

        private void ActivateMuzzleFlash()
        {
            spriteRenderer.gameObject.SetActive(true);

        }

        private void DesactivateMuzzleFlash()
        {
            spriteRenderer.gameObject.SetActive(false);
        }


    }
}
