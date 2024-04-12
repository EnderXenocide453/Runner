using System;
using UnityEngine;
using Utils;

namespace InputManagement
{
    [Serializable]
    public class SwipeDetector
    {
        [SerializeField] float _swipeMaxTime = 2f;
        [SerializeField] float _swipeMinLenght = 0.5f;

        private float _startTime;
        private Vector2 _startPosition;

        public event Action<MoveDirection> onSwipeDetected;

        public void BeginDetection(Vector2 position, float time)
        {
            _startPosition = position;
            _startTime = time;
        }

        public void EndDetection(Vector2 position, float time)
        {
            float distance = Vector2.Distance(_startPosition, position) / Screen.width;
            Debug.Log(distance);
            if (time - _startTime > _swipeMaxTime || Vector2.Distance(_startPosition, position) < _swipeMinLenght)
                return;

            MoveDirection direction = Utils.Utils.GetDirectionFromVector(position - _startPosition);
            onSwipeDetected(direction);
        }
    }
}
