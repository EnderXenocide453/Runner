using Character;
using UnityEngine;

namespace Triggers
{
    public class DamageTrigger : TriggerArea
    {
        [SerializeField] private int _damage = 1;
        [SerializeField] private bool _permaDeath = false;

        protected override void Activate(Collider other)
        {
            if (other.attachedRigidbody && other.attachedRigidbody.TryGetComponent<CharacterHealth>(out var health)) {
                if (_permaDeath)
                    health.PermaDeath();
                else
                    health.GetDamage(_damage);
            }
        }

        protected override void Deactivate(Collider other) { }
    }
}
