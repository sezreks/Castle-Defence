using System;
using System.ComponentModel;
using UnityEngine;

namespace Extentions
{
    public static class ConvertExtensions
    { 
        public static int ToInt(this string input)
        {
            if (int.TryParse(input, out int result))
                return result;

            throw new InvalidCastException("Invalid Cast To Int");
        }
        public static int ToInt(this float input)
        {
            if (int.TryParse(input.ToString("F0"), out int result))
                return result;

            throw new InvalidCastException("Invalid Cast To Int");
        }


        public static float ToFloat(this string input)
        {
            if (float.TryParse(input, out float result))
                return result;

            throw new InvalidCastException("Invalid Cast To Float");
        }
        public static float ToFloat(this int input)
        {
            if (float.TryParse(input.ToString(), out float result))
                return result;

            throw new InvalidCastException("Invalid Cast To Float");
        }

        public static Guid ToGuid(this string input)
        {
            if (Guid.TryParse(input, out Guid result))
                return result;
            throw new InvalidCastException("Invalid Cast To Guid");
        }
        public static Vector2 ToVector2(this Vector3 input)
        {
            return new Vector2(input.x, input.z);
        }
        public static DateTime ToDateTime(this string input)
        {
            if (DateTime.TryParse(input, out DateTime result))
                return result;


            throw new InvalidCastException("Invalid Cast To DateTime");
        }

        public static T ConvertTo<T>(this object objectToConvert)
        {
            return (T)Convert.ChangeType(objectToConvert, typeof(T));
        }
    }
}
