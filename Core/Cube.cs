using System;
using System.Collections.Generic;
using System.Linq;

namespace RubikCubeSolver.Core
{
    public record Cube
    {
        public Side Back { get; init; }

        public Side Down { get; init; }

        public Side Front { get; init; }

        public Side Left { get; init; }

        public Side Right { get; init; }

        public Side Top { get; init; }

        public Cube()
        {
            Back = new Side(Color.Red);
            Down = new Side(Color.White);
            Front = new Side(Color.Orange);
            Left = new Side(Color.Green);
            Back = new Side(Color.Blue);
            Top = new Side(Color.Yellow);
        }

        public Cube(
            Side back,
            Side down,
            Side front,
            Side left,
            Side right,
            Side top
        )
        {
            Back = back;
            Down = down;
            Front = front;
            Left = left;
            Right = right;
            Top = top;
        }

        public bool IsSolved
        {
            get
            {
                return Back.IsOneColor
                    && Down.IsOneColor
                    && Front.IsOneColor
                    && Left.IsOneColor
                    && Right.IsOneColor
                    && Top.IsOneColor;
            }
        }

        public Cube Rotate(Rotation rotation)
        {
            switch (rotation)
            {
                case Rotation.BackClockwise: return BackRotateClockwise();
                case Rotation.BackCounterClockwise: return BackRotateCounterClockwise();
                case Rotation.BackDouble: return BackRotateDouble();
                case Rotation.DownClockwise: return DownRotateClockwise();
                case Rotation.DownCounterClockwise: return DownRotateCounterClockwise();
                case Rotation.DownDouble: return DownRotateDouble();
                case Rotation.FrontClockwise: return FrontRotateClockwise();
                case Rotation.FrontCounterClockwise: return FrontRotateCounterClockwise();
                case Rotation.FrontDouble: return FrontRotateDouble();
                case Rotation.LeftClockwise: return LeftRotateClockwise();
                case Rotation.LeftCounterClockwise: return LeftRotateCounterClockwise();
                case Rotation.LeftDouble: return LeftRotateDouble();
                case Rotation.RightClockwise: return RightRotateClockwise();
                case Rotation.RightCounterClockwise: return RightRotateCounterClockwise();
                case Rotation.RightDouble: return RightRotateDouble();
                case Rotation.TopClockwise: return TopRotateClockwise();
                case Rotation.TopCounterClockwise: return TopRotateCounterClockwise();
                case Rotation.TopDouble: return TopRotateDouble();
                default: throw new ArgumentException($"Invalid rotation: '{rotation}'.");
            }
        }

        private Cube BackRotateClockwise()
        {
            var newDownRow = Left.LeftColumn.Reverse().ToList();
            var newLeftColumn = Top.TopRow.Reverse().ToList();
            var newRightColumn = Down.TopRow;
            var newTopRow = Right.RightColumn;

            var newBack = Back.RotateClockwise();
            var newDown = Down.SetTopRow(newDownRow);
            var newLeft = Left.SetLeftColumn(newLeftColumn);
            var newRight = Right.SetRightColumn(newRightColumn);
            var newTop = Top.SetTopRow(newTopRow);

            return this with 
            {
                Back = newBack,
                Down = newDown,
                Left = newLeft,
                Right = newRight,
                Top = newTop
            };
        }

        private Cube BackRotateCounterClockwise()
        {
            var newDownRow = Right.RightColumn;
            var newTopRow = Left.LeftColumn.Reverse().ToList();
            var newLeftColumn = Down.TopRow.Reverse().ToList();
            var newRightColumn = Top.TopRow;

            var newBack = Back.RotateCounterClockwise();
            var newDown = Down.SetTopRow(newDownRow);
            var newLeft = Left.SetLeftColumn(newLeftColumn);
            var newRight = Right.SetRightColumn(newRightColumn);
            var newTop = Top.SetTopRow(newTopRow);

            return this with 
            {
                Back = newBack,
                Down = newDown,
                Left = newLeft,
                Right = newRight,
                Top = newTop
            };
        }

