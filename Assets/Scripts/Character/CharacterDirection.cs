using GameManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using Zenject;

namespace Character
{
    /// <summary>
    /// Логика управления направлением движения персонажа
    /// </summary>
    [RequireComponent(typeof(CharacterMove))]
    public class CharacterDirection : MonoBehaviour
    {
        #region Поля
        private CharacterMove _characterMove;

        private Vector3 _direction = Vector3.forward;

        private HashSet<MoveDirection> _availableDirections;
        private Vector3 _turnOrigin;
        private SoundManager _soundManager;
        #endregion

        #region Свойства
        public CharacterMove CharacterMove
        {
            get
            {
                if (_characterMove == null)
                    _characterMove = GetComponent<CharacterMove>();
                return _characterMove;
            }
        }
        #endregion

        #region События
        public event Action onIncorrectTurn;
        #endregion

        [Inject]
        public void Construct(SoundManager soundManager)
        {
            _soundManager = soundManager;
        }

        public void TurnTo(MoveDirection direction)
        {
            if (_availableDirections == null || !_availableDirections.Contains(direction)) {
                //Обрабатываем врезание в стену
                onIncorrectTurn?.Invoke();
                return;
            }

            Quaternion angle = Utils.Utils.GetRotationFromDirection(direction);

            _direction = angle * _direction;
            CharacterMove.SetDirection(_direction);
            CharacterMove.AllowRotation();

            DisableTurn();
            _soundManager.PlaySound(SoundType.shipTurn);
            CharacterMove.MoveTo(_turnOrigin);
        }

        public void EnableTurn(MoveDirection[] directions, Vector3 origin)
        {
            _availableDirections = new HashSet<MoveDirection>(directions);
            _turnOrigin = origin;
        }

        public void DisableTurn()
        {
            _availableDirections.Clear();
        }

        private void OnDisable()
        {
            CharacterMove.enabled = false;
        }

        private void OnEnable()
        {
            CharacterMove.enabled = true;
        }
    }
}

