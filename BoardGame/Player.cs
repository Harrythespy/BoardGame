using System;
using static System.Console;
using System.Drawing;

namespace BoardGame
{
    public abstract class Player
    {
        protected string _InputCoordinate;
        protected Point _Coordinate;
        public Piece Piece { get; set; }

        public Player() {}

        public Player(Piece piece)
        {
            Piece = piece;
        }

        public Point getCoordinate(string[,] boardState)
        {
            char[] DELIM = { ' ', ',' };
            Write("Enter a coordinate >> ");
            _InputCoordinate = ReadLine();

            while (_InputCoordinate == "")
            {
                Write("Coordinate cannot be empty." +
                    "\nTry again >> ");
                _InputCoordinate = ReadLine();
            }

            string[] axises = _InputCoordinate.Split(DELIM);
            while (axises.Length != 2)
            {
                Write("Please enter TWO valid position >> ");
                _InputCoordinate = ReadLine();
                axises = _InputCoordinate.Split(DELIM);
            }

            bool checkXAxis = int.TryParse(axises[0], out int x);
            bool checkYAxis = int.TryParse(axises[1], out int y);
            while (!checkXAxis || !checkYAxis || x > boardState.GetLength(0) || y > boardState.GetLength(1))
            {
                Write("input coordinate is not valid, please retry >>");
                _InputCoordinate = ReadLine();
                axises = _InputCoordinate.Split(DELIM);
                checkXAxis = int.TryParse(axises[0], out x);
                checkYAxis = int.TryParse(axises[1], out y);
            }

            _Coordinate = new Point(x - 1, y - 1);
            //WriteLine($"input coordinate: {_Coordinate}");

            while (!checkValidity(_Coordinate, boardState))
            {
                Write("coordinate is unavailable, please try again >> ");
                _InputCoordinate = ReadLine();

                while (_InputCoordinate == "")
                {
                    Write("Coordinate cannot be empty." +
                        "\nTry again >> ");
                    _InputCoordinate = ReadLine();
                }

                axises = _InputCoordinate.Split(DELIM);
                checkXAxis = int.TryParse(axises[0], out x);
                checkYAxis = int.TryParse(axises[1], out y);

                while (!checkXAxis || !checkYAxis || x > boardState.GetLength(0) || y > boardState.GetLength(1))
                {
                    Write("input coordinate is not valid, please retry >>");
                    _InputCoordinate = ReadLine();
                    axises = _InputCoordinate.Split(' ');
                    checkXAxis = int.TryParse(axises[0], out x);
                    checkYAxis = int.TryParse(axises[1], out y);
                }

                _Coordinate = new Point(x - 1, y - 1);
                //WriteLine($"input coordinate: {_Coordinate}");
            }

            return _Coordinate;
        }

        protected bool checkValidity(Point coordinate, string[,] boardState)
        {
            if (boardState[coordinate.X, coordinate.Y] == " ")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
