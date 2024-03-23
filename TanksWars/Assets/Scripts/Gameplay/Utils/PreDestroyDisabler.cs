using System;
using UnityEngine;

namespace Gameplay.Utils
{
    [RequireComponent(typeof(DestroyOnContact))]
    public class PreDestroyDisabler : MonoBehaviour
    {
        [Header("PreDestroy Disabler")]              
        [SerializeField] private new Rigidbody2D rigidbody2D;
        [SerializeField] private new Collider2D collider2D;
        [SerializeField] private SpriteRenderer spriteRenderer;

        private DestroyOnContact destroyOnContact;

        private void Awake()
        {
            destroyOnContact = GetComponent<DestroyOnContact>();
        }

        private void Start()
        {
            destroyOnContact.OnPreDestroy += DestroyOnContact_OnPreDestroy;

        }

        private void OnDestroy()
        {
            destroyOnContact.OnPreDestroy -= DestroyOnContact_OnPreDestroy;
        }

        private void DestroyOnContact_OnPreDestroy()
        {
            DisableComponents();
        }

        private void DisableComponents()
        {
            rigidbody2D.Sleep();
            collider2D.enabled = false;
            spriteRenderer.enabled = false;
        }
    }
}
