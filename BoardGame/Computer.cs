using System;
namespace BoardGame
{
    public class Computer : Player
    {
        int[] difficultyIndex = { 1, 2 };
        new Piece Piece;

        public Computer(Piece piece, int difficulty) :base(piece)
        {
            Piece = piece;    
        }

    }
}
