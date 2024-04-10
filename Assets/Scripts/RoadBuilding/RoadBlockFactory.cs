using UnityEngine;

namespace RoadBuilding
{
    [CreateAssetMenu(fileName = "New Road Factory", menuName = "RoadBuilding/Road Factory")]
    public class RoadBlockFactory : ScriptableObject
    {
        [SerializeField] private GameObject[] _roadBlockPrefabs;

        public RoadBlock GenerateBlock(Vector3 position, Quaternion rotation)
        {
            if (_roadBlockPrefabs.Length == 0) {
                Debug.LogError($"В фабрике {name} не установлен список блоков!", this);
                return null;
            }

            int id = Random.Range(0, _roadBlockPrefabs.Length);
            GameObject currentBlock = _roadBlockPrefabs[id];

            return Instantiate(currentBlock, position, rotation).GetComponent<RoadBlock>();
        }
    }
}