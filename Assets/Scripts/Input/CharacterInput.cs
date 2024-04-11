using Character;
using UnityEngine;
using Utils;
using Zenject;

namespace InputManagement
{
    public class CharacterInput : MonoBehaviour
    {
        private CharacterRun _characterRun;
        private CharacterActivities _activities;
        private InputManager _inputManager;

        [Inject]
        public void Construct(InputManager manager)
        {
            _inputManager = manager;
            _inputManager.onMoveInput += OnCharacterMoveInput;
        }

        private void Awake()
        {
            _characterRun = GetComponent<CharacterRun>();
            _activities = GetComponentInChildren<CharacterActivities>();
        }

        protected void OnCharacterMoveInput(Direction direction)
        {
            if (direction == Direction.Right || direction == Direction.Left) {
                _characterRun.TurnTo(direction);
            }
            else if (direction == Direction.Forward) {
                _activities.Jump();
            }
            else if (direction == Direction.Back) {
                _activities.Roll();
            }
        }

        private void FixedUpdate()
        {
            float deviation = _inputManager.GetDeviation();
            Debug.Log(deviation);
            Deviate(deviation);
        }

        protected void Deviate(float value)
        {
            _activities.Deviate(value);
        }
    }
}
