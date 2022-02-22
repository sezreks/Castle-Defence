using UnityEngine;

namespace Extentions
{
    public static class BoundsExtentions
    {
        public static Vector3 RandomPointInBounds(this Bounds bounds, bool grounded = true) => new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            grounded ? 0 : Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z));
    }
}
