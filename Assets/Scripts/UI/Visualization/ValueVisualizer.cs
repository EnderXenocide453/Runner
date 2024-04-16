using UnityEngine;

namespace UI.Visualization
{
    public abstract class ValueVisualizer : MonoBehaviour
    {
        public abstract void Visualize(float value);
    }
}

