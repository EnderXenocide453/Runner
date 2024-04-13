using GameManagement;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DeathScreen : MonoBehaviour
    {
        [SerializeField] private Text _scoreField;
        [SerializeField] private Text _highScoreField;
        [SerializeField] private string _newHighScoreReplic = "Новый рекорд!";
        [SerializeField] private string _highScoreDescription = "Рекорд: ";
        [SerializeField] private string _scoreDescription = "Очков набрано: ";

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void ShowHighScore(float highScore, float currScore)
        {
            _scoreField.text = _scoreDescription + currScore;
            
            if (highScore > currScore) {
                _highScoreField.text = _highScoreDescription + highScore;
            } else {
                _highScoreField.text = _newHighScoreReplic;
            }
        }

        public void ToMainMenu()
        {
            SceneLoader.LoadMenu();
        }

        public void Restart()
        {
            SceneLoader.LoadGame();
        }
    }
}

