using System;
using System.Collections.Generic;
using UnityEngine;

namespace RoadBuilding
{
    [RequireComponent(typeof(RoadBlockAnimation))]
    public class RoadBlock : MonoBehaviour
    {
        [SerializeField] private Transform[] _connectors;

        private RoadBlock _parent;
        private HashSet<RoadBlock> _children = new HashSet<RoadBlock>();
        private RoadBlockAnimation _animation;

        public Transform[] Connectors => _connectors;
        public HashSet<RoadBlock> Children => _children;

        public event Action<RoadBlock> onActivated;

        private void Start()
        {
            _animation = GetComponent<RoadBlockAnimation>();
            Appear();
        }

        public void Appear()
        {
            //Появление
            _animation.PlayAppearAnimation();
        }

        public void Disappear()
        {
            //Исчезновение
            _animation.PlayDisappearAnimation();
        }

        public void OnDisappeared()
        {
            Destroy(gameObject);
        }

        [ContextMenu("Activate")]
        public void OnActivated()
        {
            _parent?.RemoveChild(this);
            onActivated?.Invoke(this);
        }

        public void AddChild(RoadBlock child)
        {
            _children.Add(child);
            child._parent = this;
        }

        public void RemoveChild(RoadBlock child)
        {
            _children.Remove(child);
            child._parent = null;
        }
    }
}

