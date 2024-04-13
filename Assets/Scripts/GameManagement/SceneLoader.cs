using UnityEngine.SceneManagement;

namespace GameManagement
{
    public static class SceneLoader
    {
        private static readonly int _menuIndex = 0;
        private static readonly int _gameIndex = 1;

        public static void LoadGame() => SceneManager.LoadScene(_gameIndex);
        public static void LoadMenu() => SceneManager.LoadScene(_menuIndex);
    }
}
