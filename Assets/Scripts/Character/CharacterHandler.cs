﻿using System;
using UnityEngine;

namespace Character
{
    public class CharacterHandler : MonoBehaviour
    {
        [SerializeField] private CharacterRun _characterRun;
        [SerializeField] private CharacterActivities _activities;
        [SerializeField] private CharacterHealth _characterHealth;
        [SerializeField] private CharacterAnimation _characterAnimation;
        [SerializeField] private CharacterAbility _characterAbility;
        [SerializeField] private CharacterInfoVisualizer _valuesVisualizer;

        public CharacterRun CharacterRun => _characterRun;
        public CharacterActivities Activity => _activities;
        public CharacterAbility CharacterAbility => _characterAbility;

        public event Action onDeath;

        private void Awake()
        {
            _characterRun.onIncorrectTurn += () => _characterHealth.GetDamage(1);
            _characterHealth.onDeath += OnDeath;

            ConnectVisualizers();
        }

        private void ConnectVisualizers()
        {
            _valuesVisualizer.SetMaxHP(_characterHealth.MaxHealth);
            _valuesVisualizer.SetHP(_characterHealth.CurrentHealth);
            _valuesVisualizer.SetMaxChargesCount(_characterAbility.MaxCharges);
            _valuesVisualizer.SetChargesCount(_characterAbility.Charges);

            _characterHealth.onHealthChanged += _valuesVisualizer.SetHP;
            _characterAbility.onChargesChanged += _valuesVisualizer.SetChargesCount;
            _characterRun.onDistanceChanged += _valuesVisualizer.SetScore;
        }

        private void OnDeath()
        {
            _characterRun.enabled = false;
            _characterHealth.enabled = false;
            _activities.enabled = false;

            _characterAnimation.Death();

            onDeath?.Invoke();
        }
    }
}

