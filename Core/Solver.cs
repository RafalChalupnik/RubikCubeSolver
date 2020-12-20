using System;
using System.Collections.Generic;
using RubikCubeSolver.Core.Extensions;

namespace RubikCubeSolver.Core
{
    public static class Solver
    {
        private static readonly IReadOnlyCollection<Rotation> s_possibleRotations = Enum.GetValues(typeof(Rotation));

        public static List<Rotation> Solve(Cube cube)
            => Foo(cube, depth: 20);

        private static List<Rotation> Foo(Cube cube, int depth)
        {
            if (depth <= 0)
            {
                return cube.IsSolved
                    ? new List<Rotation>()
                    : null;
            }

            foreach (var rotation in s_possibleRotations)
            {
                var newCube = cube.Rotate(rotation);
                var node = Foo(newCube, depth - 1);

                if (node != null)
                {
                    return rotation.AsList()
                        .Concat(node)
                        .ToList();
                }
            }

            return null;
        }
    }
}