using System.ComponentModel;

namespace Extentions
{
    public static class ArrayExtensions
    {/// <summary>
     /// For each component in an array, take an action
     /// </summary>
     /// <typeparam name="T"></typeparam>
     /// <param name="array"></param>
     /// <param name="callback">The action to take</param>
        public static void ForEachComponent<T>(this T[] array, System.Action<T> callback) where T : Component
        {
            for (var i = 0; i < array.Length; i++)
            {
                if (callback != null)
                    callback.Invoke(array[i]);
            }
        }
    }
}
