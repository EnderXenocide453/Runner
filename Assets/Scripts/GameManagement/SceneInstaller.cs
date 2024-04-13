using Character;
using System;
using UI;
using UnityEngine;
using Zenject;

namespace GameManagement
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private GameObject _playerCanvasPrefab;
        [SerializeField] private GameObject _deathCanvasPrefab;
        [SerializeField] private InputManagement.InputManager _input;

        private CharacterHandler _characterHandler;
        private DeathScreen _deathScreen;
        private ScoreHandler _scoreHandler;

        public override void InstallBindings()
        {
            InputInstall();
            InstallUI();
            InstallPlayer();
            LoadInfo();
        }

        private void InputInstall()
        {
            Container.Bind<InputManagement.InputManager>().FromInstance(_input).AsSingle().NonLazy();
        }

        private void InstallPlayer()
        {
            _characterHandler = Container
                        .InstantiatePrefabForComponent<CharacterHandler>(_playerPrefab, _spawnPoint.position, _spawnPoint.rotation, null);
            Container.Bind<CharacterHandler>()
                .FromInstance(_characterHandler)
                .AsSingle()
                .NonLazy();

            _characterHandler.onDeath += OnPlayerDeath;
        }

        private void InstallUI()
        {
            CharacterInfoVisualizer visualizer = Container
                            .InstantiatePrefabForComponent<CharacterInfoVisualizer>(_playerCanvasPrefab);
            Container.Bind<CharacterInfoVisualizer>()
                .FromInstance(visualizer)
                .AsSingle()
                .NonLazy();

            _deathScreen = Container
                .InstantiatePrefabForComponent<DeathScreen>(_deathCanvasPrefab);
            Container.Bind<DeathScreen>()
                .FromInstance(_deathScreen)
                .AsSingle()
                .NonLazy();
            _deathScreen.gameObject.SetActive(false);
        }

        private void LoadInfo()
        {
            SaveInfo info = SaveManager.Load();

            _scoreHandler = new ScoreHandler(info.highScore);
        }

        private void OnPlayerDeath()
        {
            int score = (int)_characterHandler.CharacterRun.CurrentDistance;

            _scoreHandler.SetHighScore(score);
            _deathScreen.ShowHighScore(_scoreHandler.HighScore, score);
            _deathScreen.Show();

            SaveManager.Save(new SaveInfo() { highScore = _scoreHandler.HighScore });
        }
    }
}
