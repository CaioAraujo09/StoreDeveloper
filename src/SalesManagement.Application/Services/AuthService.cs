using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using global::SalesManagement.Domain.Interfaces;
using System.Security.Cryptography;
using SalesManagement.Domain.Entities;

namespace SalesManagement.Application.Services;

public class AuthService
{
    private readonly IAuthRepository _userRepository;
    private readonly IConfiguration _configuration;

    public AuthService(IAuthRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public async Task<string> AuthenticateAsync(string username, string password)
    {
        var user = await _userRepository.GetByUsernameAsync(username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Credenciais inválidas");
        }

        var secretKey = _configuration["JwtSettings:SecretKey"];
        var issuer = _configuration["JwtSettings:Issuer"];
        var audience = _configuration["JwtSettings:Audience"];
        var expiresInHours = int.TryParse(_configuration["JwtSettings:ExpiresInHours"], out var hours) ? hours : 1;

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = new HMACSHA256(Encoding.ASCII.GetBytes(secretKey)).Key;
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(expiresInHours),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public async Task CreateUserAsync(string username, string password)
    {
        var existingUser = await _userRepository.GetByUsernameAsync(username);
        if (existingUser != null)
        {
            throw new InvalidOperationException("O usuário já existe.");
        }

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

        var newUser = new User
        {
            Id = Guid.NewGuid(),
            Username = username,
            PasswordHash = passwordHash
        };

        await _userRepository.AddAsync(newUser);
    }
}