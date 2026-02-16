using PokemonBattle.Api.Enums;
using PokemonBattle.Api.Infrastructure.Interfaces;
using PokemonBattle.Api.Interfaces;
using PokemonBattle.Api.Models;

namespace PokemonBattle.Api.Services
{
    public class BattleService : IBattleService
    {
        private readonly IStatsRepository _statsRepo;
        private readonly Random _random = new();

        public BattleService(IStatsRepository statsRepo)
        {
            _statsRepo = statsRepo;
        }

        public PlayResult Play(PokemonType playerChoice)
        {
            //Get random Enum value to choose an opponent
            var opponent = GetRandomType();

            string outcome = DetermineOutcome(playerChoice, opponent);

            //Call repo
            _statsRepo.Record(outcome);

            return new PlayResult
            {
                Player = playerChoice,
                Opponent = opponent,
                Outcome = outcome
            };
        }

        public Dictionary<string, int> GetStats()
        {
            return _statsRepo.GetStats();
        }

        #region Private Functions

        private string DetermineOutcome(PokemonType player, PokemonType opponent)
        {
            if (player == opponent)
                return "draw";

            if ((player == PokemonType.Fire && opponent == PokemonType.Grass) ||
                (player == PokemonType.Grass && opponent == PokemonType.Water) ||
                (player == PokemonType.Water && opponent == PokemonType.Fire))
            {
                return "win";
            }

            return "loss";


        }
        private PokemonType GetRandomType()
        {
            var values = Enum.GetValues(typeof(PokemonType));
            return (PokemonType)values.GetValue(_random.Next(values.Length))!;
        }

        #endregion
    }
}


