using Animations;
using GameManagement;
using System;
using UnityEngine;
using Zenject;

namespace LevelObjects
{
    [RequireComponent(typeof(AppearableObjectAnimation))]
    public class LevelObject : MonoBehaviour, IAppearableObject
    {
        [SerializeField] private SoundType _destructionSound = SoundType.levelObjectDestruction;
        private AppearableObjectAnimation m_Animation;
        private SoundManager m_SoundManager;

        public event Action onDestroyed;

        [Inject]
        public void Construct(SoundManager soundManager)
        {
            m_SoundManager = soundManager;
        }

        private void Awake()
        {
            m_Animation = GetComponent<AppearableObjectAnimation>();
        }

        private void Start()
        {
            Appear();
        }

        public void Appear()
        {
            m_Animation.PlayAppearAnimation();
        }

        public void Disappear()
        {
            m_Animation.PlayDisappearAnimation();
        }

        public void DestroyLevelObject(bool withSound = false)
        {
            if (withSound)
                m_SoundManager?.PlaySound(_destructionSound);

            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            onDestroyed?.Invoke();
        }
    }
}
