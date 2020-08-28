using System;
using System.Drawing;

namespace BoardGame
{
    public class Computer : Player
    {
        public int[] difficultyIndices = { 1, 2 };
        protected int _Difficulty;
        protected double[,] _BoardScore;
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
            else if (_Difficulty == 2) return SimpleRule(boardState);
            else return new Point(-1, -1);
        }

        public Point SimpleRule(string[,] boardState)
        {
            _BoardScore = new double[boardState.GetLength(0), boardState.GetLength(1)];
            analyzeHorizontalSets(boardState);
            
            int xBest = -1, yBest = -1;
            double bestScore = 0;

            for (int i = 0; i < _BoardScore.GetLength(0); i++)
            {
                for (int j = 0; j < _BoardScore.GetLength(1); j++)
                {
                    if (_BoardScore[i, j] > bestScore)
                    {
                        if (boardState[i, j] == " ")
                        {
                            Console.WriteLine(_BoardScore[i, j]);
                            bestScore = _BoardScore[i, j];
                            Console.WriteLine(bestScore);
                            xBest = i;
                            yBest = j;
                        }
                    }
                }
            }
            
            return new Point(xBest, yBest);
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

        public double shapeScore(int consecutive, int openEnds)
        {
            if (openEnds == 0 && consecutive < 5)
                return 0;
            else
            {
                switch (consecutive)
                {
                    case 4:
                        switch (openEnds)
                        {
                            case 1:
                                return 100000000;
                            case 2:
                                return 100000000;
                        }
                        break;

                    case 3:
                        switch (openEnds)
                        {
                            case 1:
                                return 7;
                            case 2:
                                return 10000;
                        }
                        break;

                    case 2:
                        switch (openEnds)
                        {
                            case 1:
                                return 2;
                            case 2:
                                return 5;
                        }
                        break;

                    case 1:
                        switch (openEnds)
                        {
                            case 1:
                                return 0.5;
                            case 2:
                                return 1;
                        }
                        break;

                    default:
                        return 200000000;
                }
                return 200000000;
            }
        }

        public void analyzeHorizontalSets(string[,] boardState)
        {
            double score = 0;
            int countConsecutive = 1;
            int openEnds = 0;
            
            for (int i = 0; i < boardState.GetLength(0); i++)
            {
                for (int j = 0; j < boardState.GetLength(1); j++)
                {
                    if (boardState[i, j] == Piece.printPiece())
                        countConsecutive++;
                        
                    else if (boardState[i, j] == " " && countConsecutive > 0)
                    {
                        openEnds++;
                        score += shapeScore(countConsecutive,
                            openEnds);
                        countConsecutive = 0;
                        openEnds = 1;
                    }
                    else if (boardState[i, j] == " ")
                        openEnds = 1;
                    else if (countConsecutive > 0)
                    {
                        score += shapeScore(countConsecutive,
                            openEnds);
                        countConsecutive = 0;
                        openEnds = 0;
                    }
                    else openEnds = 0;

                    _BoardScore[i, j] = score;
                }
                countConsecutive = 1;
                openEnds = 0;
                score = 0;
            }
        }
    }
}
