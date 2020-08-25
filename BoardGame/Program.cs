using System;
using static System.Console;
using System.IO;
using System.Linq;

namespace BoardGame
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            // Games that can play in the program.
            string[] games = { "Gomoku" , "Othello", "Mills" };

            WriteLine("Hello! Welcome to Board Game Program");
            WriteLine($"Here are the games that you can play in the program");
            for (int i = 0; i < games.Length; i++) { WriteLine($"{i+1}. {games[i]}"); }

            Write("\nEnter the index of the game you want to play >> ");
            Int32.TryParse(ReadLine(), out int choseGame);

            // Check if the index of the game is not valid.
            while (choseGame == 0 || choseGame > games.Length)
            {
                Write("\nIndex is not valid, please try again >>");
                Int32.TryParse(ReadLine(), out choseGame);
            }
            Clear();

            // Decide the rule of the game.
            Rule rule;

            switch(choseGame)
            {
                case 1:
                    rule = new GomokuRule();
                    break;
                case 2:
                    rule = new GomokuRule();
                    // Rule for Othello
                    break;
                case 3:
                    rule = new GomokuRule();
                    // Rule for Mills
                    break;
                default:
                    rule = new GomokuRule();
                    WriteLine("Cannot find the given rule.");
                    break;
            }
            Game game;
            Write("Load exitsed game from directory? Y/n >> ");
            string userInput = ReadLine();
            if (userInput == "Y" || userInput == "y")
            {
                game = new Game(rule);
                game.initialGame();
            }
            else
            {
                bool isComputer = selectedCompetitor();
                bool isBlack = selectedPieceColour();
                Clear();
                // Initial the selected game
                game = new Game(rule, isComputer, isBlack);
                game.initialGame();
            }
            
            while(game.isOver)
            {
                Write("\nStart another game? Y/n >> ");
                userInput = ReadLine();
                if (userInput == "Y" || userInput == "y")
                {
                    Clear();
                    bool isComputer = selectedCompetitor();
                    bool isBlack = selectedPieceColour();
                    Clear();
                    // Initial the selected game
                    game = new Game(rule, isComputer, isBlack);
                    game.initialGame();
                }
                else
                {
                    game.leaveGame();
                }
            }
            
            
        }

        public static bool selectedCompetitor()
        {
            string[] yes = { "Y", "y" };
            string[] no = { "N", "n" };
            // Select competitor
            Write("Play with AI? Y\\N >> ");
            string userInput = ReadLine();
            while ( !yes.Contains(userInput) && !no.Contains(userInput) )
            {
                Write("Invalid input, try again >> ");
                userInput = ReadLine();
            }
            return userInput == "Y" ? false : true;
        }

        public static bool selectedPieceColour()
        {
            Write("Select your piece colour:" +
                "\n1. Black\t2. White" +
                "\n>> ");
            bool isColour = int.TryParse(ReadLine(), out int colour);
            while (!isColour)
            {
                WriteLine("Invalid input, try again >> ");
                isColour = int.TryParse(ReadLine(), out colour);
            }
            return colour == 1 ? true : false;
        }
    }
}
