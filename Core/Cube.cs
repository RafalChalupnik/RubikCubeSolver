using System;
using System.Collections.Generic;
using System.Linq;

namespace RubikCubeSolver.Core
{
    public class Cube
    {
        public Side Back { get; }

        public Side Down { get; }

        public Side Front { get; }

        public Side Left { get; }

        public Side Right { get; }

        public Side Top { get; }

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

        public void Rotate(Rotation rotation)
        {
            switch (rotation)
            {
                case Rotation.BackClockwise: BackRotateClockwise(); break;
                case Rotation.BackCounterClockwise: BackRotateCounterClockwise(); break;
                case Rotation.BackDouble: BackRotateDouble(); break;
                case Rotation.DownClockwise: DownRotateClockwise(); break;
                case Rotation.DownCounterClockwise: DownRotateCounterClockwise(); break;
                case Rotation.DownDouble: DownRotateDouble(); break;
                case Rotation.FrontClockwise: FrontRotateClockwise(); break;
                case Rotation.FrontCounterClockwise: FrontRotateCounterClockwise(); break;
                case Rotation.FrontDouble: FrontRotateDouble(); break;
                case Rotation.LeftClockwise: LeftRotateClockwise(); break;
                case Rotation.LeftCounterClockwise: LeftRotateCounterClockwise(); break;
                case Rotation.LeftDouble: LeftRotateDouble(); break;
                case Rotation.RightClockwise: RightRotateClockwise(); break;
                case Rotation.RightCounterClockwise: RightRotateCounterClockwise(); break;
                case Rotation.RightDouble: RightRotateDouble(); break;
                case Rotation.TopClockwise: TopRotateClockwise(); break;
                case Rotation.TopCounterClockwise: TopRotateCounterClockwise(); break;
                case Rotation.TopDouble: TopRotateDouble(); break;
                default: throw new ArgumentException($"Invalid rotation: '{rotation}'.");
            }
        }

        private void BackRotateClockwise()
        {
            var newDownRow = Left.LeftColumn.Reverse();
            var newTopRow = Right.RightColumn;
            var newLeftColumn = Top.TopRow.Reverse();
            var newRightColumn = Down.TopRow;

            Back.RotateClockwise();
            Down.SetTopRow(newDownRow);
            Top.SetTopRow(newTopRow);
            Left.SetLeftColumn(newLeftColumn);
            Right.SetRightColumn(newRightColumn);
        }

        private void BackRotateCounterClockwise()
        {
            var newDownRow = Right.RightColumn;
            var newTopRow = Left.LeftColumn.Reverse();
            var newLeftColumn = Down.TopRow.Reverse();
            var newRightColumn = Top.TopRow;

            Back.RotateCounterClockwise();
            Down.SetTopRow(newDownRow);
            Top.SetTopRow(newTopRow);
            Left.SetLeftColumn(newLeftColumn);
            Right.SetRightColumn(newRightColumn);
        }

        private void BackRotateDouble()
        {
            var newDownRow = Top.TopRow;
            var newTopRow = Down.TopRow;
            var newLeftColumn = Right.RightColumn.Reverse();
            var newRightColumn = Left.LeftColumn.Reverse();

            Back.RotateDouble();
            Down.SetTopRow(newDownRow);
            Top.SetTopRow(newTopRow);
            Left.SetLeftColumn(newLeftColumn);
            Right.SetRightColumn(newRightColumn);
        }
    
        private void DownRotateClockwise()
        {
            var newBackRow = Right.BottomRow;
            var newFrontRow = Left.BottomRow;
            var newLeftRow = Back.BottomRow;
            var newRightRow = Front.BottomRow;

            Down.RotateClockwise();
            Back.SetBottomRow(newBackRow);
            Front.SetBottomRow(newFrontRow);
            Left.SetBottomRow(newLeftRow);
            Right.SetBottomRow(newRightRow);
        }

        private void DownRotateCounterClockwise()
        {
            var newBackRow = Left.BottomRow;
            var newFrontRow = Right.BottomRow;
            var newLeftRow = Front.BottomRow;
            var newRightRow = Back.BottomRow;

            Down.RotateCounterClockwise();
            Back.SetBottomRow(newBackRow);
            Front.SetBottomRow(newFrontRow);
            Left.SetBottomRow(newLeftRow);
            Right.SetBottomRow(newRightRow);
        }

        private void DownRotateDouble()
        {
            var newBackRow = Front.BottomRow;
            var newFrontRow = Back.BottomRow;
            var newLeftRow = Right.BottomRow;
            var newRightRow = Left.BottomRow;

            Down.RotateDouble();
            Back.SetBottomRow(newBackRow);
            Front.SetBottomRow(newFrontRow);
            Left.SetBottomRow(newLeftRow);
            Right.SetBottomRow(newRightRow);
        }
    
        private void FrontRotateClockwise()
        {
            var newDownRow = Right.LeftColumn;
            var newTopRow = Left.RightColumn.Reverse();
            var newLeftColumn = Down.BottomRow.Reverse();
            var newRightColumn = Top.BottomRow;

            Front.RotateClockwise();
            Down.SetBottomRow(newDownRow);
            Top.SetBottomRow(newTopRow);
            Left.SetRightColumn(newLeftColumn);
            Right.SetLeftColumn(newRightColumn);
        }