        private Cube BackRotateDouble()
        {
            var newDownRow = Top.TopRow;
            var newTopRow = Down.TopRow;
            var newLeftColumn = Right.RightColumn.Reverse().ToList();
            var newRightColumn = Left.LeftColumn.Reverse().ToList();

            var newBack = Back.RotateDouble();
            var newDown = Down.SetTopRow(newDownRow);
            var newLeft = Left.SetLeftColumn(newLeftColumn);
            var newRight = Right.SetRightColumn(newRightColumn);
            var newTop = Top.SetTopRow(newTopRow);

            return this with 
            {
                Back = newBack,
                Down = newDown,
                Left = newLeft,
                Right = newRight,
                Top = newTop
            };
        }
    
        private Cube DownRotateClockwise()
        {
            var newBackRow = Right.BottomRow;
            var newFrontRow = Left.BottomRow;
            var newLeftRow = Back.BottomRow;
            var newRightRow = Front.BottomRow;

            var newBack = Back.SetBottomRow(newBackRow);
            var newDown = Down.RotateClockwise();
            var newFront = Front.SetBottomRow(newFrontRow);
            var newLeft = Left.SetBottomRow(newLeftRow);
            var newRight = Right.SetBottomRow(newRightRow);

            return this with 
            {
                Back = newBack,
                Down = newDown,
                Front = newFront,
                Left = newLeft,
                Right = newRight
            };
        }

        private Cube DownRotateCounterClockwise()
        {
            var newBackRow = Left.BottomRow;
            var newFrontRow = Right.BottomRow;
            var newLeftRow = Front.BottomRow;
            var newRightRow = Back.BottomRow;

            var newBack = Back.SetBottomRow(newBackRow);
            var newDown = Down.RotateCounterClockwise();
            var newFront = Front.SetBottomRow(newFrontRow);
            var newLeft = Left.SetBottomRow(newLeftRow);
            var newRight = Right.SetBottomRow(newRightRow);

            return this with 
            {
                Back = newBack,
                Down = newDown,
                Front = newFront,
                Left = newLeft,
                Right = newRight
            };
        }

        private Cube DownRotateDouble()
        {
            var newBackRow = Front.BottomRow;
            var newFrontRow = Back.BottomRow;
            var newLeftRow = Right.BottomRow;
            var newRightRow = Left.BottomRow;

            var newBack = Back.SetBottomRow(newBackRow);
            var newDown = Down.RotateDouble();
            var newFront = Front.SetBottomRow(newFrontRow);
            var newLeft = Left.SetBottomRow(newLeftRow);
            var newRight = Right.SetBottomRow(newRightRow);

            return this with 
            {
                Back = newBack,
                Down = newDown,
                Front = newFront,
                Left = newLeft,
                Right = newRight
            };
        }
    
        private Cube FrontRotateClockwise()
        {
            var newDownRow = Right.LeftColumn;
            var newTopRow = Left.RightColumn.Reverse().ToList();
            var newLeftColumn = Down.BottomRow.Reverse().ToList();
            var newRightColumn = Top.BottomRow;

            var newDown = Down.SetBottomRow(newDownRow);
            var newFront = Front.RotateClockwise();
            var newLeft = Left.SetRightColumn(newLeftColumn);
            var newRight = Right.SetLeftColumn(newRightColumn);
            var newTop = Top.SetBottomRow(newTopRow);

            return this with 
            {
                Down = newDown,
                Front = newFront,
                Left = newLeft,
                Right = newRight,
                Top = newTop
            };
        }

