using System;
using UI.Visualization;
using UnityEngine;

namespace Character
{
    [Serializable]
    public class CharacterInfoVisualizer
    {
        [SerializeField] private ValueVisualizer _healthVisualizer;
        [SerializeField] private ValueVisualizer _scoreVizualizer;
        [SerializeField] private ValueVisualizer _speedVisualizer;
        [SerializeField] private ValueVisualizer _chargeCountVisualizer;

        public void SetMaxHP(int hp) => SetMaxValue(hp, _healthVisualizer);

        public void SetHP(int hp) => SetValue(hp, _healthVisualizer);

        public void SetScore(float score) => SetValue(score, _scoreVizualizer);

        public void SetMaxChargesCount(int count) => SetMaxValue(count, _chargeCountVisualizer);

        public void SetChargesCount(int count) => SetValue(count, _chargeCountVisualizer);

        public void SetMaxSpeed(float value) => SetMaxValue(value, _speedVisualizer);

        public void SetSpeed(float value) => SetValue(value, _speedVisualizer);

        private void SetValue(float value, ValueVisualizer visualizer) => visualizer?.Visualize(value);

        private void SetMaxValue(float value, ValueVisualizer visualizer)
        {
            if (visualizer == null) return;

            if (visualizer is IMaxValueHandler max)
                max.MaxValue = value;
        }
    }
}

