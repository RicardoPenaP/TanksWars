using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static Gameplay.Input.Controls;

namespace Gameplay.Input
{
    [CreateAssetMenu(fileName = "New Input Reader", menuName = "Input/Input Reader")]
    public class InputReader : ScriptableObject, IPlayerActions
    {
        public event Action<Vector2> OnMoveInputUpdated;
        public event Action<bool> OnFireInputUpdated;

        private Controls controls;

        private void OnEnable()
        {
            if (controls is null)
            {
                controls = new Controls();
                controls.Player.SetCallbacks(this);
            }

            controls.Player.Enable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {            
            Vector2 rawInput = context.ReadValue<Vector2>();
            OnMoveInputUpdated?.Invoke(rawInput);            
        }

        public void OnPrimaryFire(InputAction.CallbackContext context)
        {
            OnFireInputUpdated?.Invoke(context.ReadValueAsButton());
        }
    }
}
