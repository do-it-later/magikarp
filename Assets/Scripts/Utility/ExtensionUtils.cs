using UnityEngine;
using System.Collections.Generic;

public static class ExtensionUtils
{
    public static void ShuffleList<T>(this List<T> list)
    {
        for (int i = 0; i < list.Count; ++i)
        {
            int r = Random.Range(0, list.Count);
            T t = list[r];
            list[r] = list[i];
            list[i] = t;
        }
    }
}
