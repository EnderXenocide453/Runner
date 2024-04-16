using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameManagement
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _soundSource;
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private SoundsHolder _soundHolder;

        public bool MusicMute 
        {
            get => _musicSource.mute;
            set => _musicSource.mute = value; 
        }
        public float MusicVolume 
        {
            get => _musicSource.volume;
            set => _musicSource.volume = value; 
        }
        public bool SoundMute 
        { 
            get => _soundSource.mute;
            set => _soundSource.mute = value; 
        }
        public float SoundVolume 
        { 
            get => _soundSource.volume; 
            set => _soundSource.volume = value; 
        }

        public SoundInfo GetSoundInfo()
        {
            return new SoundInfo()
            {
                isMusicActive = !_musicSource.mute,
                musicVolume = _musicSource.volume,
                isSoundActive = !_soundSource.mute,
                soundVolume = _soundSource.volume
            };
        }

        public void SetSoundConfig(SoundInfo soundInfo)
        {
            _musicSource.mute = !soundInfo.isMusicActive;
            _musicSource.volume = soundInfo.musicVolume;
            _soundSource.mute = !soundInfo.isSoundActive;
            _soundSource.volume = soundInfo.soundVolume;
        }

        public void PlaySound(SoundType type)
        {
            if (_soundHolder.TryGetSound(type, out var sound)) {
                _soundSource.PlayOneShot(sound);
            }
        }

        public void PlayMusicQueue(AudioClip[] clips, bool mix = false)
        {
            var queue = new AudioClip[clips.Length];
            
            if (mix) {
                List<AudioClip> freeClips = new List<AudioClip>(clips);

                for (int i = 0; i < clips.Length; i++) {
                    int id = Random.Range(0, freeClips.Count);
                    queue[i] = freeClips[id];
                    freeClips.RemoveAt(id);
                }
            } 
            else {
                clips.CopyTo(queue, 0);
            }

            StopAllCoroutines();
            StartCoroutine(PlayQueue(queue));
        }

        public IEnumerator PlayQueue(AudioClip[] queue)
        {
            int index = 0;

            while (true) {
                _musicSource.clip = queue[index];
                _musicSource.Play();

                yield return new WaitForSecondsRealtime(queue[index].length);

                index = (index + 1) % queue.Length;
            }
        }
    }
}
