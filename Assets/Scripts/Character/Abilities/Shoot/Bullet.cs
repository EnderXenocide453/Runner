using Triggers;
using UnityEngine;

namespace Character.Abilities
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : BehaviourTriggerArea
    {
        [SerializeField] private float _speed;
        private Rigidbody _body;

        private void Awake()
        {
            _body = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            _body.MovePosition(_body.position + transform.TransformDirection(Vector3.forward) * _speed * Time.fixedDeltaTime);
        }
    }
}