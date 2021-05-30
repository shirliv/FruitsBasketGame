using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitsBasketGame.Players
{
    class ThoroughPlayer : BasePlayer, IPlayer
    {
        private int _nextGuess;

        public ThoroughPlayer(GameManager gameManager, string name, int minValue, int maxValue) : 
            base(gameManager, name, minValue, maxValue)
        {
            _nextGuess = minValue;
        }
        public override int MakeGuess()
        {
            var currentGuess = _nextGuess;
            _nextGuess++;
            return currentGuess;
        }
    }
}
