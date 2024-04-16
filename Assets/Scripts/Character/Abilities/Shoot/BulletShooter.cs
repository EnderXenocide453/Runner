using UnityEngine;

namespace Character.Abilities
{
    /// <summary>
    /// Класс стрельбы пулями
    /// </summary>
    public class BulletShooter : Shooter
    {
        [SerializeField] private GameObject _bullet;

        public override void Shoot()
        {
            Instantiate(_bullet, ShootPoint.position, ShootPoint.rotation);
        }
    }
}