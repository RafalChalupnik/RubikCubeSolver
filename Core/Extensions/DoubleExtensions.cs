using System;

namespace RubikCubeSolver.Core.Extensions
{
    internal static class DoubleExtensions
    {
        public static bool IsInteger(this double doubleValue)
            => Math.Abs(doubleValue % 1) <= (Double.Epsilon * 100);
    }
}