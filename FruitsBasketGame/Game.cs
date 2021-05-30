using FruitsBasketGame.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitsBasketGame
{
    class Game
    {
        private List<IPlayer> _players;
        private int _fruitsBasketWeight;
        private GameManager _gameManager;
        private HashSet<int> _allGuesses;
        private const int MinWeightValue = 40;
        private const int MaxWeightValue = 140;
        private const int MaxNumOfTotalGuesses = 100;
        public Game()
        {
            _players = new List<IPlayer>();
            _allGuesses = new HashSet<int>();
        }

        public void InitializeGame()
        {
            //Generate a random value for the fruits basket weight:
            var rand = new Random();
            _fruitsBasketWeight = rand.Next(MinWeightValue, MaxWeightValue);

            //Initialize GameManager:
            _gameManager = new GameManager(_fruitsBasketWeight, MaxNumOfTotalGuesses, _allGuesses);

            //Initialize players:
            InitializePlayers();
        }

        private void InitializePlayers()
        {
            Console.WriteLine("Please enter the number of players: (between 2 to 8)");
            string input = Console.ReadLine();
            int numberOfPlayers;
            Int32.TryParse(input, out numberOfPlayers);
            while (numberOfPlayers < 2 || numberOfPlayers > 8)
            {
                Console.WriteLine("The number received was incorrect. Please try again");
                input = Console.ReadLine();
                Int32.TryParse(input, out numberOfPlayers);
            }

            PlayerFactory playerFactory = new PlayerFactory(_gameManager, MinWeightValue, MaxWeightValue, _allGuesses);
            for (int i = 1; i <= numberOfPlayers; i++)
            {
                Console.WriteLine("Please enter Player {0}'s name:", i);
                string playerName = Console.ReadLine();

                Console.WriteLine("Please enter Player {0}'s type indicated with a number. 1 - Random, 2 - Memory, " +
                    "3 - Thorough, 4 - Cheater, 5 - ThoroughCheater ", i);
                string typeInput = Console.ReadLine();
                int type;

                //If wrong value was given as input, ask for a correct one
                while (!Int32.TryParse(typeInput, out type) || type < 1 || type > 5)
                {
                    Console.WriteLine("The type received was incorrect. Please try again");
                    typeInput = Console.ReadLine();
                }

                IPlayer player = playerFactory.CreatePlayer(playerName, (PlayerType)type);
                _players.Add(player);
            }
        }

        public void StartGame()
        {
            _gameManager.GameFinished += gm_GameFinished;
            foreach (var player in _players)
            {
                player.StartToPlay();
            }
        }

        // event handler
        public void gm_GameFinished(object sender, GameInfoEventArgs gameInfoEventArgs)
        {
            Console.WriteLine("Game was finished");
            Console.WriteLine("The real weight of the basket is {0}", _fruitsBasketWeight);
            if (gameInfoEventArgs.WasGameWon)
            {
                Console.WriteLine("Game was won by {0}.", gameInfoEventArgs.BestPlayerName);
                Console.WriteLine("Number of guesses made by all players: {0}.", gameInfoEventArgs.TotalNumberOfAttempts);
            }
            else
            {
                Console.WriteLine("The player who guessed the closest weight to the real one was {0}.", gameInfoEventArgs.BestPlayerName);
                Console.WriteLine("The weight he guessed was {0}.", gameInfoEventArgs.BestPlayerGuess);
            }
        }
    }
}
