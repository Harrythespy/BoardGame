using System;
using System.Drawing;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

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

        public void saveHistory(bool piece, bool competitor, int difficulty)
        {
            try
            {
                // Export current board states
                FileStream outFile = new FileStream(FILENAME, FileMode.Create, FileAccess.Write);
                StreamWriter writer = new StreamWriter(outFile);

                Console.WriteLine("Start saving...");

                writer.WriteLine(piece); //true = black
                writer.WriteLine(competitor); //true = human

                Console.WriteLine("Work in progress...");
                
                if (!competitor) writer.WriteLine(difficulty);
                else writer.WriteLine();

                Console.WriteLine("attributes saved.");

                foreach (Point coor in gameHistory)
                {
                    writer.WriteLine(coor);
                }

                Console.WriteLine("History saved.");
                writer.Close();
                outFile.Close();

                Console.WriteLine("Game history saved successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred while saving game history..");
                Console.WriteLine(e.Message);
            }
        }

        public bool[] loadHistory(string FILEPATH)
        {
            // Load saved board states from directory
            try
            {
                FileStream inFile = new FileStream(FILEPATH, FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(inFile);

                string record = reader.ReadToEnd();
                string[] lines;
                bool[] attributes = new bool[3];

                if (record != null)
                {
                    lines = record.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

                    for (int i = 3; i < lines.Length; i++)
                    {
                        string[] coors = new Regex(@"\D+").Split(lines[i]);
                        if (coors[0] != null && coors[1] != null)
                        {
                            int x = Convert.ToInt32(coors[1]);
                            int y = Convert.ToInt32(coors[2]);
                            gameHistory.Add(new Point(x, y));
                        }
                    }

                    if (lines[0] == "False") attributes[0] = false;
                    else attributes[0] = true;

                    if (lines[1] == "False") attributes[1] = false;
                    else attributes[1] = true;

                    if (lines[2] == null) attributes[2] = false;
                    else attributes[2] = true;
                }
                reader.Close();
                // return piece and history
                return attributes;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred while processing the file.");
                Console.WriteLine(e.Message);
            }

            return null;

        }
    }
}
