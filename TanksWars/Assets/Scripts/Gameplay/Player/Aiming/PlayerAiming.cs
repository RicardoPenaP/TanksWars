using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Player.Aiming
{
    public class PlayerAiming
    {
        private readonly Transform turretTransform;
        private Vector2 aimDirection;

        public PlayerAiming(IPlayerAimingInitializer playerAimingController)
        {            
            turretTransform = playerAimingController.TurretTransform;
            playerAimingController.OnPlayerAimUpdated += PlayerAimingController_OnPlayerAimUpdated;
        }

        private void PlayerAimingController_OnPlayerAimUpdated(Vector2 rawMousePosition)
        {
            Vector2 worldMousePosition = Camera.main.ScreenToWorldPoint(rawMousePosition);
            aimDirection = ((Vector3)worldMousePosition - turretTransform.position).normalized;
            turretTransform.up = aimDirection;
        }
    }
}
