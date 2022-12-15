using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GeoPet.Controllers.TypesReq;
using GeoPet.Data;
using GeoPet.Models;
using Microsoft.IdentityModel.Tokens;

namespace GeoPet.Utils;

public class JwtTokenManager : IJwtTokenManager
{
    private readonly IConfiguration _configuration;
    //private readonly IGeoPetRepository _geoPetRepository;

    public JwtTokenManager(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public string Authenticate(User user)
    {
        var key = _configuration.GetValue<string>("JwtConfig:Key");
        var keyBytes = Encoding.ASCII.GetBytes(key);

        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("Id", user.UserId.ToString())
            }),
            SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}