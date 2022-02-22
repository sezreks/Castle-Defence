using UnityEngine;
using UnityEngine.Events;

namespace Extentions
{
    public static class GameObjectExtensions
    {

        public static void Trigger(this GameObject self, UnityEvent evt)
        {
            if (evt != null)
                evt.Invoke();
            else
                Debug.LogWarning("Tried to invoke a null UnityEvent");
        }

        public static void Trigger<T>(this GameObject self, UnityEvent<T> evt, T data)
        {
            if (evt != null)
                evt.Invoke(data);
            else
                Debug.LogWarning("Tried to invoke a null UnityEvent on " + self.name + " with type '" + typeof(T).ToString() + "' with the follwing payload: " + data.ToString());
        }


        public static T GetComponentRequired<T>(this GameObject self, bool Create = false) where T : Component
        {
            T component = self.GetComponent<T>();

            if (component == null && Create)
                self.AddComponent<T>();

            return component;
        }

    }
}
