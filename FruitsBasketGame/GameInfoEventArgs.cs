using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitsBasketGame
{
    class GameInfoEventArgs : EventArgs
    {
        public bool WasGameWon { get; set; }
        public int TotalNumberOfAttempts { get; set; }
        public string BestPlayerName { get; set; }
        public int BestPlayerGuess { get; set; }
    }
}
