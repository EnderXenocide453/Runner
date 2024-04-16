using UnityEngine;
using UnityEngine.UI;

namespace UI.Visualization
{
    /// <summary>
    /// Текстовый визуализатор с максимальным значением
    /// </summary>
    public class TextPairVisualizer : TextVisualizer, IMaxValueHandler
    {
        [SerializeField] private Text _maxField;
        [SerializeField] private string _maxDescription;
        float _max;

        public float MaxValue
        {
            get => _max;
            set
            {
                _max = value;
                DrawValue(value, _maxDescription, _maxField);
            }
        }
    }
}

