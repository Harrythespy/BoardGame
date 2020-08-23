﻿using System;

namespace BoardGame
{
    public interface Rule
    {
        string[,] initialiBoard();
        void updateBoard(string[,] boardState);
        bool checkWinner();
    }
}
