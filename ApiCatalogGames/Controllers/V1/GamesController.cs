using ApiCatalogGames.Exceptions;
using ApiCatalogGames.InputModel;
using ApiCatalogGames.Services;
using ApiCatalogGames.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogGames.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        /// <summary>
        /// Search all games with paged list
        /// </summary>
        /// <remarks>
        /// It's not possible list without paged
        /// </remarks>
        /// <param name="page">Indicate the consulted page. Minimum value is 1</param>
        /// <param name="quantity">Indicate the qunatity registries per page. Minimum value is 1 and maximum value is 50</param>
        /// <response code="200">200 is the code returned along with list of games</response>
        /// <response code="204">204 is the code returned when not existing list of games</response>   
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameViewModel>>> Get([FromQuery, Range(1, int.MaxValue)] int page = 1, [FromQuery, Range(1, 50)] int quantity = 5)
        {
            var games = await _gameService.Get(page, page);

            if (games.Count() == 0)
                return NoContent();

            return Ok(games);
        }

        /// <summary>
        /// Search game with Id Number
        /// </summary>
        /// <param name="idgame">Id Number of the game search</param>
        /// <response code="200">Returned the found game</response>
        /// <response code="204">Returned case id number not existing</response>   
        [HttpGet("{idgame:guid}")]
        public async Task<ActionResult<GameViewModel>> Get([FromRoute] Guid idgame)
        {
            var game = await _gameService.Get(idgame);

            if (game == null)
                return NoContent();

            return Ok(game);
        }

        /// <summary>
        /// Insert the game into catalog
        /// </summary>
        /// <param name="gameInputModel">Game data to be entered</param>
        /// <response code="200">Case the game to be inserted with success</response>
        /// <response code="422">Case existing a game with the same name and producer</response>   
        [HttpPost]
        public async Task<ActionResult<GameViewModel>> Insertgame([FromBody] GameInputModel gameInputModel)
        {
            try
            {
                var game = await _gameService.Insert(gameInputModel);

                return Ok(game);
            }
            catch (ExistingGameException ex)
            {
                return UnprocessableEntity("Existing game to this producer with the same name.");
            }
        }

        /// <summary>
        /// Update a game into catalog
        /// </summary>
        /// /// <param name="idgame">Id Number of the game to be updated</param>
        /// <param name="gameInputModel">New data to update the game</param>
        /// <response code="200">Case the update occur with success</response>
        /// <response code="404">Case not existing a game with de id number</response>   
        [HttpPut("{idgame:guid}")]
        public async Task<ActionResult> Updategame([FromRoute] Guid idgame, [FromBody] GameInputModel gameInputModel)
        {
            try
            {
                await _gameService.Update(idgame, gameInputModel);

                return Ok();
            }
            catch (NotExistingGame ex)
            {
                return NotFound("Not existing this game");
            }
        }

        /// <summary>
        /// Update the price of the game
        /// </summary>
        /// /// <param name="idgame">Id number of the game updated</param>
        /// <param name="price">The new price of the game</param>
        /// <response code="200">Case the update occur with success</response>
        /// <response code="404">Case not existing a game with de id number</response>   
        [HttpPatch("{idgame:guid}/Price/{price:double}")]
        public async Task<ActionResult> Updategame([FromRoute] Guid idgame, [FromRoute] double price)
        {
            try
            {
                await _gameService.Update(idgame, price);

                return Ok();
            }
            catch (NotExistingGame ex)
            {
                return NotFound("Not existing this game");
            }
        }

        /// <summary>
        /// Delete a game
        /// </summary>
        /// /// <param name="idgame">Id number of the game deleted</param>
        /// <response code="200">Case the delete occur with success</response>
        /// <response code="404">Case not existing a game with de id number</response>   
        [HttpDelete("{idgame:guid}")]
        public async Task<ActionResult> Deletegame([FromRoute] Guid idgame)
        {
            try
            {
                await _gameService.Delete(idgame);

                return Ok();
            }
            catch (NotExistingGame ex)
            {
                return NotFound("Não existe este game");
            }
        }

    }
}
