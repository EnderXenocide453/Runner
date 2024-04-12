using UnityEngine;

namespace LevelObjects
{
    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField, Range(1, 5)] private int _objectsCount = 1;
        [SerializeField] private float _width = 1.5f;
        [SerializeField] private float _objectSize = 0.5f;

        public int Count => _objectsCount;
        public float Width => _width - _objectSize;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, new Vector3(_width, 1, 0.2f));
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(transform.position, new Vector3(Width, 1, 0.2f));
        }
    }
}
