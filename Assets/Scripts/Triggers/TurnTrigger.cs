using UnityEngine;
using Utils;

namespace Triggers
{
    public class TurnTrigger : TriggerArea
    {
        [SerializeField] private Direction _direction;

        protected override void Activate(Collider other)
        {
            //Передача возможности повернуть в систему, за это отвечающую

            //Заглушка
            if (other.attachedRigidbody && other.attachedRigidbody.TryGetComponent<CharacterMovement>(out var movement)) {
                movement.TurnTo(Directions.GetRotationFromDirection(_direction), transform.position);
            }
        }

        protected override void Deactivate(Collider other)
        {
            //Обработка проигнорированного поворота
        }
    }
}
