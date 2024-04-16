using UnityEngine;
using Zenject;

namespace GameManagement
{
    /// <summary>
    /// Установщик контекста проекта
    /// </summary>
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private SceneLoader _sceneLoader;
        [SerializeField] private SoundManager _soundManager;
        [Header("Input")]
        [SerializeField] private InputManagement.InputManager _input;

        public override void InstallBindings()
        {
            ConfigInfo config = SaveManager.LoadConfig();
            _soundManager.SetSoundConfig(config.soundInfo);

            Container.Bind<SceneLoader>().FromInstance(_sceneLoader).AsSingle().NonLazy();
            Container.Bind<SoundManager>().FromInstance(_soundManager).AsSingle().NonLazy();

            InputInstall();
        }

        private void InputInstall()
        {
            ConfigInfo config = SaveManager.LoadConfig();
            _input.SetConfig(config.inputInfo);

            Container.Bind<InputManagement.InputManager>().FromInstance(_input).AsSingle().NonLazy();
        }
    }
}
