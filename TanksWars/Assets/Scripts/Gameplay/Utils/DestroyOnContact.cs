using Gameplay.VFX;
using System;
using UnityEngine;

namespace Gameplay.Utils
{
    public class DestroyOnContact : MonoBehaviour
    {
        public event Action OnPreDestroy;
        private IVfxPlayer vfxPlayer;
        private void Awake()
        {
            vfxPlayer = GetComponentInChildren<IVfxPlayer>();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (vfxPlayer is null)
            {
                Destroy(gameObject);
            }
            else
            {
                OnPreDestroy?.Invoke();
                vfxPlayer.PlayVFX(() => Destroy(gameObject));
            }            
        }
    }
}
