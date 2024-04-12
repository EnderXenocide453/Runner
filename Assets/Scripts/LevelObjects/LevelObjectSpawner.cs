using UnityEngine;

namespace LevelObjects
{
    public class LevelObjectSpawner : MonoBehaviour, IAppearableObject
    {
        [SerializeField] private Transform[] _spawnPoints;

        public void Appear()
        {
            throw new System.NotImplementedException();
        }

        public void Disappear()
        {
            throw new System.NotImplementedException();
        }
    }
}
