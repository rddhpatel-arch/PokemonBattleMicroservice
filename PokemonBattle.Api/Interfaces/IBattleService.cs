using PokemonBattle.Api.Enums;
using PokemonBattle.Api.Models;

namespace PokemonBattle.Api.Interfaces
{
    public interface IBattleService
    {
        PlayResult Play(PokemonType playerChoice);
        Dictionary<string, int> GetStats();
    }
}
