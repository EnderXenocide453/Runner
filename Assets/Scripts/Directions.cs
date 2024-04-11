using UnityEngine;

namespace Utils
{
    public static class Directions
    {
        public static Quaternion GetRotationFromDirection(Direction direction)
        {
            switch (direction) {
                case Direction.Left:
                    return Quaternion.AngleAxis(-90, Vector3.up);
                case Direction.Right:
                    return Quaternion.AngleAxis(90, Vector3.up);
                default:
                    return Quaternion.identity;
            }
        }
    }

    public enum Direction
    {
        Forward,
        Right,
        Left
    }
}
