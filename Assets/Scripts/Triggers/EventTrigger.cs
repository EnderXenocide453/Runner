using UnityEngine;
using UnityEngine.Events;

namespace Triggers
{
    public class EventTrigger : TriggerArea
    {
        [SerializeField] private UnityEvent onActivate;
        [SerializeField] private UnityEvent onDeactivate;

        protected override void Activate(Collider other)
        {
            onActivate?.Invoke();
        }

        protected override void Deactivate(Collider other)
        {
            onDeactivate?.Invoke();
        }
    }
}
