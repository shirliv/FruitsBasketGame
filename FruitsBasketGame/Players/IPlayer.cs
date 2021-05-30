using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitsBasketGame.Players
{
    interface IPlayer
    {
        int MakeGuess();
        void StartToPlay();
        string GetName();
    }
}
