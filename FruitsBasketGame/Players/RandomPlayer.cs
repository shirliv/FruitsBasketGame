using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitsBasketGame.Players
{
    class RandomPlayer : BasePlayer, IPlayer
    {
        public RandomPlayer(GameManager gameManager, string name, int minValue, int maxValue) : 
            base(gameManager, name, minValue, maxValue)
        {

        }

        public override int MakeGuess()
        {
            var rand = new Random();
            return rand.Next(_minValue, _maxValue);
        }
    }
}
