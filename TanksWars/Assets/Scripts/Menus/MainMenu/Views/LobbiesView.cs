using System;
using UnityEngine;
using UnityEngine.UI;

namespace Menus.MainMenu.Views
{
    public class LobbiesView : MonoBehaviour, IMenuView
    {
        [Header("Lobbies View")]
        [SerializeField] private Button closeButton;
        [SerializeField] private Button refreshButton;

        public event Action OnCloseButtonPressed;
        public event Action OnRefreshButtonPressed;

        private void Awake()
        {
            closeButton.onClick.AddListener(CloseButtonPressed);
            refreshButton.onClick.AddListener(RefreshButtonPressed);
        }


        private void OnDestroy()
        {
            closeButton.onClick.RemoveListener(CloseButtonPressed);
            refreshButton.onClick.RemoveListener(RefreshButtonPressed);
        }

        private void CloseButtonPressed()
        {
            OnCloseButtonPressed?.Invoke();
        }

        private void RefreshButtonPressed()
        {
            OnRefreshButtonPressed?.Invoke();
        }

        public void ToggleView(bool state)
        {
            gameObject.SetActive(state);
        }
    }
}
