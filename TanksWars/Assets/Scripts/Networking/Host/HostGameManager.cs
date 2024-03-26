using SceneManagement;
using System;
using System.Threading.Tasks;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;

namespace Networking.Host
{
    public class HostGameManager
    {
        private const int MaxConnections = 20;
        private string ConnectionType = "udp";

        private Allocation allocation;
        private string joinCode;

        public async Task StartHostAsync()
        {
            try
            {
                allocation = await Relay.Instance.CreateAllocationAsync(MaxConnections);
            }
            catch (Exception exception)
            {
                Debug.LogError(exception);
            }

            try
            {
                joinCode = await Relay.Instance.GetJoinCodeAsync(allocation.AllocationId);
                Debug.Log(joinCode);
            }
            catch (Exception exception)
            {
                Debug.LogError(exception);
            }

            UnityTransport unityTransport = NetworkManager.Singleton.GetComponent<UnityTransport>();

            RelayServerData relayServerData = new RelayServerData(allocation, ConnectionType);
            unityTransport.SetRelayServerData(relayServerData);

            NetworkManager.Singleton.StartHost();
            GameScenesManager.ChangeAllClientsScene(GameScenes.Gameplay);
        }
    }
}
