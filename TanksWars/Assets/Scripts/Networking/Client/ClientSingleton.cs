using SceneManagement;
using System;
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

        public async Task<bool> CreateClientAsync()
        {
            clientGameManager = new ClientGameManager();

            return await clientGameManager.InitAsync();
        }

        public void GoToMainMenu()
        {
            GameScenesManager.ChangeLocalScene(GameScenes.MainMenu);
        }

        public async Task StarClientAsync(string joinCodeText) => await clientGameManager.StartClientAsync(joinCodeText);

    }
}
