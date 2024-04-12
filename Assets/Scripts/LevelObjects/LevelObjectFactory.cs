using System.Collections.Generic;
using UnityEngine;

namespace LevelObjects
{
    [CreateAssetMenu(fileName = "New Level Objects Factory", menuName = "LevelObjects/LevelObjectFactory")]
    public class LevelObjectFactory : ScriptableObject
    {
        [SerializeField] private LevelObjectInfo[] _levelObjects;
        private List<LevelObjectInfo> _nonSolidObjects;

        private void OnValidate()
        {
            _nonSolidObjects = new List<LevelObjectInfo>();

            foreach (var levelObject in _levelObjects) {
                if (levelObject.IsSolid)
                    continue;
                _nonSolidObjects.Add(levelObject);
            }
        }

        public LevelObject[] GenerateObject(Transform[] connectors)
        {
            var objects = new LevelObject[connectors.Length];
            int solidCount = 0;

            for (int i = 0; i < objects.Length; i++) {
                bool allowSolid = solidCount + 1 < objects.Length;
                objects[i] = CreateRandomObject(connectors[i], allowSolid, out bool isSolid);

                if (isSolid)
                    solidCount++;
            }

            return objects;
        }

        private LevelObject CreateRandomObject(Transform parent, bool allowSolid, out bool isSolid)
        {
            LevelObjectInfo info;

            if (allowSolid)
                info = _levelObjects[Random.Range(0, _levelObjects.Length)];
            else
                info = _nonSolidObjects[Random.Range(0, _nonSolidObjects.Count)];

            isSolid = info.IsSolid;
            LevelObject obj = Instantiate(info.Prefab, parent).GetComponent<LevelObject>();
            return obj;
        }
    }
}
