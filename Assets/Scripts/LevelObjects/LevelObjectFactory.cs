using GameManagement;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LevelObjects
{
    /// <summary>
    /// Фабрика игровых объектов. Выполнена в качестве наследника ScriptableObject чтобы в дальнейшем
    /// реализовать возможность смены заранее подготовленных фабрик для разнообразия игрового процесса.
    /// Например, для увеличения сложности ловушек со временем
    /// </summary>
    [CreateAssetMenu(fileName = "New Level Objects Factory", menuName = "LevelObjects/LevelObjectFactory")]
    public class LevelObjectFactory : ScriptableObject
    {
        [SerializeField] private LevelObjectInfo[] _levelObjects;
        private List<LevelObjectInfo> _nonSolidObjects;
        private DiInstantiator _instantiator;

        [Inject]
        public void Construct(DiInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

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
                //Если это последний элемент и все предыдущие были сплошными, запретить использование сплошного
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
            LevelObject obj = _instantiator.container.InstantiatePrefabForComponent<LevelObject>(info.Prefab, parent);
            return obj;
        }
    }
}
