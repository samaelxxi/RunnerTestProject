using System.Collections;
using System.Collections.Generic;
using System;

namespace UnavinarML.General
{
    public static class CollectionExtensions
    {
        static readonly System.Random _random = new(0);

        public static T RandomElement<T>(this IList<T> list)
        {
            if (list.Count == 0)
                return default;
            else if (list.Count == 1)
                return list[0];

            return list[_random.Next(0, list.Count)];
        }
    }
}