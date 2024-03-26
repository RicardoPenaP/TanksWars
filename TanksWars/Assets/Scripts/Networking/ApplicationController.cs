using Networking.Client;
using Networking.Host;
using System.Threading.Tasks;
using UnityEngine;

namespace Networking
{
    public class ApplicationController : MonoBehaviour
    {
        [Header("Application Controller")]

        [Header("References")]
        [SerializeField] private ClientSingleton clientSingletonPrefab;
        [SerializeField] private HostSingleton hostSingletonPrefab;

        private async void Start()
        {
            DontDestroyOnLoad(gameObject);

            await LaunchInModeAsync(SystemInfo.graphicsDeviceType == UnityEngine.Rendering.GraphicsDeviceType.Null);
        }

        private async Task LaunchInModeAsync(bool isDedicatedServer)
        {
            if (isDedicatedServer)
            {
                //Dedicated server logic
            }
            else
            {
                //Host and Client server logic
                HostSingleton hostSingleton = Instantiate(hostSingletonPrefab);

                hostSingleton.CreateHost();

                ClientSingleton clientSingleton = Instantiate(clientSingletonPrefab);

                bool authenticated = await clientSingleton.CreateClientAsync();

                if (authenticated)
                {
                    clientSingleton.GoToMainMenu();
                }
            }
        }
    }
}
