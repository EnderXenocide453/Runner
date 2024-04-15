using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

namespace InputManagement
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] float _mouseSensitivity = 1f;
        [SerializeField] float _accelerometrSensitivity = 1.5f;
        [SerializeField] SwipeDetector _swipeDetector;
        [SerializeField] MultitapDetector _multitapDetector;

        private PlayerControl _playerControl;

        public event Action<MoveDirection> onMoveInput;
        public event Action onUseAbility;
        public event Action onPause;

        private void Awake()
        {
            _playerControl = new PlayerControl();
            InitGeneral();

            if (SystemInfo.deviceType == DeviceType.Handheld) {
                InitHandheld();
            } else if (SystemInfo.deviceType == DeviceType.Desktop) {
                InitDesktop();
            }
        }

        private void InitGeneral()
        {
            _playerControl.GeneralMap.Enable();
            _playerControl.GeneralMap.PauseAction.started += OnPause;
        }

        private void InitDesktop()
        {
            _playerControl.PCmap.Enable();

            _playerControl.PCmap.Jump.started += (ctx) => { HandleMoveInput(MoveDirection.Forward); };
            _playerControl.PCmap.Roll.started += (ctx) => { HandleMoveInput(MoveDirection.Back); };
            _playerControl.PCmap.Left.started += (ctx) => { HandleMoveInput(MoveDirection.Left); };
            _playerControl.PCmap.Right.started += (ctx) => { HandleMoveInput(MoveDirection.Right); };

            _playerControl.PCmap.UseAbility.started += (ctx) => UseAbility();
        }

        private void InitHandheld()
        {
            _playerControl.TouchMap.Enable();

            _playerControl.TouchMap.TouchContact.started += StartTouch;
            _playerControl.TouchMap.TouchContact.canceled += EndTouch;

            _swipeDetector.onSwipeDetected += HandleMoveInput;
            _multitapDetector.onMultiTap += UseAbility;
        }

        private void OnPause(InputAction.CallbackContext obj)
        {
            onPause?.Invoke();
        }

        private void UseAbility()
        {
            onUseAbility.Invoke();
        }

        public float GetDeviation()
        {
            if (SystemInfo.deviceType == DeviceType.Handheld) {
                return Input.acceleration.x * _accelerometrSensitivity;
            }

            float deviation = _playerControl.PCmap.MousePosition.ReadValue<Vector2>().x;
            deviation = Mathf.InverseLerp(0, Screen.width, deviation) * 2 - 1;
            return deviation * _mouseSensitivity;
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

            float time = (float)obj.startTime;
            _swipeDetector.BeginDetection(pos, time);
            _multitapDetector.RegistrateTap(time);
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

        private void OnDestroy()
        {
            onMoveInput = null;
            onUseAbility = null;
            onPause = null;
        }
    }
}
