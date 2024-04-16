using Character;
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
        [Header("InjectQueue")]
        [SerializeField] private Object[] _injectQueue;

        private CharacterHandler _characterHandler;
        private ScoreHandler _scoreHandler;
        private DeathScreen _deathScreen;
        private PauseScreen _pauseScreen;

        public override void InstallBindings()
        {
            DiInstantiator instantiator = new DiInstantiator(Container);
            Container.Bind<DiInstantiator>().FromInstance(instantiator).AsSingle().NonLazy();

            InstallUI();
            InstallPlayer();
            LoadInfo();

            foreach (var item in _injectQueue) {
                Container.QueueForInject(item);
            }
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
            //ќтображение статистики персонажа
            CharacterInfoVisualizer visualizer = Container
                            .InstantiatePrefabForComponent<CharacterInfoVisualizer>(_playerCanvasPrefab);
            Container.Bind<CharacterInfoVisualizer>()
                .FromInstance(visualizer)
                .AsSingle()
                .NonLazy();

            //Ёкран смерти
            _deathScreen = Container
                .InstantiatePrefabForComponent<DeathScreen>(_deathCanvasPrefab);
            Container.Bind<DeathScreen>()
                .FromInstance(_deathScreen)
                .AsSingle()
                .NonLazy();
            _deathScreen.gameObject.SetActive(false);

            //Ёкран паузы
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
            SaveInfo info = SaveManager.LoadGame();

            _scoreHandler = new ScoreHandler(info.highScore);
        }

        private void OnPlayerDeath()
        {
            int score = (int)_characterHandler.CharacterRun.CurrentDistance;

            _scoreHandler.SetHighScore(score);
            _deathScreen.ShowHighScore(_scoreHandler.HighScore, score);
            _deathScreen.Show();

            SaveManager.SaveGame(new SaveInfo() { highScore = _scoreHandler.HighScore });
        }
    }

    public struct DiInstantiator
    {
        public DiContainer container { get; private set; }

        public DiInstantiator(DiContainer container)
        {
            this.container = container;
        }
    }
}
