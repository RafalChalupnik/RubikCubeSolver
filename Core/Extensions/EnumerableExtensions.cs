using System;
using System.Collections.Generic;
using System.Linq;

namespace RubikCubeSolver.Core.Extensions
{
    internal static class EnumerableExtensions
    {
        public static IReadOnlyCollection<T> EnsureEvaluated<T>(this IEnumerable<T> enumerable)
        {
            return enumerable is IReadOnlyCollection<T> evaluated
                ? evaluated
                : enumerable.ToList();
        }

        public static bool IsEmpty<T>(this IEnumerable<T> enumerable)
            => !enumerable.Any();

        public static void Match<T>(
            this IEnumerable<T> enumerable,
            Action onNone,
            Action<T> onOne,
            Action<IEnumerable<T>> onMany)
        {
            var evaluated = enumerable.EnsureEvaluated();
            
            switch (evaluated.Count)
            {
                case 0: onNone(); break;
                case 1: onOne(evaluated.Single()); break;
                default: onMany(evaluated); break;
            }
        }
    }
}