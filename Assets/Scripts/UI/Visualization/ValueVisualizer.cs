using UnityEngine;

namespace UI.Visualization
{
    /// <summary>
    /// ������������ �������� ������
    /// </summary>
    public abstract class ValueVisualizer : MonoBehaviour
    {
        public abstract void Visualize(float value);
    }
}

