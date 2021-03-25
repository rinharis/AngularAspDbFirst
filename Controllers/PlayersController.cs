using AngularAspDBFirst.Models;
using AngularAspDBFirst.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularAspDBFirst.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayersService _playersService;

        public PlayersController(IPlayersService playersService)
        {
            _playersService = playersService;
        }

        // GET: api/Players
        [HttpGet]
        public async Task<IEnumerable<Player>> Get()
        {
            return await _playersService.GetPlayersList();
        }

        // GET: api/Players/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> Get(int id)
        {
            var player = await _playersService.GetPlayerById(id);
            if (player == null)
                return NotFound();
            return Ok(player);
        }

        //POST: api/Players
        [HttpPost]
        public async Task<ActionResult<Player>> Post(Player player)
        {
            await _playersService.CreatePlayer(player);
            //TODO: autoincrement
            return CreatedAtAction("Post", new { id = player.Id }, player);
        }

        //PUT: api/Players/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Player player)
        {
            if (id != player.Id)
                return BadRequest("Not a valid player id");
            await _playersService.UpdatePlayer(player);
            return NoContent();
        }

        // DELETE: api/Players/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid player id");
            var player = await _playersService.GetPlayerById(id);
            if (player == null)
                return NotFound();
            await _playersService.DeletePlayer(player);
            return NoContent();
        }
    }
}
