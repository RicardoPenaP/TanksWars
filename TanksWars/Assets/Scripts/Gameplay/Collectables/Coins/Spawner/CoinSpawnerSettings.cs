using UnityEngine;

namespace Gameplay.Collectables.Coins.Spawner
{
    [CreateAssetMenu(fileName = "NewCoinSpawnerSettings", menuName = "Gameplay/Collectables/Coins/Coin Spawner Settings")]
    public class CoinSpawnerSettings : ScriptableObject
    {
        [Header("Coin Spawner Settings")]

        [Header("Prefabs")]
        [SerializeField] private RespawningCoin respawningCoinPrefab;

        [Header("Settings")]
        [SerializeField] private int maxCoins = 50;
        [SerializeField] private int coinValue = 10;
        [SerializeField] private Vector2 xSpawnRange;
        [SerializeField] private Vector2 ySpawnRange;
        [SerializeField] private LayerMask layerMask;

        public int MaxCoins => maxCoins;
        public int CoinValue => coinValue;
        public Vector2 XSpawnRange => xSpawnRange;
        public Vector2 YSpawnRange => ySpawnRange;
        public LayerMask LayerMask => layerMask;
        public RespawningCoin RespawningCoinPrefab => respawningCoinPrefab;
        
    }
}