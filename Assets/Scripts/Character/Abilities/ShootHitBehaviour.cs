using System;
using UnityEngine;

namespace Character.Abilities
{
    public abstract class ShootHitBehaviour : MonoBehaviour
    {
        public abstract void OnHit(Collider other);
    }
}