using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Character
{
    [RequireComponent(typeof(Rigidbody))]
    public class CharacterRun : MonoBehaviour
    {
        [SerializeField] private float _minSpeed = 1, _maxSpeed = 10;
        [SerializeField] private float _speedIncreaseDistance = 1000;
        [SerializeField] private AnimationCurve _speedCurve;

        [SerializeField] private float _rotateSpeed = 180;

        private float _currentDistance;
        private bool _allowRotation;
        private Vector3 _oldPosition;
        private Rigidbody _body;
        private Vector3 _direction = Vector3.forward;

        private HashSet<Direction> _availableDirections;
        private Vector3 _turnOrigin;

        public float CurrentSpeed
        {
            get
            {
                if (_currentDistance > _speedIncreaseDistance)
                    return _maxSpeed;

                return Mathf.Lerp(_minSpeed, _maxSpeed, _speedCurve.Evaluate(_currentDistance / _speedIncreaseDistance));
            }
        }

        private void Start()
        {
            _body = GetComponent<Rigidbody>();
            _oldPosition = transform.position;
        }

        private void FixedUpdate()
        {
            _body.MovePosition(_direction * CurrentSpeed * Time.fixedDeltaTime + _body.position);
            CalculateDistance();
            LookAtDirection();
        }

        public void TurnTo(Direction direction)
        {
            if (_availableDirections == null || !_availableDirections.Contains(direction)) {
                //Обрабатываем врезание в стену
                return;
            }

            Quaternion angle = Directions.GetRotationFromDirection(direction);

            _direction = angle * _direction;
            transform.position = _turnOrigin;
            _allowRotation = true;

            DisableTurn();
        }

        public void EnableTurn(Direction[] directions, Vector3 origin)
        {
            _availableDirections = new HashSet<Direction>(directions);
            _turnOrigin = origin;

            Debug.Log($"Enabled {directions}");
        }

        public void DisableTurn()
        {
            _availableDirections.Clear();

            Debug.Log($"Disabled");
        }

        private void CalculateDistance()
        {
            //Компенсация сдвига по y чтобы не учитывались прыжки
            _oldPosition.y = transform.position.y;

            _currentDistance += Vector3.Distance(_oldPosition, transform.position);
            _oldPosition = transform.position;
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

