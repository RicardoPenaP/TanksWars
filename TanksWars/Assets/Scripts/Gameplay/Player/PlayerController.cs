using Gameplay.Input;
using Gameplay.Player.Aiming;
using Gameplay.Player.Movement;
using Gameplay.Projectiles;
using System;
using Unity.Netcode;
using UnityEngine;


namespace Gameplay.Player
{
    public class PlayerController : NetworkBehaviour, IPlayerMovementInitializer, IPlayerAimingInitializer
    {
        [Header("Player Controller")]

        [Header("Input References")]
        [SerializeField] private InputReader inputReader;

        [Header("Tank Parts References")]
        [SerializeField] private Transform bodyTransform;
        [SerializeField] private Transform turretTransform;
        [SerializeField] private Transform shootingPositionTransform;

        [Header("Movement References")]
        [SerializeField] private PlayerMovementSettings playerMovementSettings;        
        [SerializeField] private Rigidbody2D playerRigidbody2D;

        [Header("Projectile References")]
        [SerializeField] private ProjectileLauncherSettings projectileLauncherSettings;
               

        public event Action<Vector2> OnPlayerMovevementUpdated;
        public event Action<Vector2> OnPlayerAimUpdated;

        private PlayerMovement playerMovement;
        private PlayerAiming playerAiming;        

        public PlayerMovementSettings PlayerMovementSettings => playerMovementSettings;

        public Transform BodyTransform => bodyTransform;

        public Rigidbody2D PlayerRigidbody2D => playerRigidbody2D;

        public Transform TurretTransform => turretTransform;

        public ProjectileLauncherSettings ProjectileLauncherSettings => projectileLauncherSettings;

        public Transform ShootingPositionTransform => shootingPositionTransform;

        private Vector2 rawMovementInput;        

        public override void OnNetworkSpawn()
        {
            if (!IsOwner)
            {
                return;
            }

            playerMovement = new PlayerMovement(this);
            playerAiming = new PlayerAiming(this);            

            inputReader.OnMoveInputUpdated += InputReader_OnMoveInputUpdated;                          
        }

        private void Update()
        {
            if (!IsOwner)
            {
                return;
            }
            UpdatePlayer();
        }

        public override void OnNetworkDespawn()
        {
            if (!IsOwner)
            {
                return;
            }

            inputReader.OnMoveInputUpdated -= InputReader_OnMoveInputUpdated;
        }

        private void InputReader_OnMoveInputUpdated( Vector2 rawInput)
        {
            rawMovementInput = rawInput;
        }

        private void UpdatePlayer()
        {
            UpdateMovement();
            UpdateAiming();            
        }

        private void UpdateMovement()
        {
            OnPlayerMovevementUpdated?.Invoke(rawMovementInput);
        }

        private void UpdateAiming()
        {
            OnPlayerAimUpdated?.Invoke(inputReader.AimPosition);
        }
     
    }
}
