using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

namespace InputManagement
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] float _mouseSensitivity = 1f;

        private PlayerControl _playerControl;

        public delegate void TouchEventHandler(Vector2 position, float time);
        public event TouchEventHandler onTouchStarted;
        public event TouchEventHandler onTouchEnded;
        public event Action<Direction> onMoveInput;

        private void Awake()
        {
            _playerControl = new PlayerControl();

            if (SystemInfo.deviceType == DeviceType.Handheld) {
                _playerControl.TouchMap.Enable();

                _playerControl.TouchMap.TouchContact.started += StartTouch;
                _playerControl.TouchMap.TouchContact.canceled += EndTouch;
            } else if (SystemInfo.deviceType == DeviceType.Desktop) {
                _playerControl.PCmap.Enable();

                _playerControl.PCmap.Jump.started += (ctx) => { HandleMoveInput(Direction.Forward); };
                _playerControl.PCmap.Roll.started += (ctx) => { HandleMoveInput(Direction.Back); };
                _playerControl.PCmap.Left.started += (ctx) => { HandleMoveInput(Direction.Left); };
                _playerControl.PCmap.Right.started += (ctx) => { HandleMoveInput(Direction.Right); };
            }
        }

        public float GetDeviation()
        {
            if (SystemInfo.supportsGyroscope) {
                return Input.gyro.attitude.eulerAngles.z;
            }

            float deviation = _playerControl.PCmap.MousePosition.ReadValue<Vector2>().x;
            deviation = Mathf.InverseLerp(0, Screen.width, deviation) * 2 - 1;
            return deviation * _mouseSensitivity;
        }

        private void HandleMoveInput(Direction direction)
        {
            onMoveInput?.Invoke(direction);
        }

        private void EndTouch(InputAction.CallbackContext obj)
        {
            Vector2 pos = _playerControl.TouchMap.TouchPosition.ReadValue<Vector2>();

            onTouchEnded?.Invoke(pos, (float)obj.time);
        }

        private void StartTouch(InputAction.CallbackContext obj)
        {
            Vector2 pos = _playerControl.TouchMap.TouchPosition.ReadValue<Vector2>();

            onTouchStarted?.Invoke(pos, (float)obj.startTime);
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
}
