using System.Collections.Generic;
using UnityEngine;

namespace UI.Visualization
{
    public class CountedImagesWithBGVisualizer : CountedImagesVisualizer, IMaxValueHandler
    {
        [SerializeField] private RectTransform _container;
        [SerializeField] private GameObject _backGroundPrefab;
        private int _max;
        private List<Transform> _images = new List<Transform>();

        public float MaxValue 
        { 
            get => _max; 
            set
            {
                _max = value < 0 ? 0 : (int)value;
                UpdateImages(_max, _images, _backGroundPrefab, _container);
            }
        }
    }
}

