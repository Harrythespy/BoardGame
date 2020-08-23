using System;
using static System.Console;

namespace BoardGame
{
    public class GomokuRule : Rule
    {

        public GomokuRule(){}

        public string[,] initialiBoard()
        {
            string[,] initialArr = new string[15, 15];

            for (int i = 0; i < initialArr.GetLength(0); i++)
            {
                for (int j = 0; j < initialArr.GetLength(1); j++)
                {
                    initialArr[i, j] = " ";
                }
            }

            return initialArr;
        }

        public void updateBoard(string[,] boardState)
        {
            // Print the index of X axis
            for (int i = 1; i <= 15; i++)
            {
                Write(i);
                Write("\t");
            }

            WriteLine();

            for (int i = 0; i < boardState.GetLength(0); i++)
            {
                if (i == boardState.GetLength(0) - 1)
                {
                    // Piece line
                    for (int j = 0; j < boardState.GetLength(1); j++)
                    {
                        if (j == boardState.GetLength(1) - 1)
                        {
                            Write(boardState[i, j]);
                            Write($" {i + 1}");
                            WriteLine();
                        }
                        else
                        {
                            Write(boardState[i, j]);
                            Write("  ---  ");
                        }
                    }
                }
                else
                {
                    // Piece line
                    for (int j = 0; j < boardState.GetLength(1); j++)
                    {
                        if (j == boardState.GetLength(1) - 1)
                        {
                            Write(boardState[i, j]);
                            Write($" {i + 1}");
                            WriteLine();
                        }
                        else
                        {
                            Write(boardState[i, j]);
                            Write("  ---  ");
                        }
                    }
                    // Space line
                    for (int x = 0; x < boardState.GetLength(1); x++)
                    {
                        if (x == boardState.GetLength(1) - 1)
                        {
                            Write("|   ");
                            WriteLine("\t");
                        }
                        else
                        {
                            Write("|   ");
                            Write("\t");
                        }
                    }
                }
            }
        }

        public bool checkWinner()
        {
            return false;
        }
    }
}
