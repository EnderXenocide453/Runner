using LevelObjects;
using System;
using UnityEngine;

namespace Character.Abilities
{
    public abstract class Shooter : MonoBehaviour
    {
        [SerializeField] protected Transform _shootPoint;

        public abstract void Shoot();
    }
}