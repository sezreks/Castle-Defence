﻿using System;
using System.Collections.Generic;

namespace Extentions
{
    public static class ListExtentions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        public static T RandomItem<T>(this IList<T> list)
        {
            if (list.Count == 0) throw new ArgumentOutOfRangeException(nameof(list), "Cannot select a random item from an empty list");
            return list[UnityEngine.Random.Range(0, list.Count)];
        }
        public static T RandomPickItem<T>(this IList<T> list)
        {
            if (list.Count == 0) throw new ArgumentOutOfRangeException(nameof(list), "Cannot select a random item from an empty list");
            int index = UnityEngine.Random.Range(0, list.Count);
            T item = list[index];
            list.RemoveAt(index);
            return item;
        }
    }
}
