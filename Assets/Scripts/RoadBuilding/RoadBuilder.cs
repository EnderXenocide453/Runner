using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RoadBuilding
{
    /// <summary>
    /// Логика построения дороги
    /// </summary>
    [Serializable]
    public class RoadBuilder
    {
        [SerializeField] private RoadFactoryWeight[] _factoryWeights;
        private float _totalProbability;

        private float TotalProbability 
        { 
            get
            {
                if (_totalProbability != 0)
                    return _totalProbability;

                _totalProbability = 0;
                foreach (RoadFactoryWeight weight in _factoryWeights)
                    _totalProbability += weight.Weight;
                return _totalProbability;
            } 
        }

        public RoadBlock BuildBlock(Vector3 position, Quaternion rotation, RoadBlock parent)
        {
            var factory = GetRandomFactory();
            var block = factory.GenerateBlock(position, rotation);

            parent.AddChild(block);

            return block;
        }

        public RoadBlock[] BuildChildBlocks(RoadBlock parent)
        {
            RoadBlock[] children = new RoadBlock[parent.Connectors.Length];

            for (int i = 0; i < children.Length; i++) {
                var connector = parent.Connectors[i];
                children[i] = BuildBlock(connector.transform.position, connector.transform.rotation, parent);
            }

            return children;
        }

        private RoadBlockFactory GetRandomFactory()
        {
            float randomPoint = Random.value * TotalProbability;
            
            foreach (RoadFactoryWeight weight in _factoryWeights) {
                if (weight.Weight >= randomPoint)
                    return weight.RoadFactory;
                else 
                    randomPoint -= weight.Weight;
            }

            return _factoryWeights[_factoryWeights.Length - 1].RoadFactory;
        }

        [Serializable]
        private struct RoadFactoryWeight
        {
            public float Weight;
            public RoadBlockFactory RoadFactory;
        }
    }
}

