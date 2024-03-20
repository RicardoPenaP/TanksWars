using UnityEngine;

namespace Gameplay.Projectiles
{
    [CreateAssetMenu(fileName = "NewProjectileLauncherSettings", menuName ="Gameplay/Projectiles/Projectile Launcher Settings")]
    public class ProjectileLauncherSettings : ScriptableObject
    {
        [Header("Projectile Launcher Settings")]

        [Header("Prefabs Preferences")]
        [SerializeField] private GameObject serverProjectilePrefab;
        [SerializeField] private GameObject clientProjectilePrefab;

        [Header("Projectile Settings")]
        [SerializeField] private float projectileSpeed;

        [Header("Shooting Settings")]
        [SerializeField] private float fireRate;

        public GameObject ServerProjectilePrefab => serverProjectilePrefab;
        public GameObject ClientProjectilePrefab => clientProjectilePrefab;
        public float ProjectileSpeed => projectileSpeed;
        public float FireRate => fireRate;
    }
}
