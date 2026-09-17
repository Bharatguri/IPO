using IPO.Application.EmailServises;
using IPO.Domain.Enums;
using IPO.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static System.Net.WebRequestMethods;
using IPO.Domain.Entities.Auth;

namespace IPO.API.Controllers
{


    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _dataContext;

        public AuthController(IConfiguration configuration, IEmailService emailService, ApplicationDbContext dataContext)
        {
            _configuration = configuration;
            _emailService = emailService;
            _dataContext = dataContext;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { Message = "Admin Found Success" });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest input)
        {
            var user = _dataContext.Users
                .FirstOrDefault(x => x.Email == input.Username && x.Password == input.Password);
            if (user != null)
            {
                var token = GenerateToken(input.Username, user.Id, user.Role);
                return Ok(new { Token = token, Message = "Success" });
            }
            return Ok("User Found unsuccess ❌");
        }

        //[HttpPost("login")]
        //public IActionResult Login([FromBody] LoginRequest input)
        //{
        //    if (input == null)
        //    {
        //        return BadRequest("Invalid request");
        //    }


        //    var user = _dataContext.Users
        //        .FirstOrDefault(x => x.Email == input.Username);

        //    if (user == null)
        //    {
        //        return Unauthorized(new { Message = "User not found ❌" });
        //    }


        //    if (user.Password != input.Password)
        //    {
        //        return Unauthorized(new { Message = "Wrong password ❌" });
        //    }


        //    var token = GenerateToken(user.Email, user.Id, user.Role);

        //    return Ok(new
        //    {
        //        Token = token,
        //        Message = "Login successful ✅",
        //        User = new
        //        {
        //            user.Id,
        //            user.Email,
        //            user.FirstName,
        //            user.LastName,
        //            user.Role
        //        }
        //    });
        //}

        [HttpPost("forgot")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var exists = await _dataContext.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (exists == null)
            {
                return BadRequest("User not exists");
            }
            int otpCode = OtpGenrate();
            var otp = new Otp
            {
                UserId = exists.Id,
                Code = otpCode,
                CreatedDate = DateTime.Now,
            };
            await _dataContext.Otps.AddAsync(otp);
            await _dataContext.SaveChangesAsync();

            string subject = "Password Reset";
            string body = $"Your Otp Code is {otpCode}.";
            await _emailService.SendEmail(email, subject, body);
            return Ok("Reset email sent");

        }

        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp(int code)
        {
            var otp = await _dataContext.Otps.FirstOrDefaultAsync(x => x.Code == code);

            if (otp == null)
            {
                return BadRequest("Otp code not found");
            }

            var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == otp.UserId);

            if (user == null)
            {
                return BadRequest("User not found");
            }

            string token = GenerateToken(user.Email, user.Id);

            return Ok(token);
        }

        [Authorize]
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(string oldPassword, string newPassword)
        {

            var email = User.FindFirst("username");
            var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Email == email.Value);
            if (oldPassword != user.Password)
            {
                return BadRequest("wrong Password");
            }
            if (newPassword == oldPassword)
            {
                return BadRequest("old and new password can't be same");
            }

            user.Password = newPassword;

            _dataContext.Users.Update(user);
            await _dataContext.SaveChangesAsync();

            return Ok("Password changed");

        }

        [HttpPost("NewPassword")]
        [Authorize]
        public async Task<IActionResult> ResetPass(string newPassword)
        {
            var email = User.FindFirst("username");

            var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Email == email.Value);

            if (user == null)
            {
                return BadRequest("User not found");
            }

            user.Password = newPassword;

            _dataContext.Users.Update(user);
            await _dataContext.SaveChangesAsync();

            return Ok("Password changed");
        }


        private int OtpGenrate()
        {
            var random = new Random();
            return random.Next(1000, 9999);
        }


        private string GenerateToken(string username, int? userId = null, Role? role = null)
        {
            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTSettings:SecretKey"]));

            var claims = new List<Claim>()
        {
            //new Claim("role", role.ToString()),
            new Claim("username", username)

         };

            if (userId.HasValue)
            {
                //claims.Add(new Claim(ClaimTypes.NameIdentifier, userId.Value.ToString()));
                claims.Add(new Claim("userId", userId.Value.ToString()));
            }

            if (role != null)
            {
                claims.Add(new Claim("role", role.ToString()));
            }

            var token = new JwtSecurityToken(
                issuer: _configuration["JWTSettings:Issuer"],
                audience: _configuration["JWTSettings:Audience"],
                expires: DateTime.Now.AddMonths(3),
                claims: claims,
                signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256)
                );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return jwtToken;
        }


    }
}
