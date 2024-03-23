using System.Threading.Tasks;
using UnityEngine;

namespace Networking.Host
{
    public class HostSingleton : MonoBehaviour
    {
        private static HostSingleton instance;

        public static HostSingleton Instance => instance;

        private HostGameManager hostGameManager;

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

        public void CreateHost()
        {
            hostGameManager = new HostGameManager();

        }
    }
}
