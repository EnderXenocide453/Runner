using System;
using System.Collections.Generic;
using UnityEngine;

namespace Triggers
{
    public abstract class TriggerArea : MonoBehaviour
    {
        [SerializeField] private string[] _targetTagsArray;
        private HashSet<string> _targetTags;

        public event Action onActivated;
        public event Action onDeactivated;

        public HashSet<string> TargetTags
        {
            get
            {
                if (_targetTags == null) {
                    _targetTags = new HashSet<string>(_targetTagsArray);
                }

                return _targetTags;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (TargetTags.Contains(other.tag)) {
                Activate(other);
                onActivated?.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (TargetTags.Contains(other.tag)) {
                Deactivate(other);
                onDeactivated?.Invoke();
            }
        }

        protected abstract void Activate(Collider other);
        protected abstract void Deactivate(Collider other);
    }
}
