using IPO.Application.Interfaces.Services;
using IPO.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IPO.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyFinancialController : ControllerBase
    {
        private readonly ICompanyFinancialService _companyFinancialService;

        public CompanyFinancialController(
            ICompanyFinancialService companyFinancialService)
        {
            _companyFinancialService = companyFinancialService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var financials = await _companyFinancialService.GetAllAsync();

            return Ok(financials);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var financial = await _companyFinancialService.GetByIdAsync(id);

            if (financial is null)
            {
                return NotFound(new
                {
                    message = "Company financial record not found"
                });
            }

            return Ok(financial);
        }

        // GET: api/CompanyFinancial/upcoming
        //[HttpGet("upcoming")]
        //public async Task<IActionResult> GetUpcoming()
        //{
        //    var financials = await _companyFinancialService.GetUpcomingAsync();

        //    return Ok(financials);
        //}

        // GET: api/CompanyFinancial/open
        //[HttpGet("open")]
        //public async Task<IActionResult> GetOpen()
        //{
        //    var financials = await _companyFinancialService.GetOpenAsync();

        //    return Ok(financials);
        //}

        // GET: api/CompanyFinancial/closed
        //[HttpGet("closed")]
        //public async Task<IActionResult> GetClosed()
        //{
        //    var financials = await _companyFinancialService.GetClosedAsync();

        //    return Ok(financials);
        //}

        // POST: api/CompanyFinancial
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CompanyFinancial companyFinancial)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _companyFinancialService.AddAsync(companyFinancial);

            return Ok(new
            {
                message = "Company financial record created successfully",
                data = companyFinancial
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] CompanyFinancial companyFinancial)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingFinancial =
                await _companyFinancialService.GetByIdAsync(id);

            if (existingFinancial is null)
            {
                return NotFound(new
                {
                    message = "Company financial record not found"
                });
            }

            companyFinancial.Id = id;

            await _companyFinancialService.UpdateAsync(companyFinancial);

            return Ok(new
            {
                message = "Company financial record updated successfully",
                data = companyFinancial
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingFinancial =
                await _companyFinancialService.GetByIdAsync(id);

            if (existingFinancial is null)
            {
                return NotFound(new
                {
                    message = "Company financial record not found"
                });
            }

            await _companyFinancialService.DeleteAsync(id);

            return Ok(new
            {
                message = "Company financial record deleted successfully"
            });
        }
    }
}
