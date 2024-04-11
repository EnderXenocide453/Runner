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

        protected void OnCharacterMoveInput(MoveDirection direction)
        {
            if (direction == MoveDirection.Right || direction == MoveDirection.Left) {
                _characterRun.TurnTo(direction);
            }
            else if (direction == MoveDirection.Forward) {
                _activities.Jump();
            }
            else if (direction == MoveDirection.Back) {
                _activities.Roll();
            }
        }

        private void Update()
        {
            float deviation = _inputManager.GetDeviation();
            Debug.Log(deviation);
            _activities.Deviate(deviation);
        }
    }
}
