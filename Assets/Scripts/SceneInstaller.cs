using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private InputManagement.InputManager _input;

    public override void InstallBindings()
    {
        Container.Bind<InputManagement.InputManager>().FromInstance(_input).AsSingle().NonLazy();
    }
}