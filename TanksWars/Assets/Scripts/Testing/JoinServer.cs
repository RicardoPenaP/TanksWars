using Unity.Netcode;
using UnityEngine;

namespace Testing
{
    public class JoinServer : MonoBehaviour
    {
        public void Join()
        {
            NetworkManager.Singleton.StartClient();
        }
    }
}
