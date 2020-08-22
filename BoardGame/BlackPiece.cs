using System;
namespace BoardGame
{
    public class BlackPiece : Piece
    {
        private string _BlackPiece = "X";

        public BlackPiece()
        {
        }

        public string printPiece()
        {
            return this._BlackPiece;
        }
    }
}
