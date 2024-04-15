using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManagement
{
    public class SceneLoader : MonoBehaviour
    {
        private readonly int _menuIndex = 0;
        private readonly int _gameIndex = 1;
        private readonly int _loadingIndex = 2;

        public void LoadGame() => LoadScene(_gameIndex);
        public void LoadMenu() => LoadScene(_menuIndex);

        private void LoadScene(int index)
        {
            StartCoroutine(Load(index));
        }

        private IEnumerator Load(int index)
        {
            AsyncOperation loading = SceneManager.LoadSceneAsync(_loadingIndex);
            while (!loading.isDone)
                yield return null;

            loading = SceneManager.LoadSceneAsync(index);
            while (!loading.isDone)
                yield return null;
        }
    }
}
