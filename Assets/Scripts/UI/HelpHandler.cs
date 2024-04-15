using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HelpHandler : MonoBehaviour
    {
        [SerializeField] private Text _helpField;
        [SerializeField, TextArea] private string _generalDescription;
        [SerializeField, TextArea] private string _handheldDescription;
        [SerializeField, TextArea] private string _desktopDescription;

        private void Awake()
        {
            string helpText = _generalDescription;
            if (SystemInfo.deviceType == DeviceType.Handheld)
                helpText += _handheldDescription;
            else
                helpText += _desktopDescription;

            _helpField.text = helpText;
        }
    }
}