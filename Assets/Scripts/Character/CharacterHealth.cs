using GameManagement;
using System;
using UnityEngine;
using Zenject;

namespace Character
{
    public class CharacterHealth : MonoBehaviour
    {
        [SerializeField] private int _maxHealth = 3;
        private int _currentHealth;
        private SoundManager _soundManager;

        public int MaxHealth => _maxHealth;
        public int CurrentHealth => _currentHealth;

        public event Action onDeath;
        public event Action<int> onHealthChanged;

        [Inject]
        public void Construct(SoundManager soundManager)
        {
            _soundManager = soundManager;
        }

        private void Awake()
        {
            SetHealth(MaxHealth);
        }

        public void GetHealing(int healAmount)
        {
            SetHealth(_currentHealth + healAmount);
        }

        public void GetDamage(int damage)
        {
            SetHealth(_currentHealth - damage);
            _soundManager.PlaySound(SoundType.shipDamage);

            BecomeInvincible();
        }

        private void BecomeInvincible()
        {
            //Неуязвимость
        }

        public void PermaDeath()
        {
            SetHealth(0);
        }

        private void SetHealth(int amount)
        {
            _currentHealth = Mathf.Clamp(amount, 0, _maxHealth);

            onHealthChanged?.Invoke(amount);

            if (_currentHealth == 0) {
                Death();
            }
        }

        private void Death()
        {
            _soundManager.PlaySound(SoundType.shipDestruction);
            onDeath?.Invoke();
        }
    }
}

