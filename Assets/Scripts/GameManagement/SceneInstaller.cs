using Character;
using System;
using UI;
using UnityEngine;
using Zenject;

namespace GameManagement
{
    public class SceneInstaller : MonoInstaller
    {
        [Header("Player")]
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private Transform _spawnPoint;
        [Header("UI")]
        [SerializeField] private GameObject _playerCanvasPrefab;
        [SerializeField] private GameObject _deathCanvasPrefab;
        [SerializeField] private GameObject _pauseCanvasPrefab;
        [Header("Input")]
        [SerializeField] private InputManagement.InputManager _input;

        private CharacterHandler _characterHandler;
        private ScoreHandler _scoreHandler;
        private DeathScreen _deathScreen;
        private PauseScreen _pauseScreen;

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
            //����������� ���������� ���������
            CharacterInfoVisualizer visualizer = Container
                            .InstantiatePrefabForComponent<CharacterInfoVisualizer>(_playerCanvasPrefab);
            Container.Bind<CharacterInfoVisualizer>()
                .FromInstance(visualizer)
                .AsSingle()
                .NonLazy();

            //����� ������
            _deathScreen = Container
                .InstantiatePrefabForComponent<DeathScreen>(_deathCanvasPrefab);
            Container.Bind<DeathScreen>()
                .FromInstance(_deathScreen)
                .AsSingle()
                .NonLazy();
            _deathScreen.gameObject.SetActive(false);

            //����� �����
            _pauseScreen = Container
                .InstantiatePrefabForComponent<PauseScreen>(_pauseCanvasPrefab);
            Container.Bind<PauseScreen>()
                .FromInstance(_pauseScreen)
                .AsSingle()
                .NonLazy();
            _pauseScreen.Hide();
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
