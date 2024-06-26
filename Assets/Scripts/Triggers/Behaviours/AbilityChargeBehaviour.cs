﻿using UnityEngine;

namespace Character.Abilities
{
    /// <summary>
    /// Поведение заряда способности
    /// </summary>
    public class AbilityChargeBehaviour : CollisionBehaviour
    {
        public override void OnHit(Collider other)
        {
            if (other.attachedRigidbody && other.attachedRigidbody.TryGetComponent<CharacterAbility>(out var ability))
                ability.AddCharge();
        }
    }
}