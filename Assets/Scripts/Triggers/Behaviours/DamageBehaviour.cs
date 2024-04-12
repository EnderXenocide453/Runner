using UnityEngine;

namespace Character.Abilities
{
    public class DamageBehaviour : CollisionBehaviour
    {
        [SerializeField] private int _damage = 1;
        [SerializeField] private bool _permaDeath = false;

        public override void OnHit(Collider other)
        {
            if (other.attachedRigidbody && other.attachedRigidbody.TryGetComponent<CharacterHealth>(out var health)) {
                if (!_permaDeath)
                    health.GetDamage(_damage);
                else
                    health.PermaDeath();
            }
        }
    }
}