        private void FrontRotateCounterClockwise()
        {
            var newDownRow = Left.RightColumn.Reverse();
            var newTopRow = Right.LeftColumn;
            var newLeftColumn = Top.BottomRow.Reverse();
            var newRightColumn = Down.BottomRow;

            Front.RotateCounterClockwise();
            Down.SetBottomRow(newDownRow);
            Top.SetBottomRow(newTopRow);
            Left.SetRightColumn(newLeftColumn);
            Right.SetLeftColumn(newRightColumn);
        }

        private void FrontRotateDouble()
        {
            var newDownRow = Top.BottomRow;
            var newTopRow = Down.BottomRow;
            var newLeftColumn = Right.LeftColumn.Reverse();
            var newRightColumn = Left.RightColumn.Reverse();

            Front.RotateDouble();
            Down.SetBottomRow(newDownRow);
            Top.SetBottomRow(newTopRow);
            Left.SetRightColumn(newLeftColumn);
            Right.SetLeftColumn(newRightColumn);
        }
    
        private void LeftRotateClockwise()
        {
            var newTopColumn = Back.RightColumn.Reverse();
            var newDownColumn = Front.LeftColumn.Reverse();
            var newFrontColumn = Top.LeftColumn;
            var newBackColumn = Down.RightColumn;

            Left.RotateClockwise();

            Top.SetLeftColumn(newTopColumn);
            Down.SetRightColumn(newDownColumn);
            Front.SetLeftColumn(newFrontColumn);
            Back.SetRightColumn(newBackColumn);
        }

        private void LeftRotateCounterClockwise()
        {
            var newTopColumn = Front.LeftColumn;
            var newDownColumn = Back.RightColumn;
            var newFrontColumn = Down.RightColumn.Reverse();
            var newBackColumn = Top.LeftColumn.Reverse();

            Left.RotateCounterClockwise();

            Top.SetLeftColumn(newTopColumn);
            Down.SetRightColumn(newDownColumn);
            Front.SetLeftColumn(newFrontColumn);
            Back.SetRightColumn(newBackColumn);
        }

        private void LeftRotateDouble()
        {
            var newTopColumn = Down.RightColumn.Reverse();
            var newDownColumn = Top.LeftColumn.Reverse();
            var newFrontColumn = Back.RightColumn.Reverse();
            var newBackColumn = Front.LeftColumn.Reverse();

            Left.RotateDouble();

            Top.SetLeftColumn(newTopColumn);
            Down.SetRightColumn(newDownColumn);
            Front.SetLeftColumn(newFrontColumn);
            Back.SetRightColumn(newBackColumn);
        }

        private void RightRotateClockwise()
        {
            var newTopColumn = Front.RightColumn;
            var newDownColumn = Back.LeftColumn;
            var newFrontColumn = Down.LeftColumn.Reverse();
            var newBackColumn = Top.RightColumn.Reverse();

            Right.RotateClockwise();

            Top.SetRightColumn(newTopColumn);
            Down.SetLeftColumn(newDownColumn);
            Front.SetRightColumn(newFrontColumn);
            Back.SetLeftColumn(newBackColumn);
        }

        private void RightRotateCounterClockwise()
        {
            var newTopColumn = Back.LeftColumn.Reverse();
            var newDownColumn = Front.RightColumn.Reverse();
            var newFrontColumn = Top.RightColumn;
            var newBackColumn = Down.LeftColumn;

            Right.RotateCounterClockwise();

            Top.SetRightColumn(newTopColumn);
            Down.SetLeftColumn(newDownColumn);
            Front.SetRightColumn(newFrontColumn);
            Back.SetLeftColumn(newBackColumn);
        }

        private void RightRotateDouble()
        {
            var newTopColumn = Down.LeftColumn.Reverse();
            var newDownColumn = Top.RightColumn.Reverse();
            var newFrontColumn = Back.LeftColumn.Reverse();
            var newBackColumn = Front.RightColumn.Reverse();

            Right.RotateDouble();

            Top.SetRightColumn(newTopColumn);
            Down.SetLeftColumn(newDownColumn);
            Front.SetRightColumn(newFrontColumn);
            Back.SetLeftColumn(newBackColumn);
        }

        private void TopRotateClockwise()
        {
            var newBackRow = Left.TopRow;
            var newFrontRow = Right.TopRow;
            var newLeftRow = Front.TopRow;
            var newRightRow = Back.TopRow;

            Top.RotateClockwise();
            Back.SetTopRow(newBackRow);
            Front.SetTopRow(newFrontRow);
            Left.SetTopRow(newLeftRow);
            Right.SetTopRow(newRightRow);
        }

        private void TopRotateCounterClockwise()
        {
            var newBackRow = Right.TopRow;
            var newFrontRow = Left.TopRow;
            var newLeftRow = Back.TopRow;
            var newRightRow = Front.TopRow;

            Top.RotateCounterClockwise();
            Back.SetTopRow(newBackRow);
            Front.SetTopRow(newFrontRow);
            Left.SetTopRow(newLeftRow);
            Right.SetTopRow(newRightRow);
        }

        private void TopRotateDouble()
        {
            var newBackRow = Front.TopRow;
            var newFrontRow = Back.TopRow;
            var newLeftRow = Right.TopRow;
            var newRightRow = Left.TopRow;

            Top.RotateDouble();
            Back.SetTopRow(newBackRow);
            Front.SetTopRow(newFrontRow);
            Left.SetTopRow(newLeftRow);
            Right.SetTopRow(newRightRow);
        }
    }
}
