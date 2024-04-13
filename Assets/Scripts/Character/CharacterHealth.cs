using System;
using UnityEngine;

namespace Character
{
    public class CharacterHealth : MonoBehaviour
    {
        [SerializeField] private int _maxHealth = 3;
        private int _currentHealth;

        public int MaxHealth => _maxHealth;
        public int CurrentHealth => _currentHealth;

        public event Action onDeath;
        public event Action<int> onHealthChanged;

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

            //Обновляем визуализацию здоровья

            if (_currentHealth == 0) {
                Death();
            }
        }

        private void Death()
        {
            onDeath?.Invoke();
        }
    }
}

