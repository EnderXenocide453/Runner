using UnityEngine;

namespace Animations
{
    /// <summary>
    /// Вращающийся объект
    /// </summary>
    public class Rotator : MonoBehaviour
    {
        [SerializeField] private Vector3 _axis;
        [SerializeField] private float _speed;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, transform.position + _axis);
        }

        private void Update()
        {
            transform.rotation *= Quaternion.AngleAxis(_speed * Time.deltaTime, _axis);
        }
    }
}

