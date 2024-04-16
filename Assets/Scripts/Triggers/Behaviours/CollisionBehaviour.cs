using System;
using UnityEngine;

namespace Character.Abilities
{
    /// <summary>
    /// Поведение при столкновении. Может использоваться совместно с любыми триггерами и коллайдерами
    /// Позволяет настроить поведение коллайдера
    /// </summary>
    public abstract class CollisionBehaviour : MonoBehaviour
    {
        public abstract void OnHit(Collider other);
    }
}