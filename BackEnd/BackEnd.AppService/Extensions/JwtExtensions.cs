using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BackEnd.AppService.Extensions;

public static class JwtExtensions
{
    public static string ToJwt(Guid id, string username, string role, IConfiguration configuration)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, id.ToString()),
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, role),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private const int WorkFactor = 12; // Ajuste este valor para controlar o custo computacional (quanto maior, mais seguro, mas mais lento)

    public static string HashPassword(string password)
    {
        // Gera o salt automaticamente e realiza o hash com bcrypt
        return BCrypt.Net.BCrypt.HashPassword(password, WorkFactor);
    }

    public static bool VerifyPassword(string password, string hashedPassword)
    {
        // Verifica se a senha fornecida corresponde ao hash armazenado
        try
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
        catch (SaltParseException) //Trata possíveis erros de formatação no hash
        {
            return false;
        }
    }

}
