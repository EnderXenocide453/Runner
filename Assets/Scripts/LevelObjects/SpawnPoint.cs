using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

namespace LevelObjects
{
    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField] private List<Transform> _connectors;

        public Transform[] Connectors => _connectors.ToArray();

        private void OnDrawGizmos()
        {
            foreach (Transform t in _connectors) {
                Gizmos.DrawSphere(t.position, 0.05f);
            }
        }

        public void UpdateChildren()
        {
            _connectors = new List<Transform>();
            for (int i = 0; i < transform.childCount; i++) {
                _connectors.Add(transform.GetChild(i));
            }
        }

        public void AddChild()
        {
            Transform child = new GameObject("Connector").transform;
            child.SetParent(transform);
            child.localPosition = Vector3.zero;
            child.localRotation = Quaternion.identity;

            _connectors.Add(child);
        }
    }
}
