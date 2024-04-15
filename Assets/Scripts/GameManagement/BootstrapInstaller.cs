using UnityEngine;
using Zenject;

namespace GameManagement
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private SceneLoader _sceneLoader;
        [SerializeField] private SoundManager _soundManager;

        public override void InstallBindings()
        {
            Container.Bind<SceneLoader>().FromInstance(_sceneLoader).AsSingle().NonLazy();
            Container.Bind<SoundManager>().FromInstance(_soundManager).AsSingle().NonLazy();
        }
    }
}