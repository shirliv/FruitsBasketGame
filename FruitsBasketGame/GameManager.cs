using FruitsBasketGame.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitsBasketGame
{
    class GameManager
    {
        private GuessChecker _guessChecker;
        private HashSet<int> _allGuesses;
        private int _guessesCount;
        private int _goalValue;
        public event EventHandler<GameInfoEventArgs> GameFinished;
        private int _maxTotalGuesses;
        private bool _isGameOn;
        //Closest guess so far- the player and the relevant guess
        private Tuple<IPlayer, int> _closestGuess;
        public const int GAME_OVER_ERROR = -1;
        private object _isGameOnLock = new object();

        public GameManager(int goalValue, int maxTotalGuesses, HashSet<int> allGuesses)
        {
            _goalValue = goalValue;
            _guessChecker = new GuessChecker(goalValue);
            _allGuesses = allGuesses;
            _guessesCount = 0;
            _maxTotalGuesses = maxTotalGuesses;
            _isGameOn = true;
        }

        //Returns delta between guess to real value
        public int ReceiveGuess(IPlayer player, int guess)
        {
            lock (_isGameOnLock)
            {
                if (!_isGameOn)
                {
                    return GAME_OVER_ERROR;
                }
            }
            _guessesCount++;
            int guessDeltaToGoal = _guessChecker.GetDeltaGuessToGoal(guess);
            if (guessDeltaToGoal == 0)
            {
                FinishGameWithWinner(player, guess);
                return guessDeltaToGoal;
            }
            if (!_allGuesses.Contains(guess))
            {
                _allGuesses.Add(guess);
                if (_closestGuess == null ||
                    _guessChecker.GetDeltaGuessToGoal(_closestGuess.Item2) > guessDeltaToGoal)
                {
                    _closestGuess = new Tuple<IPlayer, int>(player, guess);
                }
            }
            if (_guessesCount == _maxTotalGuesses)
            {
                FinishGameWhenTotalGuessesAreUp();
                return guessDeltaToGoal;
            }
            return guessDeltaToGoal;
        }

        private bool IsGuessCorrect(int guess)
        {
            return _guessChecker.IsGuessCorrect(guess);
        }

        protected virtual void OnGameFinished(GameInfoEventArgs e)
        {
            GameFinished?.Invoke(this, e);
        }

        private void FinishGameWithWinner(IPlayer player, int guess)
        {
            lock (_isGameOnLock)
            {
                _isGameOn = false;
            }
            GameInfoEventArgs gameInfo = new GameInfoEventArgs
            {
                WasGameWon = true,
                BestPlayerName = player.GetName(),
                BestPlayerGuess = guess,
                TotalNumberOfAttempts = _guessesCount
            };
            OnGameFinished(gameInfo);
        }

        private void FinishGameWhenTotalGuessesAreUp()
        {
            lock (_isGameOnLock)
            {
                _isGameOn = false;
            }
            GameInfoEventArgs gameInfo = new GameInfoEventArgs
            {
                WasGameWon = false,
                BestPlayerName = _closestGuess.Item1.GetName(),
                BestPlayerGuess = _closestGuess.Item2,
                TotalNumberOfAttempts = _guessesCount
            };
            OnGameFinished(gameInfo);
        }
    }
}
