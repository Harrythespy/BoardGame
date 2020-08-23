using System;
using System.Drawing;
using System.Collections.Generic;
using System.IO;

namespace BoardGame
{
    public class History
    {
        private const string FILENAME = "BoardHistory.txt";
        public List<Point> gameHistory = new List<Point>();

        public History(){}

        public void recordStep(Point coordinate)
        {
            // Save current move to history
            gameHistory.Add(coordinate);
            foreach(Point coor in gameHistory)
            {
                System.Console.WriteLine($"X:{coor.X} Y: {coor.Y}");
            }
        }

        public void undoStep(Point coordinate)
        {
            // undo the current step
            gameHistory.Remove(coordinate);
        }

        public void saveHistory()
        {
            // Export current board states
            System.Console.WriteLine(gameHistory.ToArray());
        }

        public void loadHistory()
        {
            // Load saved board states from directory
        }
    }
}
