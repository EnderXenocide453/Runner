using System.Collections.Generic;
using UnityEngine;

namespace UI.Visualization
{
    /// <summary>
    /// Визуализация целого числа в виде количества изображений с задним фоном
    /// </summary>
    public class CountedImagesWithBGVisualizer : CountedImagesVisualizer, IMaxValueHandler
    {
        [SerializeField] private RectTransform _backContainer;
        [SerializeField] private Sprite _backGroundImage;
        private int _max;
        private List<Transform> _images = new List<Transform>();

        public float MaxValue 
        { 
            get => _max; 
            set
            {
                _max = value < 0 ? 0 : (int)value;
                UpdateImages(_max, _images, _backGroundImage, _backContainer);
            }
        }
    }
}

