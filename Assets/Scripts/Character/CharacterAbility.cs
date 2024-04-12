using Character.Abilities;
using System;
using UnityEngine;

namespace Character
{
    public class CharacterAbility : MonoBehaviour
    {
        [SerializeField] private BaseAbility _ability;
        [SerializeField] private int _maxChargesCount = 32;
        [SerializeField] private int _chargesCount = 0;

        public event Action onAbilityExecuted;
        public event Action onRecharged;

        public void Execute()
        {
            if (_chargesCount == 0 || _ability == null || !_ability.IsReady)
                return;

            _chargesCount--;
            _ability.Execute();
            onAbilityExecuted?.Invoke();
        }

        public void AddCharge()
        {
            if (_chargesCount >= _maxChargesCount)
                return;

            _chargesCount++;
            onRecharged?.Invoke();
        }
    }
}