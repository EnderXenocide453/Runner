using Character;
using UnityEngine;
using Utils;

namespace Triggers
{
    public class TurnTrigger : TriggerArea
    {
        [SerializeField] private MoveDirection[] _directions = new MoveDirection[] {MoveDirection.Forward};

        protected override void Activate(Collider other)
        {
            //Передача возможности повернуть в систему, за это отвечающую

            //Заглушка
            if (other.attachedRigidbody && other.attachedRigidbody.TryGetComponent<CharacterRun>(out var movement)) {
                movement.EnableTurn(_directions, transform.position);
            }
        }

        protected override void Deactivate(Collider other)
        {
            //Обработка проигнорированного поворота
            if (other.attachedRigidbody && other.attachedRigidbody.TryGetComponent<CharacterRun>(out var movement)) {
                movement.DisableTurn();
            }
        }
    }
}
