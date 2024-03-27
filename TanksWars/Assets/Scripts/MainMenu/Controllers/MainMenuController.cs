using MainMenu.Views;
using Networking.Client;
using Networking.Host;
using System.Threading.Tasks;
using UnityEngine;

namespace MainMenu.Controllers
{
    public class MainMenuController : MonoBehaviour
    {
        [Header("Main Menu Controller")]
        [SerializeField] private MainMenuView mainMenuView;

        private void Start()
        {
            mainMenuView.OnHostButtonPressed += MainMenuView_OnHostButtonPressed;
            mainMenuView.OnClientButtonPressed += MainMenuView_OnClientButtonPressed;
        }

        private void OnDestroy()
        {
            mainMenuView.OnHostButtonPressed -= MainMenuView_OnHostButtonPressed;
            mainMenuView.OnClientButtonPressed -= MainMenuView_OnClientButtonPressed;
        }

        private void MainMenuView_OnHostButtonPressed()
        {
            StarHost();
        }

        private void MainMenuView_OnClientButtonPressed(string joinCodeText)
        {
            StartClient(joinCodeText);
        }

        private async void StarHost() => await HostSingleton.Instance.StartHostAsync();

        private async void StartClient(string joinCodeText) => await ClientSingleton.Instance.StarClientAsync(joinCodeText);
    }
}
