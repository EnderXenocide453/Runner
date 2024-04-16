using GameManagement;
using UnityEngine;
using Zenject;

namespace Character
{
    [RequireComponent(typeof(CharacterAnimation))]
    public class CharacterActivities : MonoBehaviour
    {
        [SerializeField] private float _maxDeviation = 0.75f;
        [SerializeField] private float _deviationSharpness = 0.5f;
        private float _currentDeviation;
        private CharacterAnimation _animation;
        private SoundManager _soundManager;

        public float currentDeviation => _currentDeviation;

        [Inject]
        public void Construct(SoundManager soundManager)
        {
            _soundManager = soundManager;
        }

        private void Awake()
        {
            _animation = GetComponent<CharacterAnimation>();
        }

        public void Jump()
        {
            _soundManager.PlaySound(SoundType.shipTurn);
            _animation.Jump();
        }

        public void Roll()
        {
            _soundManager.PlaySound(SoundType.shipTurn);
            _animation.Roll();
        }

        public void Deviate(float deviation)
        {
            _currentDeviation = Mathf.Lerp(_currentDeviation, deviation, _deviationSharpness * Time.timeScale);
            float displayDeviation = Mathf.Clamp(_currentDeviation, -_maxDeviation, _maxDeviation);
            
            transform.localPosition = new Vector3(displayDeviation, transform.localPosition.y, transform.localPosition.z);
        }
    }
}

