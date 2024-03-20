using Gameplay.UI.Bars;
using Unity.Netcode;
using UnityEngine;

namespace Gameplay.Combat.Health
{
    public class HealthView : NetworkBehaviour
    {
        [Header("Health View")]

        [Header("References")]
        [SerializeField] private HealthBar healthBar;

        public void UpdateHealthBar(float normalizedValue) => healthBar.UpdateFill(normalizedValue);

    }
}
