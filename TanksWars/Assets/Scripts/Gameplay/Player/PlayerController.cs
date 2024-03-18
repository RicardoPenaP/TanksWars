using Gameplay.Input;
using Gameplay.Player.Movement;
using System;
using Unity.Netcode;
using UnityEngine;


namespace Gameplay.Player
{
    public class PlayerController : NetworkBehaviour, IPlayerMovementController
    {
        [Header("Player Controller")]

        [Header("Input References")]
        [SerializeField] private InputReader inputReader;

        [Header("Tank Parts References")]
        [SerializeField] private Transform bodyTransform;
        [SerializeField] private Transform turretTransform;

        [Header("Movement References")]
        [SerializeField] private PlayerMovementSettings playerMovementSettings;        
        [SerializeField] private Rigidbody2D playerRigidbody2D;

        //testing
        [SerializeField] private Vector2 testInput;
        

        public event Action<Vector2> OnPlayerMovevementUpdated;

        private PlayerMovement playerMovement;

        public PlayerMovementSettings PlayerMovementSettings => playerMovementSettings;

        public Transform BodyTransform => bodyTransform;

        public Rigidbody2D PlayerRigidbody2D => playerRigidbody2D;

        private Vector2 rawMovementInput;

        public override void OnNetworkSpawn()
        {
            if (!IsOwner)
            {
                return;
            }

            playerMovement = new PlayerMovement(this);
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
        }

        private void UpdateMovement()
        {
            OnPlayerMovevementUpdated?.Invoke(rawMovementInput);
        }
    }
}
