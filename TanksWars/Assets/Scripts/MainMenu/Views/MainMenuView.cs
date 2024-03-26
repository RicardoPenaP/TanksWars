using System;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu.Views
{
    public class MainMenuView : MonoBehaviour
    {
        [Header("Main Menu View")]
        [SerializeField] private Button hostButton;

        public event Action OnHostButtonPressed;

        private void Awake()
        {
            hostButton.onClick.AddListener(HostButtonPressed);
        }

        private void OnDestroy()
        {
            hostButton.onClick.RemoveListener(HostButtonPressed);
        }

        private void HostButtonPressed()
        {
            OnHostButtonPressed?.Invoke();
        }
    }
}
