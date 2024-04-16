﻿using LevelObjects;
using UnityEngine;

namespace Character.Abilities
{
    public class DestroyBehaviour : CollisionBehaviour
    {
        public override void OnHit(Collider other)
        {
            if (other.attachedRigidbody && other.attachedRigidbody.TryGetComponent<LevelObject>(out var obj))
                obj.DestroyLevelObject(withSound: true);
        }
    }
}