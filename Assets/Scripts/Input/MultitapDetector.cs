using System;
using UnityEngine;

namespace InputManagement
{
    [Serializable]
    public class MultitapDetector
    {
        [SerializeField] private float _maxMultiTapDelay = 1;
        private float _startTime;

        public event Action onMultiTap;

        public void RegistrateTap(float time)
        {
            if (time - _startTime <= _maxMultiTapDelay) {
                onMultiTap?.Invoke();
            }

            _startTime = time;
        }
    }
}
