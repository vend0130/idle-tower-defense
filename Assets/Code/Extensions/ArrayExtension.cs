using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code.Extensions
{
    public static class ArrayExtension
    {
        public static T GetRandom<T>(this IEnumerable<T> array)
        {
            var newArray = array.ToArray();
            return newArray[Random.Range(0, newArray.Length)];
        }
    }
}