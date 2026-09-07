using IPO.Application.Interfaces.Services;
using IPO.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IPO.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerService _managerService;

        public ManagerController(IManagerService managerService)
        {
            _managerService = managerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var managers = await _managerService.GetAllAsync();

            return Ok(managers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var manager = await _managerService.GetByIdAsync(id);

            if (manager is null)
            {
                return NotFound(new
                {
                    message = "Manager not found"
                });
            }

            return Ok(manager);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] Manager manager)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _managerService.AddAsync(manager);

            return Ok(new
            {
                message = "Manager created successfully",
                data = manager
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] Manager manager)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingManager =
                await _managerService.GetByIdAsync(id);

            if (existingManager is null)
            {
                return NotFound(new
                {
                    message = "Manager not found"
                });
            }

            manager.Id = id;

            await _managerService.UpdateAsync(manager);

            return Ok(new
            {
                message = "Manager updated successfully",
                data = manager
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingManager =
                await _managerService.GetByIdAsync(id);

            if (existingManager is null)
            {
                return NotFound(new
                {
                    message = "Manager not found"
                });
            }

            await _managerService.DeleteAsync(id);

            return Ok(new
            {
                message = "Manager deleted successfully"
            });
        }
    }
}