        private Cube FrontRotateCounterClockwise()
        {
            var newDownRow = Left.RightColumn.Reverse().ToList();
            var newTopRow = Right.LeftColumn;
            var newLeftColumn = Top.BottomRow.Reverse().ToList();
            var newRightColumn = Down.BottomRow;

            var newDown = Down.SetBottomRow(newDownRow);
            var newFront = Front.RotateCounterClockwise();
            var newLeft = Left.SetRightColumn(newLeftColumn);
            var newRight = Right.SetLeftColumn(newRightColumn);
            var newTop = Top.SetBottomRow(newTopRow);

            return this with 
            {
                Down = newDown,
                Front = newFront,
                Left = newLeft,
                Right = newRight,
                Top = newTop
            };
        }

        private Cube FrontRotateDouble()
        {
            var newDownRow = Top.BottomRow;
            var newTopRow = Down.BottomRow;
            var newLeftColumn = Right.LeftColumn.Reverse().ToList();
            var newRightColumn = Left.RightColumn.Reverse().ToList();

            var newDown = Down.SetBottomRow(newDownRow);
            var newFront = Front.RotateDouble();
            var newLeft = Left.SetRightColumn(newLeftColumn);
            var newRight = Right.SetLeftColumn(newRightColumn);
            var newTop = Top.SetBottomRow(newTopRow);

            return this with 
            {
                Down = newDown,
                Front = newFront,
                Left = newLeft,
                Right = newRight,
                Top = newTop
            };
        }
    
        private Cube LeftRotateClockwise()
        {
            var newTopColumn = Back.RightColumn.Reverse().ToList();
            var newDownColumn = Front.LeftColumn.Reverse().ToList();
            var newFrontColumn = Top.LeftColumn;
            var newBackColumn = Down.RightColumn;

            var newBack = Back.SetRightColumn(newBackColumn);
            var newDown = Down.SetRightColumn(newDownColumn);
            var newFront = Front.SetLeftColumn(newFrontColumn);
            var newLeft = Left.RotateClockwise();
            var newTop = Top.SetLeftColumn(newTopColumn);

            return this with 
            {
                Back = newBack,
                Down = newDown,
                Front = newFront,
                Left = newLeft,
                Top = newTop
            };
        }

        private Cube LeftRotateCounterClockwise()
        {
            var newTopColumn = Front.LeftColumn;
            var newDownColumn = Back.RightColumn;
            var newFrontColumn = Down.RightColumn.Reverse().ToList();
            var newBackColumn = Top.LeftColumn.Reverse().ToList();

            var newBack = Back.SetRightColumn(newBackColumn);
            var newDown = Down.SetRightColumn(newDownColumn);
            var newFront = Front.SetLeftColumn(newFrontColumn);
            var newLeft = Left.RotateCounterClockwise();
            var newTop = Top.SetLeftColumn(newTopColumn);

            return this with 
            {
                Back = newBack,
                Down = newDown,
                Front = newFront,
                Left = newLeft,
                Top = newTop
            };
        }

        private Cube LeftRotateDouble()
        {
            var newTopColumn = Down.RightColumn.Reverse().ToList();
            var newDownColumn = Top.LeftColumn.Reverse().ToList();
            var newFrontColumn = Back.RightColumn.Reverse().ToList();
            var newBackColumn = Front.LeftColumn.Reverse().ToList();

            var newBack = Back.SetRightColumn(newBackColumn);
            var newDown = Down.SetRightColumn(newDownColumn);
            var newFront = Front.SetLeftColumn(newFrontColumn);
            var newLeft = Left.RotateDouble();
            var newTop = Top.SetLeftColumn(newTopColumn);

            return this with 
            {
                Back = newBack,
                Down = newDown,
                Front = newFront,
                Left = newLeft,
                Top = newTop
            };
        }

        private Cube RightRotateClockwise()
        {
            var newTopColumn = Front.RightColumn;
            var newDownColumn = Back.LeftColumn;
            var newFrontColumn = Down.LeftColumn.Reverse().ToList();
            var newBackColumn = Top.RightColumn.Reverse().ToList();

            var newBack = Back.SetLeftColumn(newBackColumn);
            var newDown = Down.SetLeftColumn(newDownColumn);
            var newFront = Front.SetRightColumn(newFrontColumn);
            var newRight = Right.RotateClockwise();
            var newTop = Top.SetRightColumn(newTopColumn);

            return this with 
            {
                Back = newBack,
                Down = newDown,
                Front = newFront,
                Right = newRight,
                Top = newTop
            };
        }

