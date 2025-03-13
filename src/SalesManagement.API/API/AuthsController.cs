using Microsoft.AspNetCore.Mvc;
using SalesManagement.Application.DTOs.Requests;
using SalesManagement.Application.Services;

namespace SalesManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateReq request)
        {
            if (request == null || string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
                return BadRequest("Usuário e senha são obrigatórios.");

            try
            {
                var token = await _authService.AuthenticateAsync(request.Username, request.Password);
                return Ok(new { Token = token });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Credenciais inválidas.");
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterReq request)
        {
            if (request == null || string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
                return BadRequest("Usuário e senha são obrigatórios.");

            try
            {
                await _authService.CreateUserAsync(request.Username, request.Password);
                return CreatedAtAction(nameof(Authenticate), new { request.Username }, new { Message = "Usuário criado com sucesso." });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }
    }
}
