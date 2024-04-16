using GameManagement;
using UnityEngine;
using Zenject;

namespace RoadBuilding
{
    /// <summary>
    /// Фабрика блоков дороги. Выполнена в качестве наследника ScriptableObject чтобы в дальнейшем
    /// реализовать возможность смены заранее подготовленных фабрик для разнообразия игрового процесса.
    /// Например, для биомов со временем
    /// </summary>
    [CreateAssetMenu(fileName = "New Road Factory", menuName = "RoadBuilding/Road Factory")]
    public class RoadBlockFactory : ScriptableObject
    {
        [SerializeField] private GameObject[] _roadBlockPrefabs;
        private DiInstantiator _instantiator;

        [Inject]
        public void Construct(DiInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        public RoadBlock GenerateBlock(Vector3 position, Quaternion rotation)
        {
            if (_roadBlockPrefabs.Length == 0) {
                Debug.LogError($"В фабрике {name} не установлен список блоков!", this);
                return null;
            }

            int id = Random.Range(0, _roadBlockPrefabs.Length);
            GameObject currentBlock = _roadBlockPrefabs[id];

            return _instantiator.container.InstantiatePrefabForComponent<RoadBlock>(currentBlock, position, rotation, null);
        }
    }
}