using GameManagement;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Text _scoreField;
        [SerializeField] private string _fieldDescription = "Лучший рекорд: ";

        private void Awake()
        {
            SaveInfo info = SaveManager.Load();
            _scoreField.text = _fieldDescription + info.highScore;
        }

        public void StartGame()
        {
            SceneLoader.LoadGame();
        }
    }
}

