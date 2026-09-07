using IPO.Application.Interfaces.Services;
using IPO.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IPO.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrarController : ControllerBase
    {
        private readonly IRegistrarService _registrarService;

        public RegistrarController(IRegistrarService registrarService)
        {
            _registrarService = registrarService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var registrars = await _registrarService.GetAllAsync();

            return Ok(registrars);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var registrar = await _registrarService.GetByIdAsync(id);

            if (registrar is null)
            {
                return NotFound(new
                {
                    message = "Registrar not found"
                });
            }

            return Ok(registrar);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] Registrar registrar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _registrarService.AddAsync(registrar);

            return Ok(new
            {
                message = "Registrar created successfully",
                data = registrar
            });
        }
 
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] Registrar registrar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingRegistrar =
                await _registrarService.GetByIdAsync(id);

            if (existingRegistrar is null)
            {
                return NotFound(new
                {
                    message = "Registrar not found"
                });
            }

            registrar.Id = id;

            await _registrarService.UpdateAsync(registrar);

            return Ok(new
            {
                message = "Registrar updated successfully",
                data = registrar
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingRegistrar =
                await _registrarService.GetByIdAsync(id);

            if (existingRegistrar is null)
            {
                return NotFound(new
                {
                    message = "Registrar not found"
                });
            }

            await _registrarService.DeleteAsync(id);

            return Ok(new
            {
                message = "Registrar deleted successfully"
            });
        }
    }
}
