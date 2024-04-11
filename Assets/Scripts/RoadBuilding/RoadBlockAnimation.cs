using Animations;
using UnityEngine;

namespace RoadBuilding
{

    public class RoadBlockAnimation : AppearableObjectAnimation
    {
        [SerializeField] private AppearableObjectAnimation[] _childAnimations;

        public override void PlayAppearAnimation()
        {
            base.PlayAppearAnimation();
            foreach (var child in _childAnimations) {
                child.PlayAppearAnimation();
            }
        }

        public override void PlayDisappearAnimation()
        {
            base.PlayDisappearAnimation();
            foreach (var child in _childAnimations) {
                child.PlayDisappearAnimation();
            }
        }
    }
}

