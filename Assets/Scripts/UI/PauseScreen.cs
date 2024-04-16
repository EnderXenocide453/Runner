using GameManagement;
using UnityEngine;
using Zenject;

namespace UI
{
    public class PauseScreen : MonoBehaviour
    {
        private InputManagement.InputManager _inputManager;
        private SceneLoader _sceneLoader;

        [Inject]
        public void Construct(InputManagement.InputManager inputManager, SceneLoader sceneLoader)
        {
            _inputManager = inputManager;
            _inputManager.onPause += Toggle;

            _sceneLoader = sceneLoader;
        }

        private void OnApplicationPause(bool pause)
        {
            Show();
        }

        public void Show()
        {
            gameObject.SetActive(true);
            Time.timeScale = 0;
            _inputManager.enabled = false;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            Time.timeScale = 1;
            _inputManager.enabled = true;
        }

        public void ToMainMenu()
        {
            _sceneLoader.LoadMenu();
        }

        private void Toggle()
        {
            if (gameObject.activeSelf)
                Hide();
            else
                Show();
        }

        private void OnDestroy()
        {
            _inputManager.onPause -= Toggle;
        }
    }
}

