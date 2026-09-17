using IPO.Application.DTOs;
using IPO.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace IPO.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IPOController : ControllerBase
    {
        private readonly IIPOService _ipoService;

        public IPOController(IIPOService ipoService)
        {
            _ipoService = ipoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _ipoService.GetAllAsync();

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _ipoService.GetByIdAsync(id);

            if (result is null)
                return NotFound(new
                {
                    message = "IPO not found."
                });

            return Ok(result);
        }

        [HttpGet("upcoming")]
        public async Task<IActionResult> GetUpcoming()
        {
            var result = await _ipoService.GetUpcomingAsync();

            return Ok(result);
        }

        [HttpGet("open")]
        public async Task<IActionResult> GetOpen()
        {
            var result = await _ipoService.GetOpenAsync();

            return Ok(result);
        }

        [HttpGet("closed")]
        public async Task<IActionResult> GetClosed()
        {
            var result = await _ipoService.GetClosedAsync();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] IPOListDto request)
        {
            var result = await _ipoService.CreateAsync(request);

            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Id },
                result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] IPOListDto request)
        {
            var updated = await _ipoService.UpdateAsync(id, request);

            if (!updated)
                return NotFound(new
                {
                    message = "IPO not found."
                });

            return NoContent();
        }

        // DELETE: api/IPO/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _ipoService.DeleteAsync(id);

            if (!deleted)
                return NotFound(new
                {
                    message = "IPO not found."
                });

            return NoContent();
        }
    }
}
