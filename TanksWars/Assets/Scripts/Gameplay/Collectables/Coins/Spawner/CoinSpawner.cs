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

            for (int i = 0; i < coinSpawnerSettings.MaxCoins; i++)
            {
                SpawnCoin();
            }

        }

        private void SpawnCoin()
        {
            RespawningCoin coinInstance = Instantiate(coinSpawnerSettings.RespawningCoinPrefab, GetSpawnPoint(),
                                                        Quaternion.identity, transform);
            coinInstance.SetValue(coinSpawnerSettings.CoinValue);
            coinInstance.GetComponent<NetworkObject>().Spawn();

            coinInstance.OnCollected += CoinInstance_OnCollected;
        }

        private void CoinInstance_OnCollected(RespawningCoin coin)
        {
            coin.transform.position = GetSpawnPoint();
            coin.ResetCoin();
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
