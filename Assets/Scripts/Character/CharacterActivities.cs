using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(CharacterAnimation))]
    public class CharacterActivities : MonoBehaviour
    {
        [SerializeField] private float _maxDeviation = 0.75f;
        [SerializeField] private float _deviationSharpness = 0.5f;
        private float _currentDeviation;
        private CharacterAnimation _animation;

        public float currentDeviation => _currentDeviation;

        private void Awake()
        {
            _animation = GetComponent<CharacterAnimation>();
        }

        public void Jump()
        {
            _animation.Jump();
        }

        public void Roll()
        {
            _animation.Roll();
        }

        public void Deviate(float deviation)
        {
            _currentDeviation = Mathf.Lerp(_currentDeviation, deviation, _deviationSharpness);
            float displayDeviation = Mathf.Clamp(_currentDeviation, -_maxDeviation, _maxDeviation);
            transform.localPosition = new Vector3(displayDeviation, transform.localPosition.y, transform.localPosition.z);
        }
    }
}

