using UnityEngine;

namespace Gameplay.Player.Movement
{
    [CreateAssetMenu(fileName = "NewPlayerMovementSettings", menuName = "Gameplay/Player/Player Movement Settings")]
    public class PlayerMovementSettings : ScriptableObject
    {
        [Header("Player Movement Settings")]
        [SerializeField] private float movementSpeed;
        [SerializeField] private float turningSpeed;

        public float MovementSpeed => movementSpeed;
        public float TurningSpeed => turningSpeed;
    }
}
