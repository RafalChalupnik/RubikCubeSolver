using System;
using System.Collections.Generic;
using System.Linq;
using RubikCubeSolver.Core.Extensions;

namespace RubikCubeSolver.Core
{
    public class Side
    {
        private const int c_size = 3;

        private Color[] m_colors;

        public Side(Color color)
        {
            AssertColorsAreValid(color);

            m_colors = new Color[c_size * c_size];

            for (var i = 0; i < m_colors.Length; i++)
            {
                m_colors[i] = color;
            }
        }

        public Side(params Color[] colors)
        {
            var expectedColorsCount = c_size * c_size;

            if (colors.Length != expectedColorsCount)
            {
                throw new ArgumentException($"Expected exactly '{expectedColorsCount}' colors, found: '{colors.Length}'.");
            }
            
            AssertColorsAreValid(colors);

            m_colors = new Color[colors.Length];

            for (var i = 0; i < m_colors.Length; i++)
            {
                m_colors[i] = colors[i];
            }
        }
    
        public IReadOnlyList<Color> BottomRow => m_colors
            .Skip(c_size * 2)
            .Take(c_size)
            .ToList();

        public IReadOnlyList<Color> LeftColumn => m_colors
            .Where((_, index) => index % c_size == 0)
            .ToList();

        public IReadOnlyList<Color> RightColumn => m_colors
            .Where((_, index) => index % c_size == 2)
            .ToList();

        public IReadOnlyList<Color> TopRow => m_colors
            .Take(c_size)
            .ToList();

        public void SetBottomRow(IReadOnlyList<Color> colors)
        {
            if (colors.Count != c_size)
            {
                throw new ArgumentException($"Expected exactly '{c_size}' colors, found '{colors.Count}'.");
            }

            for (var i = c_size * 2; i < c_size * 3; i++)
            {
                m_colors[i] = colors[i];
            }
        }

        public void SetLeftColumn(IReadOnlyList<Color> colors)
        {
            if (colors.Count != c_size)
            {
                throw new ArgumentException($"Expected exactly '{c_size}' colors, found '{colors.Count}'.");
            }

            for (var i = 0; i < 3; i++)
            {
                m_colors[i * c_size] = colors[i];
            }
        }

        public void SetRightColumn(IReadOnlyList<Color> colors)
        {
            if (colors.Count != c_size)
            {
                throw new ArgumentException($"Expected exactly '{c_size}' colors, found '{colors.Count}'.");
            }

            for (var i = 0; i < 3; i++)
            {
                m_colors[i * c_size + 2] = colors[i];
            }
        }

        public void SetTopRow(IReadOnlyList<Color> colors)
        {
            if (colors.Count != c_size)
            {
                throw new ArgumentException($"Expected exactly '{c_size}' colors, found '{colors.Count}'.");
            }

            for (var i = 0; i < c_size; i++)
            {
                m_colors[i] = colors[i];
            }
        }

        public void RotateClockwise()
        {
            var newColors = new Color[c_size * c_size];

            newColors[0] = m_colors[6];
            newColors[1] = m_colors[3];
            newColors[2] = m_colors[0];
            newColors[3] = m_colors[7];
            newColors[4] = m_colors[4];
            newColors[5] = m_colors[1];
            newColors[6] = m_colors[8];
            newColors[7] = m_colors[5];
            newColors[8] = m_colors[2];

            m_colors = newColors;
        }

        public void RotateCounterClockwise()
        {
            var newColors = new Color[c_size * c_size];

            newColors[0] = m_colors[2];
            newColors[1] = m_colors[5];
            newColors[2] = m_colors[8];
            newColors[3] = m_colors[1];
            newColors[4] = m_colors[4];
            newColors[5] = m_colors[7];
            newColors[6] = m_colors[0];
            newColors[7] = m_colors[3];
            newColors[8] = m_colors[6];

            m_colors = newColors;
        }

        public void RotateDouble()
        {
            m_colors = m_colors.Reverse().ToArray();
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
