using UnityEngine;
using UnityEngine.UI;

namespace UI.Visualization
{
    public class TextPairVisualizer : TextVisualizer, IMaxValueHandler
    {
        [SerializeField] private Text _maxField;
        float _max;

        public float MaxValue
        {
            get => _max;
            set
            {
                _max = value;
                _maxField.text = value.ToString();
            }
        }
    }
}

