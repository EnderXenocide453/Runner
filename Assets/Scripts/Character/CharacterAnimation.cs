using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(Animator))]
    public class CharacterAnimation : MonoBehaviour
    {
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Jump()
        {
            _animator.SetTrigger("jump");
        }

        public void Roll()
        {
            _animator.SetTrigger("roll");
        }
    }
}

