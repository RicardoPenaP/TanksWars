using Unity.Netcode;
using UnityEngine;

namespace Testing
{
    public class ServerTestingFunctionalities : MonoBehaviour
    {
        public void StartHost()
        {
            NetworkManager.Singleton.StartHost();
        }
        public void StartClient()
        {
            NetworkManager.Singleton.StartClient();
        }
    }
}
