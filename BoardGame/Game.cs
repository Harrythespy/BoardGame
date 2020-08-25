using System;
using System.Collections.Generic;

namespace BoardGame
{
    public class Game : BoardGame
    {

        public List<Game> subGames = new List<Game>();
        public Game(Rule rule):base(rule) { }
        public Game(Rule rule, bool competitor, bool colour): base(rule, competitor, colour)
        {
        }

        
    }
}
