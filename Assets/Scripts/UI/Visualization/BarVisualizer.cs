using UnityEngine;
using UnityEngine.UI;

namespace UI.Visualization
{
    //Визуализатор в виде полосы прогресса
    public class BarVisualizer : ValueVisualizer, IMaxValueHandler
    {
        [SerializeField] private Image _bar;
        private float _max;
        private float _current;

        public float MaxValue 
        { 
            get => _max;
            set 
            {
                if (value < 0) {
                    _max = 0;
                    Visualize(_current);
                }

                _max = value;
                Visualize(_current);
            } 
        }

        public override void Visualize(float value)
        {
            _current = value;

            if (_bar == null)
                return;

            if (_max == 0) {
                _bar.fillAmount = 0;
            }

            _bar.fillAmount = value / _max;
        }
    }
}

