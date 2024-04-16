using System;
using UnityEngine;
using Zenject;

namespace Character
{
    /// <summary>
    /// Класс-контейнер логики персонажа
    /// </summary>
    public class CharacterHandler : MonoBehaviour
    {
        [SerializeField] private CharacterDirection _characterDirection;
        [SerializeField] private CharacterActivities _activities;
        [SerializeField] private CharacterHealth _characterHealth;
        [SerializeField] private CharacterAnimation _characterAnimation;
        [SerializeField] private CharacterAbility _characterAbility;
        private CharacterInfoVisualizer _valuesVisualizer;

        public CharacterDirection CharacterDirection => _characterDirection;
        public CharacterActivities Activity => _activities;
        public CharacterAbility CharacterAbility => _characterAbility;

        public event Action onDeath;

        [Inject]
        public void Constructor(CharacterInfoVisualizer visualizer)
        {
            _valuesVisualizer = visualizer;
            ConnectVisualizers();
        }

        private void Awake()
        {
            _characterDirection.onIncorrectTurn += () => _characterHealth.GetDamage(1);
            _characterHealth.onDeath += OnDeath;
        }

        private void ConnectVisualizers()
        {
            _valuesVisualizer.SetMaxHP(_characterHealth.MaxHealth);
            _valuesVisualizer.SetHP(_characterHealth.CurrentHealth);
            _valuesVisualizer.SetMaxChargesCount(_characterAbility.MaxCharges);
            _valuesVisualizer.SetChargesCount(_characterAbility.Charges);
            _valuesVisualizer.SetMaxSpeed(_characterDirection.CharacterMove.MaxSpeedMultiplier);

            _characterHealth.onHealthChanged += _valuesVisualizer.SetHP;
            _characterAbility.onChargesChanged += _valuesVisualizer.SetChargesCount;
            _characterDirection.CharacterMove.onDistanceChanged += _valuesVisualizer.SetScore;
            _characterDirection.CharacterMove.onSpeedChanged += _valuesVisualizer.SetSpeed;
        }

        private void OnDeath()
        {
            _characterDirection.enabled = false;
            _characterHealth.enabled = false;
            _activities.enabled = false;

            _characterAnimation.Death();

            onDeath?.Invoke();
        }
    }
}

