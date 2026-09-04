using IPO.Application.Interfaces.Services;
using IPO.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IPO.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _service;

        public SubscriptionController(ISubscriptionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var subscriptions = await _service.GetAllAsync();

            return Ok(subscriptions);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var subscription = await _service.GetByIdAsync(id);

            if (subscription == null)
                return NotFound(new
                {
                    message = "Subscription not found"
                });

            return Ok(subscription);
        }

        [HttpGet("ipo/{ipoId:int}")]
        public async Task<IActionResult> GetByIpoId(int ipoId)
        {
            var subscriptions = await _service.GetByIpoIdAsync(ipoId);

            return Ok(subscriptions);
        }

        [HttpGet("ipo/{ipoId:int}/date")]
        public async Task<IActionResult> GetByIpoAndDate(
            int ipoId,
            [FromQuery] DateTime date)
        {
            var subscriptions =
                await _service.GetByIpoAndDateAsync(ipoId, date);

            return Ok(subscriptions);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] Subscription subscription)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.AddAsync(subscription);

            return Ok(new
            {
                message = "Subscription created successfully",
                data = subscription
            });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] Subscription subscription)
        {
            if (id != subscription.Id)
            {
                return BadRequest(new
                {
                    message = "ID mismatch"
                });
            }

            var existing = await _service.GetByIdAsync(id);

            if (existing == null)
            {
                return NotFound(new
                {
                    message = "Subscription not found"
                });
            }

            await _service.UpdateAsync(subscription);

            return Ok(new
            {
                message = "Subscription updated successfully",
                data = subscription
            });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _service.GetByIdAsync(id);

            if (existing == null)
            {
                return NotFound(new
                {
                    message = "Subscription not found"
                });
            }

            await _service.DeleteAsync(id);

            return Ok(new
            {
                message = "Subscription deleted successfully"
            });
        }
    }
}
