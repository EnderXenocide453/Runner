using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RoadBuilding
{
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

        public RoadBlock Build(Vector3 position, Quaternion roattion)
        {
            var factory = GetRandomFactory();
            return factory.GenerateBlock(position, roattion);
        }

        private RoadBlockFactory GetRandomFactory()
        {
            float randomPoint = Random.value * TotalProbability;
            
            foreach (RoadFactoryWeight weight in _factoryWeights) {
                if (weight.Weight < randomPoint)
                    return weight.PatternFactory;
                else 
                    randomPoint -= weight.Weight;
            }

            return _factoryWeights[_factoryWeights.Length - 1].PatternFactory;
        }

        [Serializable]
        private struct RoadFactoryWeight
        {
            public float Weight;
            public RoadBlockFactory PatternFactory;
        }
    }
}

