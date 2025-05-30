using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Sigma.Application.Dtos;
using Sigma.Domain.Interfaces;
using Sigma.Domain.Interfaces.Repositories;
using Sigma.Infra.CrossCutting.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Sigma.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUsuarioRepository _usuarioRepository;

        public AuthController(IConfiguration config, IUsuarioRepository usuarioRepository)
        {
            _config = config;
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto login)
        {
            var passwordHash = HashHelper.GerarHashMD5(login.Password);
            var user = await _usuarioRepository.ObterPorLoginAsync(login.Username, passwordHash);

            if (user == null)
                return Unauthorized("Usuário ou senha inválidos.");

            var token = GenerateToken(user.Username, user.Role);
            return Ok(new { token });
        }

        private string GenerateToken(string username, string role)
        {
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
