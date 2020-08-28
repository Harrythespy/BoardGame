using System;
using System.Drawing;

namespace BoardGame
{
    public interface Rule
    {
        string[,] initialBoard();
        void updateBoard(string[,] boardState);
        bool checkWinner(string[,] boardState, Point coordinate, Piece piece);
    }
}
