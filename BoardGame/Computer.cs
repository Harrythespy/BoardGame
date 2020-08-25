using System;
namespace BoardGame
{
    public class Computer : Player
    {
        public int _Difficulty;
        new Piece Piece;

        public Computer() { }

        public Computer(Piece piece, int difficulty) :base(piece)
        {
            Piece = piece;
            _Difficulty = difficulty;
        }

    }
}
