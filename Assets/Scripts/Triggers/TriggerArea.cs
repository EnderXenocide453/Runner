using System.Collections.Generic;
using UnityEngine;

namespace Triggers
{
    public abstract class TriggerArea : MonoBehaviour
    {
        [SerializeField] private string[] _targetTagsArray;
        private HashSet<string> _targetTags;

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
            if (_targetTags.Contains(other.tag)) {
                Activate(other);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (_targetTags.Contains(other.tag)) {
                Deactivate(other);
            }
        }

        protected abstract void Activate(Collider other);
        protected abstract void Deactivate(Collider other);
    }
}
