using UnityEngine;

namespace Character.Abilities
{
    public class BulletShooter : Shooter
    {
        [SerializeField] private GameObject _bullet;

        public override void Shoot()
        {
            Instantiate(_bullet, ShootPoint.position, ShootPoint.rotation);
        }
    }
}