using Character;
using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private GameObject _player;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private GameObject _playerCanvas;
    [SerializeField] private InputManagement.InputManager _input;

    public override void InstallBindings()
    {
        InputInstall();
        InstallPlayer();
    }

    private void InputInstall()
    {
        Container.Bind<InputManagement.InputManager>().FromInstance(_input).AsSingle().NonLazy();
    }

    private void InstallPlayer()
    {
        CharacterInfoVisualizer visualizer = Container
            .InstantiatePrefabForComponent<CharacterInfoVisualizer>(_playerCanvas);

        Container.Bind<CharacterInfoVisualizer>()
            .FromInstance(visualizer)
            .AsSingle()
            .NonLazy();

        CharacterHandler characterHandler = Container
                    .InstantiatePrefabForComponent<CharacterHandler>(_player, _spawnPoint.position, _spawnPoint.rotation, null);

        Container.Bind<CharacterHandler>()
            .FromInstance(characterHandler)
            .AsSingle()
            .NonLazy();
    }
}