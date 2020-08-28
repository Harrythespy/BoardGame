using System;
using System.Drawing;
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

        public bool checkWinner(string[,] boardState, Point coordinate, Piece piece)
        {
            string _Piece = piece.printPiece();
            // check horizontal
            int horizontal = checkHorizontal(boardState, coordinate, _Piece);
            // [0] = consecutive, [1] = openEnds
            int vertical = checkVertical(boardState, coordinate, _Piece);
            int diagonalLeft = checkDiagonalLeft(boardState, coordinate, _Piece);
            int diagonalRight = checkDiagonalRight(boardState, coordinate, _Piece);
            //updateScore();
            if (horizontal >= 5 || vertical >= 5 || diagonalLeft >= 5 || diagonalRight >= 5) return true;
            else return false;
        }

        public int checkHorizontal(string[,] boardState, Point coordinate, string piece)
        {
            int consecutive = 1;
            // search forward
            for (int i = coordinate.Y - 1; i >= 0; i--)
            {
                if (boardState[coordinate.X, i] == piece) consecutive++;
                else break;
            }
            // search backward
            for (int i = coordinate.Y + 1; i < boardState.GetLength(1); i++)
            {
                if (boardState[coordinate.X, i] == piece) consecutive++;
                else break;
            }
            return consecutive;
        }

        public int checkVertical(string[,] boardState, Point coordinate, string piece)
        {
            int consecutive = 1;
            // search forward
            for (int i = coordinate.X - 1; i >= 0; i--)
            {
                if (boardState[i, coordinate.Y] == piece) consecutive++;
                else break;
            }
            // search backward
            for (int i = coordinate.X + 1; i < boardState.GetLength(0); i++)
            {
                if (boardState[i, coordinate.Y] == piece) consecutive++;
                else break;
            }
            return consecutive;
        }

        public int checkDiagonalLeft(string[,] boardState, Point coordinate, string piece)
        {
            int consecutive = 1;
            // search forward
            int j = coordinate.Y - 1;
            for (int i = coordinate.X - 1; i >= 0; i--, j--)
            {
                if (j >= 0)
                {
                    if (boardState[i, j] == piece) consecutive++;
                    else break;
                }
            }

            j = coordinate.Y + 1;
            for (int i = coordinate.X + 1; i < boardState.GetLength(0); i++, j++)
            {
                if (j < boardState.GetLength(1))
                {
                    if (boardState[i, j] == piece) consecutive++;
                    else break;
                }
            }
            return consecutive;
        }

        public int checkDiagonalRight(string[,] boardState, Point coordinate, string piece)
        {
            int consecutive = 1;
            int j = coordinate.Y + 1;
            for (int i = coordinate.X - 1; i >= 0; i--, j++)
            {
                if (j < boardState.GetLength(1))
                {
                    if (boardState[i, j] == piece) consecutive++;
                    else break;
                }
            }

            j = coordinate.Y - 1;
            for (int i = coordinate.X + 1; i < boardState.GetLength(0); i++, j--)
            {
                if (j >= 0)
                {
                    if (boardState[i, j] == piece) consecutive++;
                    else break;
                }
            }
            return consecutive;
        }
    }
}
