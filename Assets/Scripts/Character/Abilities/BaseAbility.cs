using System.Collections;
using UnityEngine;

namespace Character.Abilities
{
    public abstract class BaseAbility : MonoBehaviour, IAbility
    {
        [SerializeField] protected float delay;

        protected bool _isReady = true;
        public bool IsReady => _isReady;

        public virtual void Execute()
        {
            if (_isReady) {
                UseAbility();
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
