namespace UI.Visualization
{
    /// <summary>
    /// Визуализатор, управляющий другими визуализаторами
    /// </summary>
    public class ComplexVisualizer : ValueVisualizer, IMaxValueHandler
    {
        private ValueVisualizer[] _childVisualizers;
        private IMaxValueHandler[] _childMaxVisualizers;
        private float _max;

        private void Awake()
        {
            _childVisualizers = GetComponentsInChildren<ValueVisualizer>();
            _childMaxVisualizers = GetComponentsInChildren<IMaxValueHandler>();
        }

        public float MaxValue 
        {
            get => _max;
            set
            {
                foreach (var child in _childMaxVisualizers) {
                    if (ReferenceEquals(child, this))
                        continue;

                    child.MaxValue = value;
                }

                _max = value;
            }
        }

        public override void Visualize(float value)
        {
            foreach (var childVisualizer in _childVisualizers) {
                //во избежание зацикливания
                if (ReferenceEquals(childVisualizer, this))
                    continue;

                childVisualizer.Visualize(value);
            }
        }
    }
}

