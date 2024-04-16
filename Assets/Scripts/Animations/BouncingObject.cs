using UnityEngine;

namespace Animations
{
    /// <summary>
    /// Объект с анимацией движения по Y по кривой
    /// </summary>
    public class BouncingObject : MonoBehaviour
    {
        [SerializeField] private float _force = 0.1f;
        [SerializeField] private float _loopTime = 3;
        [SerializeField] private float _timeOffset;
        [SerializeField] private AnimationCurve _bounceCurve;

        private float _currentTime;
        private Vector3 _initPosition;

        private void Start()
        {
            _currentTime = _timeOffset;
            _initPosition = transform.localPosition;
        }

        private void Update()
        {
            _currentTime = (_currentTime + Time.deltaTime) % _loopTime;

            float currentY = _bounceCurve.Evaluate(_currentTime / _loopTime) * _force;
            transform.localPosition = _initPosition + Vector3.up * currentY;
        }
    }
}

