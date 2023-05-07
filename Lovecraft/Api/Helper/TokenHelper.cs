using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Lovecraft.Datas;
using Lovecraft.Model;
using Microsoft.IdentityModel.Tokens;

namespace Lovecraft.Api.Helper;

public class TokenHelper
{

    public IConfiguration _configuration;
    
    public TokenHelper(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public JwtSecurityToken CreateJWTToken(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new Claim("UserId", user.Id.ToString()),
            new Claim("Email", user.Email)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        JwtSecurityToken token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: DateTime.UtcNow.AddMinutes(10),
            signingCredentials: signIn);
        return token;
    }
}