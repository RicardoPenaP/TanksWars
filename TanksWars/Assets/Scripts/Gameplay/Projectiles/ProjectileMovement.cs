using UnityEngine;

namespace Gameplay.Projectiles
{
    public class ProjectileMovement : MonoBehaviour
    {
        [Header("Projectile Movement")]

        [Header("References")]
        [SerializeField] private new Rigidbody2D rigidbody2D;

        [Header("Settings")]
        [SerializeField] private float projectileSpeed = 30f;

        private void Start()
        {
            rigidbody2D.velocity = transform.up * projectileSpeed;
        }
    }
}
