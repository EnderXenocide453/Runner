using UnityEngine;
using UnityEngine.UI;

namespace UI.Visualization
{
    public class TextVisualizer : ValueVisualizer
    {
        [SerializeField] private Text _valueField;

        public override void Visualize(float value)
        {
            _valueField.text = value.ToString();
        }
    }
}

