using System;
using static System.Console;

namespace BoardGame
{
    public abstract class BoardGame
    {
        protected Player _CompetitorType;
        protected int _CurrentSteps = 0;
        // false of choseColour indicates the black piece;
        protected Piece _ChoseColour;
        protected Rule _Rule;
        protected string[,] _BoardState;

        public BoardGame(Rule rule, bool competitor, bool colour)
        {
            _Rule = rule;
            this._BoardState = rule.initialiBoard();
            _CompetitorType = chooseCompetitor(competitor);
            _ChoseColour = choseColour(colour);
        }

        protected Computer selectDifficulty()
        {
            Write("Please select difficulty.." +
                "\n1. Easy\t2.Difficult" +
                "\n>> ");
            bool isDifficulty = int.TryParse(ReadLine(), out int difficulty);
            while(!isDifficulty)
            {
                Write("Invalid input, please enter again >> ");
                isDifficulty = int.TryParse(ReadLine(), out difficulty);
            }
            return new Computer(difficulty);
        }

        protected Player chooseCompetitor(bool competitor)
        {
            if (competitor) return new Human();
            else return selectDifficulty();
        }

        protected Piece choseColour(bool colour)
        {
            if (colour) return new BlackPiece();
            else return new WhitePiece();
        }

        public void initialGame()
        {
            _Rule.updateBoard(this._BoardState);
            WriteLine($"chose colour is {_ChoseColour.printPiece()}");
            displayTerms();
        }

        public void leaveGame()
        {
            WriteLine("Bye bye, see you next time!");
            ReadKey();
        }
        public void displayTerms()
        {
            string player;
            player = this._CurrentSteps % 2 == 0 ? "Player 1" : "PLayer 2";
            WriteLine($"Now it's {player}'s turn");
        }
    }
}
