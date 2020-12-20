using System;
using System.Collections.Generic;
using System.Linq;
using RubikCubeSolver.Core.Extensions;

namespace RubikCubeSolver.Core
{
    public record Side
    {
        private const int c_size = 3;

        public IReadOnlyList<Color> Colors { get; init; }

        public Side(Color color)
        {
            AssertColorsAreValid(color);

            Colors = Enumerable.Range(0, c_size * c_size)
                .Select(_ => color)
                .ToList();
        }

        public Side(params Color[] colors)
        {
            var expectedColorsCount = c_size * c_size;

            if (colors.Length != expectedColorsCount)
            {
                throw new ArgumentException($"Expected exactly '{expectedColorsCount}' colors, found: '{colors.Length}'.");
            }
            
            AssertColorsAreValid(colors);
            Colors = colors.ToList();
        }
    
        public bool IsOneColor => Colors
            .Distinct()
            .Count() == 1;

        public IReadOnlyList<Color> BottomRow => Colors
            .Skip(c_size * 2)
            .Take(c_size)
            .ToList();

        public IReadOnlyList<Color> LeftColumn => Colors
            .Where((_, index) => index % c_size == 0)
            .ToList();

        public IReadOnlyList<Color> RightColumn => Colors
            .Where((_, index) => index % c_size == 2)
            .ToList();

        public IReadOnlyList<Color> TopRow => Colors
            .Take(c_size)
            .ToList();

        public Side SetBottomRow(IReadOnlyList<Color> colors)
        {
            if (colors.Count != c_size)
            {
                throw new ArgumentException($"Expected exactly '{c_size}' colors, found '{colors.Count}'.");
            }

            var newColors = colors.ToList();

            for (var i = c_size * 2; i < c_size * 3; i++)
            {
                newColors[i] = colors[i];
            }

            return this with { Colors = newColors };
        }

        public Side SetLeftColumn(IReadOnlyList<Color> colors)
        {
            if (colors.Count != c_size)
            {
                throw new ArgumentException($"Expected exactly '{c_size}' colors, found '{colors.Count}'.");
            }
            
            var newColors = colors.ToList();

            for (var i = 0; i < 3; i++)
            {
                newColors[i * c_size] = colors[i];
            }

            return this with { Colors = newColors };
        }

        public Side SetRightColumn(IReadOnlyList<Color> colors)
        {
            if (colors.Count != c_size)
            {
                throw new ArgumentException($"Expected exactly '{c_size}' colors, found '{colors.Count}'.");
            }

            var newColors = colors.ToList();

            for (var i = 0; i < 3; i++)
            {
                newColors[i * c_size + 2] = colors[i];
            }

            return this with { Colors = newColors };
        }

        public Side SetTopRow(IReadOnlyList<Color> colors)
        {
            if (colors.Count != c_size)
            {
                throw new ArgumentException($"Expected exactly '{c_size}' colors, found '{colors.Count}'.");
            }

            var newColors = colors.ToList();

            for (var i = 0; i < c_size; i++)
            {
                newColors[i] = colors[i];
            }

            return this with { Colors = newColors };
        }

        public Side RotateClockwise()
        {
            var newColors = new Color[c_size * c_size];

            newColors[0] = Colors[6];
            newColors[1] = Colors[3];
            newColors[2] = Colors[0];
            newColors[3] = Colors[7];
            newColors[4] = Colors[4];
            newColors[5] = Colors[1];
            newColors[6] = Colors[8];
            newColors[7] = Colors[5];
            newColors[8] = Colors[2];

            return this with { Colors = newColors };
        }

        public Side RotateCounterClockwise()
        {
            var newColors = new Color[c_size * c_size];

            newColors[0] = Colors[2];
            newColors[1] = Colors[5];
            newColors[2] = Colors[8];
            newColors[3] = Colors[1];
            newColors[4] = Colors[4];
            newColors[5] = Colors[7];
            newColors[6] = Colors[0];
            newColors[7] = Colors[3];
            newColors[8] = Colors[6];

            return this with { Colors = newColors };
        }

        public Side RotateDouble()
        {
            var newColors = Colors.Reverse().ToArray();
            return this with { Colors = newColors };
        }

        private static void AssertColorsAreValid(params Color[] colors)
        {
            colors
                .Select(color => !color.IsCorrect())
                .Match(
                    onNone: () => { },
                    onOne: color => throw new ArgumentException($"Invalid color: '{color}'."),
                    onMany: colors => throw new ArgumentException($"Invalid colors: '{string.Join(",", colors)}'.")
                );
        }
    }
}
