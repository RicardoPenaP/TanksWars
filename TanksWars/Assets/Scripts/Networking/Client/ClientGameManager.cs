using Networking.Client.Authentication;
using System;
using System.Threading.Tasks;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;

namespace Networking.Client
{
    public class ClientGameManager
    {
        private const string ConnectionType = "dtls";
        private JoinAllocation allocation;
        internal async Task<bool> InitAsync()
        {
            await UnityServices.InitializeAsync();

            return await AuthenticationHandler.StartAuthenticationAsync() == AuthenticationState.Authenticated;
        }

        internal async Task StartClientAsync(string joinCodeText)
        {
            try
            {
                allocation = await Relay.Instance.JoinAllocationAsync(joinCodeText);
            }
            catch (Exception exception)
            {
                Debug.LogError(exception);
                return;
            }

            UnityTransport unityTransport = NetworkManager.Singleton.GetComponent<UnityTransport>();

            RelayServerData relayServerData = new RelayServerData(allocation, ConnectionType);
            unityTransport.SetRelayServerData(relayServerData);

            NetworkManager.Singleton.StartClient();
        }
    }
}
