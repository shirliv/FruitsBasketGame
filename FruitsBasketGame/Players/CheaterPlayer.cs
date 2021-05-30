using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitsBasketGame.Players
{
    class CheaterPlayer : BasePlayer, IPlayer
    {
        private readonly HashSet<int> _allGuessesMade;
        public CheaterPlayer(GameManager gamemManager, string name, int minValue, int maxValue, HashSet<int> allGuessesMade) :
            base(gamemManager, name, minValue, maxValue)
        {
            _allGuessesMade = allGuessesMade;
        }

        public override int MakeGuess()
        {
            int guessedValue;
            var rand = new Random();
            do
            {
                guessedValue = rand.Next(_minValue, _maxValue);
            }
            while (_allGuessesMade.Contains(guessedValue));
            return guessedValue;
        }
    }
}
