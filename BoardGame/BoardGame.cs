using System;
using System.Drawing;
using static System.Console;

namespace BoardGame
{
    public abstract class BoardGame
    {
        protected Human _Player1;
        protected Player _Competitor;
        protected Piece _CompeitorColour;
        protected int _CurrentSteps = 0;
        protected Rule _Rule;
        protected string[,] _BoardState;
        protected History history = new History();

        public BoardGame(Rule rule, bool competitor, bool colour)
        {
            _Rule = rule;
            _BoardState = rule.initialiBoard();
            _Player1 = new Human(chooseColour(colour));
            _Competitor = chooseCompetitor(competitor);
        }

        protected Computer selectDifficulty(Piece competitorPiece)
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
            return new Computer(competitorPiece, difficulty);
        }

        protected Player chooseCompetitor(bool competitor)
        {
            if (competitor) return new Human(_CompeitorColour);
            else return selectDifficulty(_CompeitorColour);
        }

        protected Piece chooseColour(bool colour)
        {
            if (colour) {
                _CompeitorColour = new WhitePiece();
                return new BlackPiece();
            } 
            else
            {
                _CompeitorColour = new BlackPiece();
                return new WhitePiece();
            }
        }

        public void initialGame()
        {
            _Rule.updateBoard(_BoardState);
            do
            {
                _CurrentSteps++;
                while(_CurrentSteps > 1)
                {
                    Write($"Undo the previous step? Y/n >> ");
                    string userInput = ReadLine();
                    if (userInput == "Y" || userInput == "y")
                    {
                        --_CurrentSteps;
                        Point prevStep = history.gameHistory[_CurrentSteps - 1];
                        history.undoStep(prevStep);
                        _BoardState[prevStep.X, prevStep.Y] = " ";
                        _Rule.updateBoard(_BoardState);
                    }
                    else
                    {
                        break;
                    }
                }
                // Decide whether the player want to undo or save the game
                // decide player term
                Player player = _CurrentSteps % 2 == 0 ? _Competitor : _Player1;
                displayTerms();
                // get the coordinate from player
                Point coordinate = player.getCoordinate(_BoardState);
                _BoardState[coordinate.X, coordinate.Y] = player.Piece.printPiece();
                // store new state to history
                history.recordStep(coordinate);
                // update the board state
                _Rule.updateBoard(_BoardState);
            } while (!_Rule.checkWinner());
        }



        public void leaveGame()
        {
            // Save game and leave
            Write("Save the game? Y/n");
            if(ReadLine() == "Y" || ReadLine() == "y")
            {
                // Save game history.
                WriteLine("The game should be saved here.");
            }

            WriteLine("Bye bye, see you next time!");
            ReadKey();
        }

        public void displayTerms()
        {
            string player;
            player = _CurrentSteps % 2 == 0 ? "Player 2" : "PLayer 1";
            WriteLine($"Now it's {player}'s turn");
        }
    }
}
