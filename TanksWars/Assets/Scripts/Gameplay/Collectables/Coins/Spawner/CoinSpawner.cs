using Unity.Netcode;
using UnityEngine;

namespace Gameplay.Collectables.Coins.Spawner
{
    public class CoinSpawner : NetworkBehaviour
    {
        [Header("Coin Spawner")]

        [Header("References")]
        [SerializeField] private CoinSpawnerSettings coinSpawnerSettings;

        private Collider2D[] coinBuffer = new Collider2D[1];
        private float coinRadius;



        public override void OnNetworkSpawn()
        {
            if (!IsServer)
            {
                return;
            }
            coinRadius = coinSpawnerSettings.RespawningCoinPrefab.GetComponent<CircleCollider2D>().radius;

        }

        private void SpawnCoin()
        {
            Instantiate(coinSpawnerSettings.RespawningCoinPrefab);
        }

        private Vector2 GetSpawnPoint()
        {
            float x, y;

            while (true)
            {
                x = Random.Range(coinSpawnerSettings.XSpawnRange.x, coinSpawnerSettings.XSpawnRange.y);
                y = Random.Range(coinSpawnerSettings.YSpawnRange.x, coinSpawnerSettings.YSpawnRange.y);
                Vector2 spawnPoint = new Vector2(x, y);

                int numColliders = Physics2D.OverlapCircleNonAlloc(spawnPoint, coinRadius, coinBuffer, coinSpawnerSettings.LayerMask);
                if (numColliders == 0)
                {
                    return spawnPoint;
                }
            }
        }
    }
}
