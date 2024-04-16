using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Triggers
{
    public class TriggerArea : MonoBehaviour
    {
        [SerializeField] private string[] _targetTagsArray;
        private HashSet<string> _targetTags;

        [SerializeField] private UnityEvent onActivated;
        [SerializeField] private UnityEvent onDeactivated;

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

        protected virtual void Activate(Collider other) { }
        protected virtual void Deactivate(Collider other) { }
    }
}