        private Cube RightRotateCounterClockwise()
        {
            var newTopColumn = Back.LeftColumn.Reverse().ToList();
            var newDownColumn = Front.RightColumn.Reverse().ToList();
            var newFrontColumn = Top.RightColumn;
            var newBackColumn = Down.LeftColumn;

            var newBack = Back.SetLeftColumn(newBackColumn);
            var newDown = Down.SetLeftColumn(newDownColumn);
            var newFront = Front.SetRightColumn(newFrontColumn);
            var newRight = Right.RotateCounterClockwise();
            var newTop = Top.SetRightColumn(newTopColumn);

            return this with 
            {
                Back = newBack,
                Down = newDown,
                Front = newFront,
                Right = newRight,
                Top = newTop
            };
        }

        private Cube RightRotateDouble()
        {
            var newTopColumn = Down.LeftColumn.Reverse().ToList();
            var newDownColumn = Top.RightColumn.Reverse().ToList();
            var newFrontColumn = Back.LeftColumn.Reverse().ToList();
            var newBackColumn = Front.RightColumn.Reverse().ToList();

            var newBack = Back.SetLeftColumn(newBackColumn);
            var newDown = Down.SetLeftColumn(newDownColumn);
            var newFront = Front.SetRightColumn(newFrontColumn);
            var newRight = Right.RotateDouble();
            var newTop = Top.SetRightColumn(newTopColumn);

            return this with 
            {
                Back = newBack,
                Down = newDown,
                Front = newFront,
                Right = newRight,
                Top = newTop
            };
        }

        private Cube TopRotateClockwise()
        {
            var newBackRow = Left.TopRow;
            var newFrontRow = Right.TopRow;
            var newLeftRow = Front.TopRow;
            var newRightRow = Back.TopRow;

            var newBack = Back.SetTopRow(newBackRow);
            var newFront = Front.SetTopRow(newFrontRow);
            var newLeft = Left.SetTopRow(newLeftRow);
            var newRight = Right.SetTopRow(newRightRow);
            var newTop = Top.RotateClockwise();

            return this with 
            {
                Back = newBack,
                Front = newFront,
                Left = newLeft,
                Right = newRight,
                Top = newTop
            };
        }

        private Cube TopRotateCounterClockwise()
        {
            var newBackRow = Right.TopRow;
            var newFrontRow = Left.TopRow;
            var newLeftRow = Back.TopRow;
            var newRightRow = Front.TopRow;

            var newBack = Back.SetTopRow(newBackRow);
            var newFront = Front.SetTopRow(newFrontRow);
            var newLeft = Left.SetTopRow(newLeftRow);
            var newRight = Right.SetTopRow(newRightRow);
            var newTop = Top.RotateClockwise();

            return this with 
            {
                Back = newBack,
                Front = newFront,
                Left = newLeft,
                Right = newRight,
                Top = newTop
            };
        }

        private Cube TopRotateDouble()
        {
            var newBackRow = Front.TopRow;
            var newFrontRow = Back.TopRow;
            var newLeftRow = Right.TopRow;
            var newRightRow = Left.TopRow;

            var newBack = Back.SetTopRow(newBackRow);
            var newFront = Front.SetTopRow(newFrontRow);
            var newLeft = Left.SetTopRow(newLeftRow);
            var newRight = Right.SetTopRow(newRightRow);
            var newTop = Top.RotateClockwise();

            return this with 
            {
                Back = newBack,
                Front = newFront,
                Left = newLeft,
                Right = newRight,
                Top = newTop
            };
        }
    }
}
