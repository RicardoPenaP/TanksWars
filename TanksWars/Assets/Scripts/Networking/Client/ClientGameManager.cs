using Networking.Client.Authentication;
using SceneManagement;
using System.Threading.Tasks;
using Unity.Services.Core;

namespace Networking.Client
{
    public class ClientGameManager
    {
        public async Task<bool> InitAsync()
        {
            await UnityServices.InitializeAsync();            

            return await AuthenticationHandler.StartAuthenticationAsync() == AuthenticationState.Authenticated;
        }

        internal void LoadGameScene(GameScenes gameScene)
        {
            GameScenesManager.ChangeScene(gameScene);
        }
    }
}
