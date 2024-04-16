using UnityEngine;
using UnityEngine.UI;

namespace UI.Visualization
{
    /// <summary>
    /// Текстовый визуализатор
    /// </summary>
    public class TextVisualizer : ValueVisualizer
    {
        [SerializeField] private Text _valueField;
        [SerializeField] private string _valueDescription;
        [SerializeField, Range(0, 7)] private byte _roundAmount = 0;

        public override void Visualize(float value)
        {
            DrawValue(value, _valueDescription, _valueField);
        }

        protected void DrawValue(float value, string description, Text textField)
        {
            textField.text = description + value.ToString($"F{_roundAmount}");
        }
    }
}

