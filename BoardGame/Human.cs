using System;
using static System.Console;
using System.Drawing;

namespace BoardGame
{
    public class Human : Player
    {
        new Point _Coordinate;
        public new Piece Piece { get; set; }

        public Human(Piece piece) :base(piece)
        {
            Piece = piece;
        }
    }
}
