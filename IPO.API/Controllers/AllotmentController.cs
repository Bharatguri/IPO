using IPO.Application.Interfaces.Services;
using IPO.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IPO.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AllotmentController : ControllerBase
    {
        private readonly IAllotmentService _allotmentService;

        public AllotmentController(IAllotmentService allotmentService)
        {
            _allotmentService = allotmentService;
        }

        // GET: api/Allotment
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allotments = await _allotmentService.GetAllAsync();

            return Ok(allotments);
        }

        // GET: api/Allotment/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var allotment = await _allotmentService.GetByIdAsync(id);

            if (allotment == null)
            {
                return NotFound(new
                {
                    message = "Allotment not found"
                });
            }

            return Ok(allotment);
        }

        // POST: api/Allotment
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Allotment allotment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _allotmentService.AddAsync(allotment);

            return Ok(new
            {
                message = "Allotment created successfully",
                data = allotment
            });
        }

        // PUT: api/Allotment/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] Allotment allotment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingAllotment = await _allotmentService.GetByIdAsync(id);

            if (existingAllotment == null)
            {
                return NotFound(new
                {
                    message = "Allotment not found"
                });
            }

            allotment.Id = id;

            await _allotmentService.UpdateAsync(allotment);

            return Ok(new
            {
                message = "Allotment updated successfully",
                data = allotment
            });
        }

        // DELETE: api/Allotment/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingAllotment = await _allotmentService.GetByIdAsync(id);

            if (existingAllotment == null)
            {
                return NotFound(new
                {
                    message = "Allotment not found"
                });
            }

            await _allotmentService.DeleteAsync(id);

            return Ok(new
            {
                message = "Allotment deleted successfully"
            });
        }
    }
}
