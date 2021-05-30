using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitsBasketGame.Players
{
    class ThoroughCheaterPlayer : BasePlayer, IPlayer
    {
        private readonly HashSet<int> _allGuessesMade;
        private int _nextGuess;
        public ThoroughCheaterPlayer(GameManager gameManager, string name, int minValue, int maxValue, HashSet<int> allGuessesMade) : 
            base(gameManager, name, minValue, maxValue)
        {
            _allGuessesMade = allGuessesMade;
            _nextGuess = minValue;
        }

        public override int MakeGuess()
        {
            while (_allGuessesMade.Contains(_nextGuess))
            {
                _nextGuess++;
            }
            var currentGuess = _nextGuess;
            _nextGuess++; 
            return currentGuess;
        }
    }
}
