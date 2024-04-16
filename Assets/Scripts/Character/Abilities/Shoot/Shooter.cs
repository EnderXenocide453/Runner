using GameManagement;
using LevelObjects;
using System;
using UnityEngine;

namespace Character.Abilities
{
    public abstract class Shooter : MonoBehaviour
    {
        [SerializeField] private Transform _shootPoint;
        [SerializeField] protected SoundType _shootSound;

        public Transform ShootPoint => _shootPoint ? _shootPoint : transform;
        public SoundType ShootSound => _shootSound;

        public abstract void Shoot();
    }
}