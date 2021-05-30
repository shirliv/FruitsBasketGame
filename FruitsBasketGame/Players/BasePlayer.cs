using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FruitsBasketGame.Players
{
    abstract class BasePlayer : IPlayer
    {
        protected string _name { get; set; }
        protected int _minValue {get; set;}
        protected int _maxValue { get; set; }
        protected GameManager _gameManager { get; set; }
        protected bool _shouldPlay { get; set; }

        public BasePlayer(GameManager gameManager, string name, int minValue, int maxValue)
        {
            _name = name;
            _minValue = minValue;
            _maxValue = maxValue;
            _gameManager = gameManager;
            _shouldPlay = true;
        }

        public string GetName()
        {
            return _name;
        }

        public void StartToPlay()
        {
            _gameManager.GameFinished += gm_GameFinished;
            Thread playerThread = new Thread(Play);
            playerThread.Start();
        }

        public abstract int MakeGuess();

        public void Play()
        {
            while (_shouldPlay)
            {
                int guess = MakeGuess();
                int guessResult = _gameManager.ReceiveGuess(this, guess);
                if (guessResult > 0)
                {
                    Thread.Sleep(guessResult);
                } 
            }
        }

        public void gm_GameFinished(object sender, GameInfoEventArgs gameInfoEventArgs)
        {
            _shouldPlay = false;
        }
    }
}
