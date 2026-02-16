namespace PokemonBattle.Api.Infrastructure.Interfaces
{
    public interface IStatsRepository
    {
        void Record(string outcome);
        Dictionary<string, int> GetStats();
    }
}
