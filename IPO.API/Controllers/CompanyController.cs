using IPO.Application.DTOs;
using IPO.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace IPO.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }


        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CompanyListDto request)
        {
            var result = await _companyService.CreateAsync(request);

            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Id },
                result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _companyService.GetAllAsync();

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _companyService.GetByIdAsync(id);

            if (result is null)
                return NotFound(new
                {
                    message = "Company not found."
                });

            return Ok(result);
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] CompanyListDto request)
        {
            var result = await _companyService.UpdateAsync(id, request);

            if (!result)
                return NotFound(new
                {
                    message = "Company not found."
                });

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _companyService.DeleteAsync(id);

            if (!result)
                return NotFound(new
                {
                    message = "Company not found."
                });

            return NoContent();
        }
    }
}
