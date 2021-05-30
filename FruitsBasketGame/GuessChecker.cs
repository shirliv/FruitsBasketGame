using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitsBasketGame
{
    public class GuessChecker
    {
        private int _goalValue;

        public GuessChecker(int rightAnswer)
        {
            _goalValue = rightAnswer;
        }

        public bool IsGuessCorrect(int guess)
        {
            if (guess == _goalValue)
            {
                return true;
            }
            return false;
        }

        public int GetDeltaGuessToGoal(int guess)
        {
            return Math.Abs(guess - _goalValue);
        }
    }
}
