using UnityEngine;

namespace Extentions
{
    public static class RigidbodyExtentions
    {
        public static void Freeze(this Rigidbody rigidbody)
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
            rigidbody.isKinematic = true;
        }
        public static void Thaw(this Rigidbody rigidbody)
        {
            rigidbody.isKinematic = false;
        }
    }
}
