using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SwitchToggle : MonoBehaviour
    {
        [SerializeField] RectTransform handle;
        [SerializeField] private Image _backgroundImage;
        [SerializeField] Color enabledColor;
        [SerializeField] Color disabledColor;
        private Toggle _toggle;
        Vector2 _handlePos;

        private void Awake()
        {
            _toggle = GetComponent<Toggle>();

            _handlePos = handle.anchoredPosition;
            _toggle.onValueChanged.AddListener(OnSwitch);

            if (_toggle.isOn)
                OnSwitch(true);
            else
                OnSwitch(false);
        }

        private void OnSwitch(bool on)
        {
            if (on) {
                handle.anchoredPosition = -_handlePos;
                _backgroundImage.color = enabledColor;
            } else {
                handle.anchoredPosition = _handlePos;
                _backgroundImage.color = disabledColor;
            }
        }
    }
}