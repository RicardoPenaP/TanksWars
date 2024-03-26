using MainMenu.Views;
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
                
        }

        private async void MainMenuView_OnHostButtonPressed()
        {
            await StarHost();
        }

        private async Task StarHost()
        {
            await HostSingleton.Instance.StartHostAsync();
        }
    }
}
