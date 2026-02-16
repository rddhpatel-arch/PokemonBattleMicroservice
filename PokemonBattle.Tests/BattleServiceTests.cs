using Moq;
using PokemonBattle.Api.Enums;
using PokemonBattle.Api.Infrastructure.Interfaces;
using PokemonBattle.Api.Services;
using Xunit;

namespace PokemonBattle.Tests.Services
{
    public class BattleServiceTests
    {
        private readonly Mock<IStatsRepository> _statsRepoMock;
        private readonly BattleService _service;

        public BattleServiceTests()
        {
            _statsRepoMock = new Mock<IStatsRepository>();
            _service = new BattleService(_statsRepoMock.Object);
        }

        // -----------------------------
        // DetermineOutcome Tests
        // -----------------------------
        [Theory]
        [InlineData(PokemonType.Fire, PokemonType.Grass, "win")]
        [InlineData(PokemonType.Grass, PokemonType.Water, "win")]
        [InlineData(PokemonType.Water, PokemonType.Fire, "win")]
        [InlineData(PokemonType.Fire, PokemonType.Water, "loss")]
        [InlineData(PokemonType.Grass, PokemonType.Fire, "loss")]
        [InlineData(PokemonType.Water, PokemonType.Grass, "loss")]
        [InlineData(PokemonType.Fire, PokemonType.Fire, "draw")]
        [InlineData(PokemonType.Water, PokemonType.Water, "draw")]
        [InlineData(PokemonType.Grass, PokemonType.Grass, "draw")]
        public void DetermineOutcome_ReturnsExpectedResult(
            PokemonType player,
            PokemonType opponent,
            string expected)
        {
            // Use reflection to call private method
            var method = typeof(BattleService)
                .GetMethod("DetermineOutcome",
                    System.Reflection.BindingFlags.NonPublic |
                    System.Reflection.BindingFlags.Instance);

            var result = method.Invoke(_service, new object[] { player, opponent });

            Assert.Equal(expected, result);
        }

        // -----------------------------
        // Play() Tests
        // -----------------------------
        [Fact]
        public void Play_ShouldReturnValidPlayResult_AndRecordOutcome()
        {
            // Arrange
            var playerChoice = PokemonType.Fire;

            // Act
            var result = _service.Play(playerChoice);

            // Assert: result object is valid
            Assert.NotNull(result);
            Assert.Equal(playerChoice, result.Player);

            // Opponent is random but must be valid enum
            Assert.True(Enum.IsDefined(typeof(PokemonType), result.Opponent));

            // Outcome must be one of the three
            Assert.Contains(result.Outcome, new[] { "win", "loss", "draw" });

            // Verify repository interaction
            _statsRepoMock.Verify(r => r.Record(It.IsAny<string>()), Times.Once);
        }

        // -----------------------------
        // GetStats() Tests
        // -----------------------------
        [Fact]
        public void GetStats_ShouldReturnStatsFromRepository()
        {
            // Arrange
            var expectedStats = new Dictionary<string, int>
            {
                { "win", 3 },
                { "loss", 1 },
                { "draw", 2 }
            };

            _statsRepoMock.Setup(r => r.GetStats()).Returns(expectedStats);

            // Act
            var stats = _service.GetStats();

            // Assert
            Assert.Equal(expectedStats, stats);
        }
    }
}