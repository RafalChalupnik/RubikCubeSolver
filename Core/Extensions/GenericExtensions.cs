using System.Collections.Generic;

namespace RubikCubeSolver.Core.Extensions
{
    internal static class GenericExtensions
    {
        public static List<T> AsList<T>(this T element)
            => new List<T> {element};
    }
}