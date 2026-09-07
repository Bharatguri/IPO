using IPO.Application.Interfaces.Services;
using IPO.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IPO.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class IPODocumentController : ControllerBase
    {
        private readonly IIPODocumentService _ipoDocumentService;

        public IPODocumentController(
            IIPODocumentService ipoDocumentService)
        {
            _ipoDocumentService = ipoDocumentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var documents = await _ipoDocumentService.GetAllAsync();

            return Ok(documents);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var document = await _ipoDocumentService.GetByIdAsync(id);

            if (document is null)
            {
                return NotFound(new
                {
                    message = "IPO document not found"
                });
            }

            return Ok(document);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] IPODocument document)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _ipoDocumentService.AddAsync(document);

            return Ok(new
            {
                message = "IPO document created successfully",
                data = document
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] IPODocument document)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingDocument =
                await _ipoDocumentService.GetByIdAsync(id);

            if (existingDocument is null)
            {
                return NotFound(new
                {
                    message = "IPO document not found"
                });
            }

            document.Id = id;

            await _ipoDocumentService.UpdateAsync(document);

            return Ok(new
            {
                message = "IPO document updated successfully",
                data = document
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingDocument =
                await _ipoDocumentService.GetByIdAsync(id);

            if (existingDocument is null)
            {
                return NotFound(new
                {
                    message = "IPO document not found"
                });
            }

            await _ipoDocumentService.DeleteAsync(id);

            return Ok(new
            {
                message = "IPO document deleted successfully"
            });
        }
    }
}
