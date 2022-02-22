using UnityEngine;

namespace Extentions
{
    public static class Vector2Extentions
    {
        public static float Distance(this Vector2 input, Vector2 target) => Vector2.Distance(input, target);
        public static Vector2 WithX(this Vector2 v, float x) => new Vector2(x, v.y);
        public static Vector2 WithY(this Vector2 v, float y) => new Vector2(v.x, y);
        public static Vector2 WithZ(this Vector2 v, float z) => new Vector3(v.x, v.y, z);
    }
}
