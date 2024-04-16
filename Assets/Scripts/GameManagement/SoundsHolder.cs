using System.Collections.Generic;
using UnityEngine;

namespace GameManagement
{
    /// <summary>
    /// Хранилище списка звуков. Преобразует список в словарь для дальнейшего взаимодействия
    /// </summary>
    [System.Serializable]
    public class SoundsHolder
    {
        [SerializeField] private Sound[] _sounds;
        private Dictionary<SoundType, AudioClip> _soundsDict;

        private Dictionary<SoundType, AudioClip> Sounds
        {
            get
            {
                if (_soundsDict == null ) {
                    Init();
                }

                return _soundsDict;
            }
        }

        public void Init()
        {
            _soundsDict = new Dictionary<SoundType, AudioClip>();

            foreach (Sound sound in _sounds) {
                _soundsDict.TryAdd(sound.type, sound.audioClip);
            }

            //Если по случайности добавлен звук для его отсутствия, удаляем
            _soundsDict.Remove(SoundType.none);
        }

        public bool TryGetSound(SoundType type, out AudioClip sound) => Sounds.TryGetValue(type, out sound);

        [System.Serializable]
        struct Sound
        {
            public SoundType type;
            public AudioClip audioClip;
        }
    }
}
