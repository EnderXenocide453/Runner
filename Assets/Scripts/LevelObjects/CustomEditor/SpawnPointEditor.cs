using UnityEditor;
using UnityEngine;

namespace LevelObjects
{
#if UNITY_EDITOR
    [CustomEditor(typeof(SpawnPoint))]
    public class SpawnPointEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            SpawnPoint point = (SpawnPoint)target;
            if (GUILayout.Button("Create connector")) {
                point.AddChild();
            }
            if (GUILayout.Button("Update")) {
                point.UpdateChildren();
            }
        }
    }
#endif
}

