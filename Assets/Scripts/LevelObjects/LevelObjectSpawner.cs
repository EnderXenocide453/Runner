using System.Collections.Generic;
using UnityEngine;

namespace LevelObjects
{
    public class LevelObjectSpawner : MonoBehaviour, IAppearableObject
    {
        [SerializeField] private SpawnPoint[] _spawnPoints;
        [SerializeField] private LevelObjectFactory _levelObjectFactory;

        private List<LevelObject> _objects = new List<LevelObject>();

        public void Appear()
        {
            foreach (var point in _spawnPoints)
            {
                var objects = _levelObjectFactory.GenerateObject(point.Connectors);
                _objects.AddRange(objects);

                foreach (var obj in objects)
                    obj.onDestroyed += () => _objects.Remove(obj);
            }
        }

        public void Disappear()
        {
            foreach (LevelObject obj in _objects) {
                obj?.Disappear();
            }
        }
    }
}
