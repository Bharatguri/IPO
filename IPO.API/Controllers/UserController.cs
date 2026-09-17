using IPO.Application.Interfaces.Services;
using IPO.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IPO.API.Controllers
{
        [ApiController]
        [Route("api/[controller]")]
        public class UserController : ControllerBase
        {
            private readonly IUserService _userService;

            public UserController(IUserService userService)
            {
                _userService = userService;
            }

            [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                var users = await _userService.GetAllAsync();

                return Ok(users);
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> GetById(int id)
            {
                var user = await _userService.GetByIdAsync(id);

                if (user is null)
                    return NotFound(new
                    {
                        message = "User not found"
                    });

                return Ok(user);
            }

            [HttpPost]
            public async Task<IActionResult> Create([FromBody] User user)
            {
                await _userService.AddAsync(user);

                return Ok(new
                {
                    message = "User created successfully",
                    data = user
                });
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> Update(
                int id,
                [FromBody] User user)
            {
                var existingUser = await _userService.GetByIdAsync(id);

                if (existingUser is null)
                    return NotFound(new
                    {
                        message = "User not found"
                    });

                user.Id = id;

                await _userService.UpdateAsync(user);

                return Ok(new
                {
                    message = "User updated successfully",
                    data = user
                });
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                var existingUser = await _userService.GetByIdAsync(id);

                if (existingUser is null)
                    return NotFound(new
                    {
                        message = "User not found"
                    });

                await _userService.DeleteAsync(id);

                return Ok(new
                {
                    message = "User deleted successfully"
                });
            }
        }
}

