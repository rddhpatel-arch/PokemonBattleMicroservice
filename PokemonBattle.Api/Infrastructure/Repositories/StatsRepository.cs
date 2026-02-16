using PokemonBattle.Api.Infrastructure.Interfaces;

namespace PokemonBattle.Api.Infrastructure.Repositories
{
    public class StatsRepository : IStatsRepository
    {
        private readonly Dictionary<string, int> stats = new()
        {
            { "win", 0 },
            { "loss", 0 },
            { "draw", 0 }
        };

    public void Record(string outcome)
    {
        if (stats.ContainsKey(outcome))
            stats[outcome]++;
    }

    public Dictionary<string, int> GetStats()
    {
        return stats;
    }
}

}
