using System;
using System.Collections;
using UnityEngine;

namespace Character
{
    /// <summary>
    /// Логика перемещения и поворота персонажа
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class CharacterMove : MonoBehaviour
    {
        [SerializeField] private float _minSpeedMultiplier = 1, _maxSpeedMultiplier = 10;
        [SerializeField] private float _speed = 2;
        [SerializeField] private float _speedIncreaseDistance = 1000;
        [SerializeField] private AnimationCurve _speedCurve;

        [SerializeField] private float _rotateSpeed = 180;
        [SerializeField] private float _positionCorrectionSpeed = 7.5f;

        private float _currentDistance;
        private float _currentSpeed;
        private bool _allowRotation;
        private Vector3 _oldPosition;
        private Rigidbody _body;
        private Vector3 _direction = Vector3.forward;

        public float MaxSpeedMultiplier => _maxSpeedMultiplier;
        public float CurrentDistance => _currentDistance;

        public event Action<float> onDistanceChanged;
        public event Action<float> onSpeedChanged;

        private void Start()
        {
            _body = GetComponent<Rigidbody>();
            _oldPosition = transform.position;
        }

        private void FixedUpdate()
        {
            CalculateCurrentSpeed();
            _body.MovePosition(_direction * _currentSpeed * Time.fixedDeltaTime + _body.position);
            CalculateDistance();
            LookAtDirection();
        }

        public void SetDirection(Vector3 direction) => _direction = direction;

        public void MoveTo(Vector3 position)
        {
            StartCoroutine(MoveToPosition(position));
        }

        public void AllowRotation() => _allowRotation = true;

        private void CalculateCurrentSpeed()
        {
            float speedMultiplier = _maxSpeedMultiplier;
            if (_currentDistance < _speedIncreaseDistance)
                speedMultiplier = Mathf.Lerp(_minSpeedMultiplier, _maxSpeedMultiplier, _speedCurve.Evaluate(_currentDistance / _speedIncreaseDistance));

            _currentSpeed = _speed * speedMultiplier;
            onSpeedChanged?.Invoke(speedMultiplier);
        }

        private void CalculateDistance()
        {
            //Компенсация сдвига по y чтобы не учитывались прыжки
            _oldPosition.y = transform.position.y;

            _currentDistance += Vector3.Distance(_oldPosition, transform.position);
            _oldPosition = transform.position;

            onDistanceChanged?.Invoke(_currentDistance);
        }

        private void LookAtDirection()
        {
            if (!_allowRotation)
                return;

            var currQuaternion = Quaternion.LookRotation(_direction);
            if (currQuaternion == transform.rotation) {
                _allowRotation = false;
                return;
            }

            _body.MoveRotation(Quaternion.RotateTowards(transform.rotation, currQuaternion, _rotateSpeed * Time.fixedDeltaTime));
        }

        private IEnumerator MoveToPosition(Vector3 position)
        {
            Vector3 offset = position - transform.position;

            while (offset.magnitude > 0.01f) {
                Vector3 delta = Vector3.MoveTowards(Vector3.zero, offset, Time.deltaTime * _positionCorrectionSpeed);
                offset -= delta;
                transform.position += delta;

                yield return null;
            }
        }
    }
}

