using UnityEngine;

namespace Extentions
{
    public static class Vector3Extentions
    {
        public static float Distance(this Vector3 input, Vector3 target) => Vector3.Distance(input, target);
        public static float GroundedDistance(this Vector3 input, Vector3 target) => Vector3.Distance(new Vector3(input.x, 0, input.z), new Vector3(target.x, 0, target.z));
        public static Vector3 WithX(this Vector3 v, float x) => new Vector3(x, v.y, v.z);
        public static Vector3 WithY(this Vector3 v, float y) => new Vector3(v.x, y, v.z);
        public static Vector3 WithZ(this Vector3 v, float z) => new Vector3(v.x, v.y, z);
        public static Vector3 WithAdditional(this Vector3 v, Vector3 z) => v + z;

    }
}
