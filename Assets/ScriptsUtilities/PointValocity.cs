using UnityEngine;

namespace ScriptsUtilities
{
    public struct PointVelocity
    {
        public Vector3 Point { get; private set; }
        public Vector3 Velocity { get; private set; }

        public PointVelocity(Vector3 point, Vector3 velocity)
        {
            Point = point;
            Velocity = velocity;
        }
    }
}
