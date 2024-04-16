using UnityEngine;
using Zenject;

namespace GameManagement
{
    public class MusicHandler : MonoBehaviour
    {
        [SerializeField] private AudioClip[] _music;
        [SerializeField] private bool _mixMusic;

        [Inject]
        public void Construct(SoundManager soundManager)
        {
            soundManager.PlayMusicQueue(_music, _mixMusic);
        }
    }
}
