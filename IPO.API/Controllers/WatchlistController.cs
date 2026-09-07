using IPO.Application.Interfaces.Services;
using IPO.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IPO.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class WatchlistController : ControllerBase
    {
        private readonly IWatchlistService _watchlistService;

        public WatchlistController(IWatchlistService watchlistService)
        {
            _watchlistService = watchlistService;
        }

        // GET: api/Watchlist
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var watchlists = await _watchlistService.GetAllAsync();

            return Ok(watchlists);
        }

        // GET: api/Watchlist/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var watchlist = await _watchlistService.GetByIdAsync(id);

            if (watchlist is null)
            {
                return NotFound(new
                {
                    message = "Watchlist not found"
                });
            }

            return Ok(watchlist);
        }

        // POST: api/Watchlist
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] Watchlist watchlist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _watchlistService.AddAsync(watchlist);

            return Ok(new
            {
                message = "Watchlist created successfully",
                data = watchlist
            });
        }

        // PUT: api/Watchlist/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] Watchlist watchlist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingWatchlist =
                await _watchlistService.GetByIdAsync(id);

            if (existingWatchlist is null)
            {
                return NotFound(new
                {
                    message = "Watchlist not found"
                });
            }

            watchlist.Id = id;

            await _watchlistService.UpdateAsync(watchlist);

            return Ok(new
            {
                message = "Watchlist updated successfully",
                data = watchlist
            });
        }

        // DELETE: api/Watchlist/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingWatchlist =
                await _watchlistService.GetByIdAsync(id);

            if (existingWatchlist is null)
            {
                return NotFound(new
                {
                    message = "Watchlist not found"
                });
            }

            await _watchlistService.DeleteAsync(id);

            return Ok(new
            {
                message = "Watchlist deleted successfully"
            });
        }
    }
}
