using System;
using static System.Console;

namespace BoardGame
{
    public class Help
    {
        public const string END = "END";

        public Help() {}

        public string displayCommand()
        {
            //Clear();
            Write("Commands that available in the program are listed below:" +
                $"\n{END} - Leave the game." +
                $"\n>> ");
            string userInput = ReadLine();
            while(userInput != END)
            {
                Write("Invalid command, try again. >> ");
                userInput = ReadLine();
            }
            return END;
        }
    }
}
