using IPO.Application.Interfaces.Services;
using IPO.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IPO.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(
            INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        // GET: api/Notification
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var notifications = await _notificationService.GetAllAsync();

            return Ok(notifications);
        }

        // GET: api/Notification/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var notification = await _notificationService.GetByIdAsync(id);

            if (notification is null)
            {
                return NotFound(new
                {
                    message = "Notification not found"
                });
            }

            return Ok(notification);
        }

        // POST: api/Notification
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] Notification notification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _notificationService.AddAsync(notification);

            return Ok(new
            {
                message = "Notification created successfully",
                data = notification
            });
        }

        // PUT: api/Notification/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] Notification notification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingNotification =
                await _notificationService.GetByIdAsync(id);

            if (existingNotification is null)
            {
                return NotFound(new
                {
                    message = "Notification not found"
                });
            }

            notification.Id = id;

            await _notificationService.UpdateAsync(notification);

            return Ok(new
            {
                message = "Notification updated successfully",
                data = notification
            });
        }

        // DELETE: api/Notification/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingNotification =
                await _notificationService.GetByIdAsync(id);

            if (existingNotification is null)
            {
                return NotFound(new
                {
                    message = "Notification not found"
                });
            }

            await _notificationService.DeleteAsync(id);

            return Ok(new
            {
                message = "Notification deleted successfully"
            });
        }
    }
}
