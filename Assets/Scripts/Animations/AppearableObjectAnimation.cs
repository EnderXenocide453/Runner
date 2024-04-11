using UnityEngine;

namespace Animations
{
    [RequireComponent(typeof(Animator))]
    public class AppearableObjectAnimation : MonoBehaviour
    {
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
        }

        public virtual void PlayDisappearAnimation()
        {
            Animator.SetBool("exists", false);
        }
    }
}

