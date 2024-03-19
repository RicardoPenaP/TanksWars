using System;
using UnityEngine;

namespace Gameplay.Player.Movement
{
    public interface IPlayerMovementInitializer
    {
        public event Action<Vector2> OnPlayerMovevementUpdated;

        public PlayerMovementSettings PlayerMovementSettings { get; }

        public Transform BodyTransform { get; }

        public Rigidbody2D PlayerRigidbody2D { get; }

    }
}
