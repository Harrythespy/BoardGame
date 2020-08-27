using System;
using System.Drawing;
using static System.Console;

namespace BoardGame
{
    public abstract class BoardGame
    {
        protected Human _Player1;
        protected bool _Competitor;
        protected Piece _CompeitorColour;
        protected int _Difficulty;
        protected int _CurrentSteps = 1;
        protected Rule _Rule;
        protected string[,] _BoardState;
        protected History history = new History();
        public bool isOver = false;
        public bool isLeave = false;

        public BoardGame(Rule rule)
        {
            _Rule = rule;
            _BoardState = rule.initialiBoard();
            loadGame();
        }
        public BoardGame(Rule rule, bool competitor, bool colour)
        {
            _Rule = rule;
            _BoardState = rule.initialiBoard();
            _Player1 = new Human(chooseColour(colour));
            //_Competitor = chooseCompetitor(competitor);
            _Competitor = competitor;
            selectDifficulty();
        }

        protected void selectDifficulty()
        {
            int[] indcies = new Computer().difficultyIndices;
            Write("\nEnter index of difficulty.." +
                "\n1. Easy\t2.Difficult" +
                "\n>> ");
            bool isDifficulty = int.TryParse(ReadLine(), out _Difficulty);
            while(!isDifficulty || !Array.Exists<int>(indcies, element => element == _Difficulty))
            {
                Write("Invalid input, please enter again >> ");
                isDifficulty = int.TryParse(ReadLine(), out _Difficulty);
            }
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

        public void playGame(Player competitor)
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
                        Clear();
                        _Rule.updateBoard(_BoardState);
                        break;
                    }
                }
                player = _CurrentSteps % 2 == 0 ? competitor : _Player1;
                displayTerms();
                // get the coordinate from player
                coordinate = player.Move(_BoardState);
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

            isOver = true;
        }

        public void initialGame()
        {
            isOver = false;

            if (_Competitor)
            {
                // Human player
                Human _Player2 = new Human(_CompeitorColour);
                playGame(_Player2);
            }
            else
            {
                // computer player
                Computer _Computer = new Computer(_CompeitorColour, _Difficulty);
                
                playGame(_Computer);
            }
        }

        public void leaveGame()
        {
            // Save game and leave
            Write("\nWould you like to save the game history? Y/n >> ");
            string userInput = ReadLine();
            if(userInput == "Y" || userInput == "y")
            {
                // Save game history.
                bool piece = _Player1.Piece.ToString().Contains("BlackPiece") ? true : false;
                bool competitor = _Competitor.ToString().Contains("Human") ? true : false;
                history.saveHistory(piece, competitor, _Difficulty);
                isLeave = true;
            }

            WriteLine("\nBye bye, see you next time!");
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
            if (player == _Player1) WriteLine("player 1 win!");
            else WriteLine("Player 2 win!");
        }

        public void loadGame()
        {
            Player _Player2;
            Write("Enter the file path >> ");
            string userInput = ReadLine();
            while (userInput == "")
            {
                Write("File path cannot be empty, enter again >> ");
                userInput = ReadLine();
            }

            bool[] attributes = history.loadHistory(userInput);
            if (attributes.Length > 0)
            {
                bool colour = attributes[0];
                bool competitor = attributes[1];
                bool difficulty = attributes[2];
                _Player1 = new Human(chooseColour(colour));
                if (competitor) _Player2 = new Human(_CompeitorColour);
                else _Player2 = new Computer(_CompeitorColour, 1);
                _CurrentSteps = history.gameHistory.Count;

                int prevStep = _CurrentSteps;

                foreach (Point piece in history.gameHistory)
                {
                    Player player = prevStep % 2 == 0 ? _Player2 : _Player1;
                    _BoardState[piece.X, piece.Y] = player.Piece.printPiece();
                    prevStep--;
                }
            }

            else WriteLine("Error occurred while parsing the file.");
            _CurrentSteps++;
        }
    }
}
