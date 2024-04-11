using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

namespace InputManagement
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] float _mouseSensitivity = 1f;
        [SerializeField] SwipeDetector _swipeDetector;

        private PlayerControl _playerControl;

        public event Action<MoveDirection> onMoveInput;

        private void Awake()
        {
            _playerControl = new PlayerControl();

            Debug.Log($"Device: {SystemInfo.deviceType}");

            //if (SystemInfo.deviceType == DeviceType.Handheld) {
                _playerControl.TouchMap.Enable();

                _playerControl.TouchMap.TouchContact.started += StartTouch;
                _playerControl.TouchMap.TouchContact.canceled += EndTouch;

                _swipeDetector.onSwipeDetected += HandleMoveInput;
            //} else if (SystemInfo.deviceType == DeviceType.Desktop) {
                _playerControl.PCmap.Enable();

                _playerControl.PCmap.Jump.started += (ctx) => { HandleMoveInput(MoveDirection.Forward); };
                _playerControl.PCmap.Roll.started += (ctx) => { HandleMoveInput(MoveDirection.Back); };
                _playerControl.PCmap.Left.started += (ctx) => { HandleMoveInput(MoveDirection.Left); };
                _playerControl.PCmap.Right.started += (ctx) => { HandleMoveInput(MoveDirection.Right); };
            //}
        }

        public float GetDeviation()
        {
            //if (SystemInfo.supportsGyroscope) {
                Debug.Log($"Gyro: {Input.gyro.attitude.eulerAngles}");
                return Input.gyro.attitude.eulerAngles.z;
            //}

            //float deviation = _playerControl.PCmap.MousePosition.ReadValue<Vector2>().x;
            //deviation = Mathf.InverseLerp(0, Screen.width, deviation) * 2 - 1;
            //return deviation * _mouseSensitivity;
        }

        private void HandleMoveInput(MoveDirection direction)
        {
            onMoveInput?.Invoke(direction);
        }

        private void EndTouch(InputAction.CallbackContext obj)
        {
            Vector2 pos = _playerControl.TouchMap.TouchPosition.ReadValue<Vector2>();

            _swipeDetector.EndDetection(pos, (float)obj.time);
        }

        private void StartTouch(InputAction.CallbackContext obj)
        {
            Vector2 pos = _playerControl.TouchMap.TouchPosition.ReadValue<Vector2>();

            _swipeDetector.BeginDetection(pos, (float)obj.startTime);
        }

        private void OnEnable()
        {
            if (SystemInfo.deviceType == DeviceType.Handheld) {
                _playerControl.TouchMap.Enable();
            } else if (SystemInfo.deviceType == DeviceType.Desktop) {
                _playerControl.PCmap.Enable();
            }
        }

        private void OnDisable()
        {
            _playerControl.TouchMap.Disable();
            _playerControl.PCmap.Disable();
        }
    }

    [Serializable]
    public class SwipeDetector
    {
        [SerializeField] float _swipeMaxTime = 2f;
        [SerializeField] float _swipeMinLenght = 0.5f;

        private float _startTime;
        private Vector2 _startPosition;

        public event Action<MoveDirection> onSwipeDetected;

        public void BeginDetection(Vector2 position, float time)
        {
            _startPosition = position;
            _startTime = time;
        }

        public void EndDetection(Vector2 position, float time)
        {
            float distance = Vector2.Distance(_startPosition, position) / Screen.width;
            Debug.Log(distance);
            if (time - _startTime > _swipeMaxTime || Vector2.Distance(_startPosition, position) < _swipeMinLenght)
                return;

            MoveDirection direction = Utils.Utils.GetDirectionFromVector(position - _startPosition);
            onSwipeDetected(direction);
        }
    }
}
