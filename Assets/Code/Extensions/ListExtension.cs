using System.Collections.Generic;
using UnityEngine;

namespace Code.Extensions
{
    public static class ListExtension
    {
        public static T GetRandom<T>(this List<T> list) =>
            list[Random.Range(0, list.Count)];
    }
}