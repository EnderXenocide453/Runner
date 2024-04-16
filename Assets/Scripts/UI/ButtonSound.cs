using GameManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace UI
{
    public class ButtonSound : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private SoundManager _soundManager;

        [Inject]
        public void Construct(SoundManager soundManager)
        {
            _soundManager = soundManager;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _soundManager.PlaySound(SoundType.buttonDown);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _soundManager.PlaySound(SoundType.buttonUp);
        }
    }
}