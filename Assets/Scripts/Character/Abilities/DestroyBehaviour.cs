using LevelObjects;
using UnityEngine;

namespace Character.Abilities
{
    public class DestroyBehaviour : ShootHitBehaviour
    {
        public override void OnHit(Collider other)
        {
            if (other.attachedRigidbody && other.attachedRigidbody.TryGetComponent<LevelObject>(out var obj))
                obj.Destroy();
        }
    }
}