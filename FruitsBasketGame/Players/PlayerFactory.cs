using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitsBasketGame.Players
{
    class PlayerFactory
    {
        private int _minGuessValue;
        private int _maxGuessValue;
        private GameManager _gameManager;
        private HashSet<int> _cheatSheet;

        public PlayerFactory(GameManager gameManager, int minValue, int maxValue, HashSet<int> cheatSheet)
        {
            _minGuessValue = minValue;
            _maxGuessValue = maxValue;
            _gameManager = gameManager;
            _cheatSheet = cheatSheet;
        }

        public IPlayer CreatePlayer(string playerName, PlayerType playerType)
        {
            switch (playerType)
            {
                case PlayerType.Random:
                    return new RandomPlayer(_gameManager, playerName, _minGuessValue, _maxGuessValue);
                case PlayerType.Memory:
                    return new MemoryPlayer(_gameManager, playerName, _minGuessValue, _maxGuessValue);
                case PlayerType.Cheater:
                    return new CheaterPlayer(_gameManager, playerName, _minGuessValue, _maxGuessValue, _cheatSheet);
                case PlayerType.Thorough:
                    return new ThoroughPlayer(_gameManager, playerName, _minGuessValue, _maxGuessValue);
                case PlayerType.ThoroughCheater:
                    return new ThoroughCheaterPlayer(_gameManager, playerName, _minGuessValue, _maxGuessValue, _cheatSheet);
                default:
                    return null;
            }
        }
    }
}
