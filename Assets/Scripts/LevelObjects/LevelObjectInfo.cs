using System;
using UnityEngine;

namespace LevelObjects
{
    [Serializable]
    public struct LevelObjectInfo
    {
        public GameObject Prefab;
        public bool IsSolid;
    }
}
