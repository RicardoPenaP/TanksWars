using UnityEngine.SceneManagement;
using static SceneManagement.GameScenes;

namespace SceneManagement
{
    public static class GameScenesManager
    {
        private static GameScenes currentScene = NetBootstrap;

        public static GameScenes CurrentScene => currentScene;

        public static void ChangeScene(GameScenes gameScene)
        {
            if (currentScene == gameScene)
            {
                return;
            }

            currentScene = gameScene;
            SceneManager.LoadScene((int)currentScene);
        }
    }
}
