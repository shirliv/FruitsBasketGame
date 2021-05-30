using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitsBasketGame.Players
{
    class MemoryPlayer : BasePlayer, IPlayer
    {
        private HashSet<int> _previousGuesses;
        public MemoryPlayer(GameManager gameManager, string name, int minValue, int maxValue) : 
            base(gameManager, name, minValue, maxValue)
        {
            _previousGuesses = new HashSet<int>();
        }

        public override int MakeGuess()
        {
            int guessedValue;
            var rand = new Random();
            do
            {
                guessedValue = rand.Next(_minValue, _maxValue);
            }
            while (_previousGuesses.Contains(guessedValue));
            _previousGuesses.Add(guessedValue);
            return guessedValue;
        }
    }
}
