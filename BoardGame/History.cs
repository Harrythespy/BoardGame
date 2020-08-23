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
        }

        public void undoStep(Point coordinate)
        {
            // undo the current step
            gameHistory.Remove(coordinate);
        }

        public void saveHistory()
        {
            // Export current board states
            FileStream outFile = new FileStream(FILENAME, FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(outFile);
            foreach(Point coor in gameHistory)
            {
                System.Console.WriteLine(coor);
                writer.WriteLine(coor);
            }
            writer.Close();
            outFile.Close();
        }

        public void loadHistory()
        {
            // Load saved board states from directory
        }
    }
}
