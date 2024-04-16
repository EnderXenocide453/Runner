using UnityEngine;

namespace UI.Visualization
{
    /// <summary>
    /// Визуализатор числовых данных
    /// </summary>
    public abstract class ValueVisualizer : MonoBehaviour
    {
        public abstract void Visualize(float value);
    }
}

