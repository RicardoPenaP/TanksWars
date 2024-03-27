using Menus.MainMenu.Views;
using System;
using UnityEngine;

namespace Menus.MainMenu.Controllers
{
    public class LobbiesController : MonoBehaviour
    {
        [Header("Lobbies Controller")]

        [Header("References")]
        [SerializeField] private LobbiesView lobbiesView;
        [SerializeField] private MainMenuController mainMenuController;

        public event Action OnCloseLobbiesMenu;

        private void Start()
        {
            lobbiesView.OnCloseButtonPressed += LobbiesView_OnCloseButtonPressed;
            mainMenuController.OnLobbiesButtonPressed += MainMenuController_OnLobbiesButtonPressed;
        }

        private void OnDestroy()
        {
            lobbiesView.OnCloseButtonPressed -= LobbiesView_OnCloseButtonPressed;
            mainMenuController.OnLobbiesButtonPressed -= MainMenuController_OnLobbiesButtonPressed;
        }

        private void LobbiesView_OnCloseButtonPressed()
        {
            ToggleView(false);
            OnCloseLobbiesMenu?.Invoke();
        }

        private void MainMenuController_OnLobbiesButtonPressed()
        {
            ToggleView(true);
        }

        public void ToggleView(bool state)
        {
            (lobbiesView as IMenuView).ToggleView(state);
        }
    }
}
