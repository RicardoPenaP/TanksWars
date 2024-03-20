using UnityEngine;

namespace Gameplay.Utils
{
    public class DestroyOnContact : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Destroy(gameObject);
        }
    }
}
