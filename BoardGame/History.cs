using System;
using System.Drawing;
using System.Collections.Generic;
using System.IO;

namespace BoardGame
{
    public class History
    {
        Dictionary<string, Point> gameHistory = new Dictionary<string, Point>();

        public History(){}

        public void recordStep(string piece, Point coordinate)
        {
            // Save current move to history
            gameHistory.Add(piece, coordinate);
        }

        public void removeStep()
        {
            // undo the current step
        }

        public void saveHistory()
        {
            // Export current board states
        }

        public void loadHistory()
        {
            // Load saved board states from directory
        }
    }
}
