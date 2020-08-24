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
        protected int _CurrentSteps = 1;
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
            // decide player term
            Player player;
            Point coordinate;
            _Rule.updateBoard(_BoardState);
            do
            {
                while (_CurrentSteps > 1)
                {
                    // undo the previous step
                    Write($"Undo the previous step? Y/n >> ");
                    string userInput = ReadLine();
                    if (userInput == "Y" || userInput == "y")
                    {
                        _CurrentSteps--;
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
                player = _CurrentSteps % 2 == 0 ? _Competitor : _Player1;
                displayTerms();
                // get the coordinate from player
                coordinate = player.getCoordinate(_BoardState);
                _BoardState[coordinate.X, coordinate.Y] = player.Piece.printPiece();
                // store new state to history
                history.recordStep(coordinate);
                // update the board state
                _Rule.updateBoard(_BoardState);
                //history.saveHistory();
                _CurrentSteps++;
            } while (!_Rule.checkWinner(_BoardState, coordinate, player.Piece));
            //Clear();
            displayWinner(player);

            ReadKey();
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

        public void displayWinner(Player player)
        {
            if (player == _Player1) WriteLine($"Winner is Player 1!");
            else WriteLine($"Winner is {_Competitor}!");

        }
    }
}
