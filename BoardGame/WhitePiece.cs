using System;
namespace BoardGame
{
    public class WhitePiece : Piece
    {
        private string _WhitePiece = "O";

        public WhitePiece()
        {
        }

        public string printPiece()
        {
            return this._WhitePiece;
        }
    }
}
