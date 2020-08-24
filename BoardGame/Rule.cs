using System;
using System.Drawing;

namespace BoardGame
{
    public interface Rule
    {
        string[,] initialiBoard();
        void updateBoard(string[,] boardState);
        bool checkWinner(string[,] boardState, Point coordinate, Piece piece);
    }
}
