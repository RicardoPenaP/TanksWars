using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menus.MainMenu.Views
{
    public class MainMenuView : MonoBehaviour, IMenuView
    {
        [Header("Main Menu View")]

        [Header("References")]
        [SerializeField] private Button hostButton;
        [SerializeField] private Button clientButton;
        [SerializeField] private Button lobbiesButton;
        [SerializeField] private TMP_InputField joinCodeInputField;

        public event Action OnHostButtonPressed;
        public event Action<string> OnClientButtonPressed;
        public event Action OnLobbiesButtonPressed;

        private void Awake()
        {
            hostButton.onClick.AddListener(HostButtonPressed);
            clientButton.onClick.AddListener(ClientButtonPressed);
            lobbiesButton.onClick.AddListener(LobbiesButtonPressed);

        }


        private void OnDestroy()
        {
            hostButton.onClick.RemoveListener(HostButtonPressed);
            clientButton.onClick.RemoveListener(ClientButtonPressed);
        }

        private void HostButtonPressed()
        {
            OnHostButtonPressed?.Invoke();
        }

        private void ClientButtonPressed()
        {
            OnClientButtonPressed?.Invoke(joinCodeInputField.text);
        }
        private void LobbiesButtonPressed()
        {
            OnLobbiesButtonPressed?.Invoke();
        }

        public void ToggleView(bool state)
        {
            gameObject.SetActive(state);
        }
    }
}
