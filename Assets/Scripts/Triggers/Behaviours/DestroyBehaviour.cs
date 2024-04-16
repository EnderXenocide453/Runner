using LevelObjects;
using UnityEngine;

namespace Character.Abilities
{
    /// <summary>
    /// Поведение уничтожения объектов
    /// </summary>
    public class DestroyBehaviour : CollisionBehaviour
    {
        public override void OnHit(Collider other)
        {
            if (other.attachedRigidbody && other.attachedRigidbody.TryGetComponent<LevelObject>(out var obj))
                obj.DestroyLevelObject(withSound: true);
        }
    }
}