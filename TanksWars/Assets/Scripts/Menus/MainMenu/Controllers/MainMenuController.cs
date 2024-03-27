using Menus.MainMenu.Views;
using Networking.Client;
using Networking.Host;
using System;
using UnityEngine;

namespace Menus.MainMenu.Controllers
{
    public class MainMenuController : MonoBehaviour
    {
        [Header("Main Menu Controller")]

        [Header("References")]
        [SerializeField] private MainMenuView mainMenuView;
        [SerializeField] private LobbiesController lobbiesController;

        public event Action OnLobbiesButtonPressed;

        private void Start()
        {
            mainMenuView.OnHostButtonPressed += MainMenuView_OnHostButtonPressed;
            mainMenuView.OnClientButtonPressed += MainMenuView_OnClientButtonPressed;
            mainMenuView.OnLobbiesButtonPressed += MainMenuView_OnLobbiesButtonPressed;

            lobbiesController.OnCloseLobbiesMenu += LobbiesController_OnCloseLobbiesMenu;
        }


        private void OnDestroy()
        {
            mainMenuView.OnHostButtonPressed -= MainMenuView_OnHostButtonPressed;
            mainMenuView.OnClientButtonPressed -= MainMenuView_OnClientButtonPressed;
            mainMenuView.OnLobbiesButtonPressed -= MainMenuView_OnLobbiesButtonPressed;
            
            lobbiesController.OnCloseLobbiesMenu -= LobbiesController_OnCloseLobbiesMenu;
        }

        private void MainMenuView_OnHostButtonPressed()
        {
            StarHost();
        }

        private void MainMenuView_OnClientButtonPressed(string joinCodeText)
        {
            StartClient(joinCodeText);
        }

        private void MainMenuView_OnLobbiesButtonPressed()
        {
            (mainMenuView as IMenuView).ToggleView(false);
            OnLobbiesButtonPressed?.Invoke();
        }

        private void LobbiesController_OnCloseLobbiesMenu()
        {
            (mainMenuView as IMenuView).ToggleView(true);
        }

        private async void StarHost() => await HostSingleton.Instance.StartHostAsync();

        private async void StartClient(string joinCodeText) => await ClientSingleton.Instance.StarClientAsync(joinCodeText);
    }
}
