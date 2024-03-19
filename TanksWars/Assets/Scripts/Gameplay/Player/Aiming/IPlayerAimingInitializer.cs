using System;
using UnityEngine;

namespace Gameplay.Player.Aiming
{
    public interface IPlayerAimingInitializer
    {
        public event Action<Vector2> OnPlayerAimUpdated;
        public Transform TurretTransform { get; }
    }
}
