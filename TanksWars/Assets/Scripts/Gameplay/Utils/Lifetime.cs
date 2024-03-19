using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Utils
{
    public class Lifetime : MonoBehaviour
    {
        [Header("Lifetime")]
        [SerializeField] private float lifetime = 3f;

        private IEnumerator Start()
        {
            float timer = 0;
            while (timer < lifetime)
            {
                timer += Time.deltaTime;
                yield return null;
            }

            Destroy(gameObject);
        }
    }
}
