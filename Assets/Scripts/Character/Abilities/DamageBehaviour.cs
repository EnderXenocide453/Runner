using UnityEngine;

namespace Character.Abilities
{
    public class DamageBehaviour : ShootHitBehaviour
    {
        [SerializeField] private int _damage = 1;

        public override void OnHit(Collider other)
        {
            if (other.attachedRigidbody && other.attachedRigidbody.TryGetComponent<CharacterHealth>(out var health))
                health.GetDamage(_damage);
        }
    }
}