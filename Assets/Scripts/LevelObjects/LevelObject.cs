using Animations;
using System;
using UnityEngine;

namespace LevelObjects
{
    [RequireComponent(typeof(AppearableObjectAnimation))]
    public class LevelObject : MonoBehaviour, IAppearableObject
    {
        private AppearableObjectAnimation m_Animation;

        public event Action onDestroyed;

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
            //m_Animation.PlayAppearAnimation();
        }

        public void Disappear()
        {
            Destroy();
            //m_Animation.PlayDisappearAnimation();
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            onDestroyed?.Invoke();
        }
    }
}
