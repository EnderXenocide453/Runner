using UnityEngine;

namespace Utils
{
    public static class Utils
    {
        public static Quaternion GetRotationFromDirection(MoveDirection direction)
        {
            switch (direction) {
                case MoveDirection.Left:
                    return Quaternion.AngleAxis(-90, Vector3.up);
                case MoveDirection.Right:
                    return Quaternion.AngleAxis(90, Vector3.up);
                default:
                    return Quaternion.identity;
            }
        }

        public static MoveDirection GetDirectionFromVector(Vector2 vector)
        {
            if (Mathf.Abs(vector.x) >= Mathf.Abs(vector.y)) {
                if (vector.x < 0)
                    return MoveDirection.Left;
                return MoveDirection.Right;
            } else if (vector.y < 0)
                return MoveDirection.Back;
            return MoveDirection.Forward;
        }
    }

    public enum MoveDirection
    {
        Forward,
        Right,
        Left,
        Back
    }
}
