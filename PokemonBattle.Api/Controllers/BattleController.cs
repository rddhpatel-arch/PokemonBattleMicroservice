using Microsoft.AspNetCore.Mvc;
using PokemonBattle.Api.Enums;
using PokemonBattle.Api.Interfaces;
using PokemonBattle.Api.Models;


namespace PokemonBattle.Api.Controllers
{
    [ApiController]
    [Route("api/v1/battle")]
    public class BattleController : ControllerBase
    {
        private readonly IBattleService _battleService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="battleService"></param>
        public BattleController(IBattleService battleService)
        {
            _battleService = battleService;
        }

        /// <summary>
        /// Plays a Pokémon battle by comparing the player's selected Pokémon type
        /// against a randomly chosen opponent. The API returns 400 (Bad Request)
        /// for an invalid type, and 200 (OK) when the request is valid.
        /// </summary>
        /// <remarks>
        /// **Expected Request Body:**  
        /// {
        ///     "playerChoice": 0 | 1 | 2
        /// }
        ///
        /// Where:
        /// - **0 = Fire**
        /// - **1 = Water**
        /// - **2 = Grass**
        ///
        /// These numeric values map to the underlying PokémonType enum.
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("play")]
        public IActionResult Play([FromBody] PlayRequest request)
        {
            if (!Enum.IsDefined(typeof(PokemonType), request.PlayerChoice))
                return BadRequest("Invalid Pokémon type. Allowed values: Fire, Water, Grass.");


            var result = _battleService.Play(request.PlayerChoice);
            return Ok(result);
        }

        /// <summary>
        /// Returns a summary of all completed battles, including the total number of
        /// wins, losses, and draws accumulated since the service started.
        /// </summary>
        /// <returns>
        /// A 200 OK response containing a dictionary of battle outcome counts.
        /// </returns>

        [HttpGet("stats")]
        public IActionResult Stats()
        {
            return Ok(_battleService.GetStats());
        }
    }
}
