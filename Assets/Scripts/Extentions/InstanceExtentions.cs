using System;

namespace Extentions
{
    public static class InstanceExtentions
    {
        public static T CreateInstance<T>(params object[] paramArray)
        {
            return (T)Activator.CreateInstance(typeof(T), args: paramArray);
        }
    }
}
