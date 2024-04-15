using GameManagement;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Text _scoreField;
        [SerializeField] private string _fieldDescription = "Лучший рекорд: ";

        private SceneLoader _sceneLoader;

        [Inject]
        public void Construct(SceneLoader sceneLoader) => _sceneLoader = sceneLoader;

        private void Awake()
        {
            SaveInfo info = SaveManager.Load();
            _scoreField.text = _fieldDescription + info.highScore;
        }

        public void StartGame()
        {
            _sceneLoader.LoadGame();
        }
    }
}

