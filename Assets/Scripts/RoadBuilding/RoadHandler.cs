﻿using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace RoadBuilding
{
    public class RoadHandler : MonoBehaviour
    {
        const int GenerationDepth = 2;

        [SerializeField] private RoadBuilder _roadBuilder;
        [SerializeField] private RoadBlock _initBlock;
        [SerializeField] private float delay = 1;

        private HashSet<RoadBlock> _lastBlocks = new HashSet<RoadBlock>();
        private RoadBlock _activeBlock;

        private void Start()
        {
            InitRoad();
        }

        private void InitRoad()
        {
            _activeBlock = _initBlock;
            AddBlock(_activeBlock);

            for (int i = 0; i < GenerationDepth; i++) {
                ExtendRoad();
            }
        }

        [ContextMenu("Test")]
        public void StartAutoTest()
        {
            StartCoroutine(AutoActivate());
        }

        private IEnumerator AutoActivate()
        {
            while(true) {
                yield return new WaitForSeconds(delay);
                var blocks = new RoadBlock[_activeBlock.Children.Count];
                _activeBlock.Children.CopyTo(blocks);
                blocks[0].OnActivated();
            }
        }

        private void OnBlockActivated(RoadBlock block)
        {
            DeactivateBlock(_activeBlock);

            _activeBlock = block;

            ExtendRoad();
        }

        private void ExtendRoad()
        {
            var tmpBlocks = new HashSet<RoadBlock>(_lastBlocks);
            _lastBlocks.Clear();

            foreach (var block in tmpBlocks) {
                var children = _roadBuilder.BuildChildBlocks(block);
                AddBlocks(children);
            }
        }

        private void DeactivateBlock(RoadBlock block)
        {
            _lastBlocks.Remove(block);

            var children = block.Children;

            foreach (var child in children)
                DeactivateBlock(child);

            block.Disappear();
        }

        private void AddBlocks(params RoadBlock[] blocks)
        {
            foreach (var block in blocks) {
                AddBlock(block);
            }
        }

        private void AddBlock(RoadBlock block)
        {
            _lastBlocks.Add(block);

            block.onActivated += OnBlockActivated;
        }
    }
}

