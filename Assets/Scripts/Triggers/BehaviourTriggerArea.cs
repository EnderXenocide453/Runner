using Character.Abilities;
using UnityEngine;

namespace Triggers
{
    public class BehaviourTriggerArea : TriggerArea
    {
        [SerializeField] private CollisionBehaviour[] _activateBehaviours;
        [SerializeField] private CollisionBehaviour[] _deactivateBehaviours;

        protected override void Activate(Collider other)
        {
            foreach (var behaviour in _activateBehaviours) {
                behaviour?.OnHit(other);
            }
        }

        protected override void Deactivate(Collider other)
        {
            foreach (var behaviour in _deactivateBehaviours) {
                behaviour?.OnHit(other);
            }
        }
    }
}
