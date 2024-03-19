using UnityEngine;

namespace Gameplay.Player.Movement
{
    public class PlayerMovement
    {
        private readonly PlayerMovementSettings playerMovementSettings;
        private readonly Transform bodyTransform;
        private readonly Rigidbody2D playerRigidbody2D;

        public PlayerMovement(IPlayerMovementInitializer playerMovementController)
        {
            playerMovementSettings = playerMovementController.PlayerMovementSettings;
            bodyTransform = playerMovementController.BodyTransform;
            playerRigidbody2D = playerMovementController.PlayerRigidbody2D;
            playerMovementController.OnPlayerMovevementUpdated += IPlayerMovementController_OnPlayerMove;
        }

        private void IPlayerMovementController_OnPlayerMove(Vector2 movementInput)
        {
            float movementDirection = movementInput.y;
            float turnDirection = -movementInput.x;

            Turn(turnDirection);
            Move(movementDirection);
        }

        private void Move(float movementDirection)
        {
            if (Mathf.Abs(movementDirection) < Mathf.Epsilon)
            {
                if (playerRigidbody2D.velocity.magnitude > Mathf.Epsilon)
                {
                    playerRigidbody2D.velocity = Vector2.zero;
                }
                return;
            }

            playerRigidbody2D.velocity = bodyTransform.up * movementDirection * playerMovementSettings.MovementSpeed;

        }

        private void Turn(float turnDirection)
        {
            if (Mathf.Abs(turnDirection) < Mathf.Epsilon)
            {
                return;
            }
            bodyTransform.Rotate(Vector3.forward, turnDirection * playerMovementSettings.TurningSpeed * Time.deltaTime);
        }
    }
}
