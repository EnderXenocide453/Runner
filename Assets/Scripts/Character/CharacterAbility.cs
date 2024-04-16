using Character.Abilities;
using System;
using UnityEngine;

namespace Character
{
    /// <summary>
    /// Класс, контроллирующий использование способностей персонажем
    /// </summary>
    public class CharacterAbility : MonoBehaviour
    {
        [SerializeField] private BaseAbility _ability;
        [SerializeField] private int _maxChargesCount = 32;
        [SerializeField] private int _chargesCount = 0;

        public int MaxCharges => _maxChargesCount;
        public int Charges => _chargesCount;

        public event Action onAbilityExecuted;
        public event Action<int> onChargesChanged;

        public void Execute()
        {
            if (_chargesCount == 0 || _ability == null || !_ability.IsReady)
                return;

            SetCount(Charges - 1);
            _ability.Execute();
            onAbilityExecuted?.Invoke();
        }

        public void AddCharge()
        {
            SetCount(Charges + 1);
        }

        private void SetCount(int count)
        {
            if (count < 0)
                count = 0;
            else if (count > MaxCharges)
                count = MaxCharges;

            _chargesCount = count;
            onChargesChanged?.Invoke(count);
        }
    }
}