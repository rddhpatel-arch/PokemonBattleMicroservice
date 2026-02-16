using PokemonBattle.Api.Enums;

namespace PokemonBattle.Api.Models
{
    public class PlayResult
    {
        public PokemonType Player { get; set; }
        public PokemonType Opponent { get; set; }
        public string Outcome { get; set; } = "";
    }
}
