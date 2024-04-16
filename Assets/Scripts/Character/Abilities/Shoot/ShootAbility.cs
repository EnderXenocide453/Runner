using GameManagement;
using UnityEngine;

namespace Character.Abilities
{
    public class ShootAbility : BaseAbility
    {
        [SerializeField] private Shooter _shooter;

        protected override SoundType AbilitySound => _shooter.ShootSound;

        protected override void UseAbility()
        {
            _shooter.Shoot();
        }
    }
}
