using System;
using System.Drawing;

namespace BoardGame
{
    public class Computer : Player
    {
        public int[] difficultyIndices = { 1, 2 };
        protected int _Difficulty;
        new Piece Piece;

        public Computer() { }

        public Computer(Piece piece, int difficulty) :base(piece)
        {
            Piece = piece;
            _Difficulty = difficulty;
        }

        public override Point Move(string[,] boardState) 
        {
            if (_Difficulty == 1) return randomSelection(boardState);
            else if (_Difficulty == 2) return SimpleRule();
            else return new Point(-1, -1);
        }

        public Point SimpleRule()
        {
            return new Point();
        }

        public Point randomSelection(string[,] boardState)
        {
            Random r = new Random();
            
            int row = r.Next(0, boardState.GetLength(0) - 1);
            int col = r.Next(0, boardState.GetLength(1) - 1);
            while (boardState[row, col] != " ")
            {
                row = r.Next(0, boardState.GetLength(0) - 1);
                col = r.Next(0, boardState.GetLength(1) - 1);
            }
            return new Point(row, col);
        }

    }
}
