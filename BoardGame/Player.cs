using System;
using static System.Console;
using System.Drawing;

namespace BoardGame
{
    public abstract class Player
    {
        protected Point _Coordinate;

        public Piece Piece { get; set; }

        public Player() {}

        public Player(Piece piece)
        {
            Piece = piece;
        }

        public abstract Point Move(string[,] boardState);
    }
}
