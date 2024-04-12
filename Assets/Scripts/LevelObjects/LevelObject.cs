using Animations;
using UnityEngine;

namespace LevelObjects
{
    [RequireComponent(typeof(AppearableObjectAnimation))]
    public class LevelObject : MonoBehaviour, IAppearableObject
    {
        private AppearableObjectAnimation m_Animation;

        private void Awake()
        {
            m_Animation = GetComponent<AppearableObjectAnimation>();
        }

        public void Appear()
        {
            m_Animation.PlayAppearAnimation();
        }

        public void Disappear()
        {
            m_Animation.PlayDisappearAnimation();
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
