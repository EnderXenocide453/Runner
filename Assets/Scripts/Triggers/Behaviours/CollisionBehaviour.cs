using System;
using UnityEngine;

namespace Character.Abilities
{
    public abstract class CollisionBehaviour : MonoBehaviour
    {
        public abstract void OnHit(Collider other);
    }
}