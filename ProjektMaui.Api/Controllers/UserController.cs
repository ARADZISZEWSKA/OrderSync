using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjektMaui.Api.Data;
using ProjektMaui.Api.DTOs;
using ProjektMaui.Api.Models;
using System.Security.Cryptography;
using System.Text;

namespace ProjektMaui.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (dto.Password != dto.ConfirmPassword)
                return BadRequest("Hasła się nie zgadzają.");

            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
                return BadRequest("Użytkownik z takim e-mailem już istnieje.");

            var user = new User
            {
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PasswordHash = HashPassword(dto.Password),
                Role = UserRole.User
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("Rejestracja zakończona sukcesem.");
        }

        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null)
                return Unauthorized("Niepoprawny email lub hasło");

            var hashedPassword = HashPassword(dto.Password);
            if (user.PasswordHash != hashedPassword)
                return Unauthorized("Niepoprawny email lub hasło");

            return Ok(new
            {
                Message = "Zalogowano pomyślnie",
                UserId = user.Id,
                Role = user.Role.ToString(),
                Name = user.FirstName
            });
        }

    }
}
