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

        public LevelObject[] GenerateObjects(Transform parent, int count, float placeWidth)
        {
            var objects = new LevelObject[count];
            int solidCount = 0;
            Vector3 startPosition = Vector3.zero;
            float step = 0;

            if (count > 1) {
                startPosition = new Vector3(-placeWidth / 2, 0, 0);
                step = placeWidth / (count - 1);
            }

            for (int i = 0; i < count; i++) {
                Vector3 position = startPosition + Vector3.right * i * step;
                objects[i] = CreateRandomObject(position, parent, solidCount < count, out bool isSolid);

                if (isSolid)
                    solidCount++;
            }

            return objects;
        }

        private LevelObject CreateRandomObject(Vector3 localPosition, Transform parent, bool allowSolid, out bool isSolid)
        {
            LevelObjectInfo info;

            if (allowSolid)
                info = _levelObjects[Random.Range(0, _levelObjects.Length)];
            else
                info = _nonSolidObjects[Random.Range(0, _nonSolidObjects.Count)];

            isSolid = info.IsSolid;
            LevelObject obj = Instantiate(info.Prefab, parent).GetComponent<LevelObject>();
            obj.transform.localPosition = localPosition;
            return obj;
        }
    }
}
