using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Character
{
    [RequireComponent(typeof(Rigidbody))]
    public class CharacterRun : MonoBehaviour
    {
        [SerializeField] private float _minSpeedMultiplier = 1, _maxSpeedMultiplier = 10;
        [SerializeField] private float _speed = 2;
        [SerializeField] private float _speedIncreaseDistance = 1000;
        [SerializeField] private AnimationCurve _speedCurve;

        [SerializeField] private float _rotateSpeed = 180;

        private float _currentDistance;
        private float _currentSpeed;
        private bool _allowRotation;
        private Vector3 _oldPosition;
        private Rigidbody _body;
        private Vector3 _direction = Vector3.forward;

        private HashSet<MoveDirection> _availableDirections;
        private Vector3 _turnOrigin;

        public float MaxSpeedMultiplier => _maxSpeedMultiplier;

        public event Action onIncorrectTurn;
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

        public void TurnTo(MoveDirection direction)
        {
            if (_availableDirections == null || !_availableDirections.Contains(direction)) {
                //Обрабатываем врезание в стену
                onIncorrectTurn?.Invoke();
                return;
            }

            Quaternion angle = Utils.Utils.GetRotationFromDirection(direction);

            _direction = angle * _direction;
            transform.position = _turnOrigin;
            _allowRotation = true;

            DisableTurn();
        }

        public void EnableTurn(MoveDirection[] directions, Vector3 origin)
        {
            _availableDirections = new HashSet<MoveDirection>(directions);
            _turnOrigin = origin;

            Debug.Log($"Enabled {directions}");
        }

        public void DisableTurn()
        {
            _availableDirections.Clear();

            Debug.Log($"Disabled");
        }

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
    }
}

