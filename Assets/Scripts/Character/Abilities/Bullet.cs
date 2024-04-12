using Triggers;
using UnityEngine;

namespace Character.Abilities
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : TriggerArea
    {
        [SerializeField] private float _speed;
        [SerializeField] private ShootHitBehaviour[] _behaviours;
        private Rigidbody _body;

        private void Awake()
        {
            _body = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            _body.MovePosition(_body.position + transform.TransformDirection(Vector3.forward) * _speed * Time.fixedDeltaTime);
        }

        protected override void Activate(Collider other)
        {
            foreach (var behaviour in _behaviours) {
                behaviour?.OnHit(other);
            }
        }
    }
}