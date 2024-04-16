using GameManagement;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Character.Abilities
{
    public abstract class BaseAbility : MonoBehaviour, IAbility
    {
        [SerializeField] protected float delay;
        private SoundManager _soundManager;

        protected bool _isReady = true;
        public bool IsReady => _isReady;
        protected abstract SoundType AbilitySound { get; }

        [Inject]
        public void Construct(SoundManager soundManager)
        {
            _soundManager = soundManager;
        }

        public virtual void Execute()
        {
            if (_isReady) {
                UseAbility();
                _soundManager.PlaySound(AbilitySound);
                StartCoroutine(Cooldown());
            }
        }

        protected virtual void OnReady()
        {
            _isReady = true;
        }

        protected abstract void UseAbility();

        protected IEnumerator Cooldown()
        {
            _isReady = false;
            yield return new WaitForSeconds(delay);

            OnReady();
        }
    }
}
