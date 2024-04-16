using GameManagement;
using InputManagement;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class SettingsUI : MonoBehaviour
    {
        [SerializeField] private Toggle _musicToggle, _soundToggle;
        [SerializeField] private Slider _musicSlider, _soundSlider, _sensitivitySlider;
        private SoundManager _soundManager;
        private InputManager _inputManager;

        [Inject]
        public void Construct(SoundManager soundManager, InputManager inputManager)
        {
            _soundManager = soundManager;
            _inputManager = inputManager;
            UpdateUI();

            _musicToggle.onValueChanged.AddListener((bool toggle) => _soundManager.MusicMute = !toggle);
            _soundToggle.onValueChanged.AddListener((bool toggle) => _soundManager.SoundMute = !toggle);
            _musicSlider.onValueChanged.AddListener((float value) => _soundManager.MusicVolume = value);
            _soundSlider.onValueChanged.AddListener((float value) => _soundManager.SoundVolume = value);
            _sensitivitySlider.onValueChanged.AddListener((float value) => _inputManager.DeviationSensitivity = value);
        }

        private void OnEnable()
        {
            UpdateUI();
        }

        private void OnDisable()
        {
            SaveManager.SaveConfig(new ConfigInfo()
            {
                soundInfo = _soundManager.GetSoundInfo(),
                inputInfo = _inputManager.GetConfig()
            });
        }

        private void UpdateUI()
        {
            _musicToggle.isOn = !_soundManager.MusicMute;
            _soundToggle.isOn = !_soundManager.SoundMute;
            _musicSlider.value = _soundManager.MusicVolume;
            _soundSlider.value = _soundManager.SoundVolume;
            _sensitivitySlider.value = _inputManager.DeviationSensitivity;
        }
    }
}