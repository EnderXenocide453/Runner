using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Visualization
{
    /// <summary>
    /// Визуализация целого числа в виде количества изображений
    /// </summary>
    public class CountedImagesVisualizer : ValueVisualizer
    {
        [SerializeField] private RectTransform _container;
        [SerializeField] private Sprite _counterImage;
        [SerializeField] private Vector2 _imageSize = new Vector2(50, 50);

        private List<Transform> _instancedImages = new List<Transform>();

        public override void Visualize(float value)
        {
            int count = (int)value;
            UpdateImages(count, _instancedImages, _counterImage, _container);
        }

        protected void UpdateImages(int count, List<Transform> images, Sprite sprite, RectTransform parent)
        {
            count = count < 0 ? 0 : count;

            if (count == images.Count)
                return;
            if (count == 0) {
                Clear();
                return;
            }
            if (count < images.Count) {
                for (int i = images.Count - 1; i >= count; i--) {
                    var image = images[i];
                    images.RemoveAt(i);
                    Destroy(image.gameObject);
                }
                return;
            }
            for (int i = images.Count; i < count; i++) {
                images.Add(CreateNewImage(parent, sprite));
            }
        }

        private Transform CreateNewImage(Transform parent, Sprite image)
        {
            var obj = new GameObject("CounterImage", typeof(CanvasRenderer), typeof(Image));

            obj.GetComponent<Image>().sprite = image;
            obj.GetComponent<RectTransform>().sizeDelta = _imageSize;
            obj.transform.SetParent(parent);
            obj.transform.localScale = Vector3.one;

            return obj.transform;
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

