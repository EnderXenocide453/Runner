using UnityEngine;
using UnityEngine.Events;

namespace Animations
{
    /// <summary>
    /// Общий класс для анимации появляющихся и исчезающих объектов
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class AppearableObjectAnimation : MonoBehaviour
    {
        [SerializeField] private UnityEvent onAppear;
        [SerializeField] private UnityEvent onDisappear;
        private Animator _animation;

        private Animator Animator
        {
            get
            {
                if (_animation == null )
                    _animation = GetComponent<Animator>();
                return _animation;
            }
        }

        public virtual void PlayAppearAnimation()
        {
            Animator.SetBool("exists", true);
            onAppear?.Invoke();
        }

        public virtual void PlayDisappearAnimation()
        {
            Animator.SetBool("exists", false);
            onDisappear?.Invoke();
        }
    }
}

