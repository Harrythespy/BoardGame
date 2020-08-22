using System;
using static System.Console;
using System.Drawing;

namespace BoardGame
{
    public class Human : Player
    {
        string _InputCoordinate;
        Point _Coordinate;

        public void EnterCoordinate()
        {
            _InputCoordinate = ReadLine();
            string[] axises = _InputCoordinate.Split(' ');
            bool checkXAxis = int.TryParse(axises[0], out int x);
            bool checkYAxis = int.TryParse(axises[1], out int y);
            while(!checkXAxis || !checkYAxis)
            {
                Write("input coordinate is not valid, please retry >>");
                _InputCoordinate = ReadLine();
                axises = _InputCoordinate.Split(' ');
                checkXAxis = int.TryParse(axises[0], out x);
                checkYAxis = int.TryParse(axises[1], out y);
            }

            _Coordinate = new Point(x, y);
            WriteLine($"input coordinate: {_Coordinate}");
        }
    }
}
