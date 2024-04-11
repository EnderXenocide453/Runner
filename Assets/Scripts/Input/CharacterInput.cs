using Character;
using UnityEngine;
using Utils;
using Zenject;

namespace InputManagement
{
    public class CharacterInput : MonoBehaviour
    {
        private CharacterHandler _character;
        private InputManager _inputManager;

        [Inject]
        public void Construct(InputManager manager)
        {
            _inputManager = manager;
            _inputManager.onMoveInput += OnCharacterMoveInput;

            _character = GetComponent<CharacterHandler>();
            _character.onDeath += () =>
            {
                enabled = false;
                _inputManager.enabled = false;
            };
        }

        protected void OnCharacterMoveInput(MoveDirection direction)
        {
            if (direction == MoveDirection.Right || direction == MoveDirection.Left) {
                _character.CharacterRun.TurnTo(direction);
            }
            else if (direction == MoveDirection.Forward) {
                _character.Activity.Jump();
            }
            else if (direction == MoveDirection.Back) {
                _character.Activity.Roll();
            }
        }

        private void Update()
        {
            float deviation = _inputManager.GetDeviation();
            _character.Activity.Deviate(deviation);
        }
    }
}
