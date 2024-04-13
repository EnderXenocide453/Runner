using System.Collections.Generic;
using UnityEngine;

namespace UI.Visualization
{
    public class CountedImagesVisualizer : ValueVisualizer
    {
        [SerializeField] private RectTransform _container;
        [SerializeField] private GameObject _counterImage;

        private List<Transform> _instancedImages = new List<Transform>();

        public override void Visualize(float value)
        {
            int count = (int)value;
            UpdateImages(count, _instancedImages, _counterImage, _container);
        }

        protected void UpdateImages(int count, List<Transform> images, GameObject prefab, RectTransform parent)
        {
            count = count < 0 ? 0 : count;

            if (count == images.Count)
                return;
            if (count == 0) {
                Clear();
                return;
            }
            if (count < images.Count) {
                for (int i = count - 1; i < images.Count; i++) {
                    var image = images[i];
                    Destroy(image.gameObject);
                }
                return;
            }
            for (int i = 0; i < count - images.Count; ++i) {
                Instantiate(prefab, parent);
            }
        }

        private void Clear()
        {
            foreach (Transform t in _instancedImages) {
                Destroy(t.gameObject);
            }

            _instancedImages.Clear();
        }
    }
}

