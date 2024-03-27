using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu.Views
{
    public class MainMenuView : MonoBehaviour
    {
        [Header("Main Menu View")]

        [Header("References")]
        [SerializeField] private Button hostButton;
        [SerializeField] private Button clientButton;
        [SerializeField] private TMP_InputField joinCodeInputField;

        public event Action OnHostButtonPressed;
        public event Action<string> OnClientButtonPressed;

        private void Awake()
        {
            hostButton.onClick.AddListener(HostButtonPressed);
            clientButton.onClick.AddListener(ClientButtonPressed);
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
    }
}
