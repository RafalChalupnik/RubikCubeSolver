using System;

namespace RubikCubeSolver.Core.Extensions
{
    internal static class EnumExtensions
    {
        public static bool IsCorrect(this Color color)
            => Enum.IsDefined(typeof(Color), color);
    }
}