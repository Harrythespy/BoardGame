﻿using System;
using static System.Console;
using System.Drawing;

namespace BoardGame
{
    public class Human : Player
    {
        const string HELP = "HELP";

        public new Piece Piece { get; set; }
        
        public Human() { }

        public Human(Piece piece) : base(piece)
        {
            Piece = piece;
        }

        public override Point Move(string[,] boardState)
        {
            char[] DELIM = { ' ', ',' };
            Write("Enter a coordinate: row, col " +
                "\nHelp: HELP" +
                "\n>> ");

            string _InputCoordinate = ReadLine();

            while (_InputCoordinate == "")
            {
                Write("Coordinate cannot be empty." +
                    "\nTry again >> ");
                _InputCoordinate = ReadLine();
            }

            if (_InputCoordinate == HELP)
            {
                return new Point(-1, -1);
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

            Point _Coordinate = new Point(x - 1, y - 1);

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
            }
            return _Coordinate;
        }

        protected bool checkValidity(Point coordinate, string[,] boardState)
        {
            if (boardState[coordinate.X, coordinate.Y] == " ") return true;
            else return false;
        }
    }
}
