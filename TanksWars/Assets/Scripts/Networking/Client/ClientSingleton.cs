using System.Threading.Tasks;
using UnityEngine;

namespace Networking.Client
{
    public class ClientSingleton : MonoBehaviour
    {
        private static ClientSingleton instance;

        public static ClientSingleton Instance => instance;

        private ClientGameManager clientGameManager;

        private void Awake()
        {
            if (instance is not null && instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        public async Task CreateClientAsync()
        {
            clientGameManager = new ClientGameManager();

            await clientGameManager.InitAsync();
        }
    }
}
