using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI.Bars
{
    public class HealthBar : MonoBehaviour
    {
        [Header("Health Bar")]
        [SerializeField] private Image fill;

        public void UpdateFill(float normalizedValue)
        {
            float newValue = Mathf.Clamp(normalizedValue, 0f, 1f);
            fill.fillAmount = newValue;
        }
    }
}
