using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(Animator))]
    public class CharacterAnimation : MonoBehaviour
    {
        private Animator _animator;
        private ActivityState _activityState;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Jump()
        {
            if (!SetActivityState(ActivityState.Jump))
                return; 
            
            _animator.SetTrigger("jump");
        }

        public void Roll()
        {
            if (!SetActivityState(ActivityState.Roll))
                return;

            _animator.SetTrigger("roll");
        }

        public void Death()
        {
            if (!SetActivityState(ActivityState.Death))
                return;

            _animator.SetTrigger("death");
        }

        public void ResetState()
        {
            _activityState = ActivityState.None;
        }

        private bool SetActivityState(ActivityState activityState)
        {
            if (_activityState == activityState)
                return false;

            _activityState = activityState;
            return true;
        }
    }

    public enum ActivityState
    {
        None,
        Jump,
        Roll,
        Death
    }
}

