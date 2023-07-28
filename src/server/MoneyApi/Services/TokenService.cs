namespace MoneyApi.Services;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MoneyApi.Models;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public record TokenClaim(int Id, string Name);
public record TokenRecord(string Token, DateTime ExpiryTime);

public interface ITokenService
{
    TokenRecord GenerateToken(TokenClaim claim, int expiryMinutes = 1140);
    Result<TokenClaim> ReadToken(string token, bool skipExpiryCheck = false);
}

public class TokenService : ITokenService
{
    private readonly JwtSetting jwtOption;

    public TokenService(IOptions<JwtSetting> jwtOption)
    {
        this.jwtOption = jwtOption.Value;
    }

    public TokenRecord GenerateToken(TokenClaim claim, int expiryMinutes = 1140)
    {
        var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, claim.Id.ToString()),
                new Claim(ClaimTypes.Name, claim.Name),
            };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOption.Secret));
        var expTime = DateTime.Now.AddMinutes(expiryMinutes);
        var token = new JwtSecurityToken(
            issuer: jwtOption.Issuer,
            audience: jwtOption.Audience,
            claims: claims,
            notBefore: DateTime.Now,
            expires: expTime,
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

        return new TokenRecord(new JwtSecurityTokenHandler().WriteToken(token), expTime);
    }

    public Result<TokenClaim> ReadToken(string token, bool skipExpiryCheck = false)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOption.Secret));

        try
        {
            var validationResult = new JwtSecurityTokenHandler().ValidateToken(token, new TokenValidationParameters
            {
                ValidIssuer = jwtOption.Issuer,
                ValidAudience = jwtOption.Audience,
                SaveSigninToken = true,
                IssuerSigningKey = key,
                ValidateIssuerSigningKey = true,
                ClockSkew = skipExpiryCheck ? TokenValidationParameters.DefaultClockSkew : TimeSpan.Zero,
            }, out var _);

            // cannot get id, something's wrong
            if (int.TryParse(validationResult.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value, out var id) == false)
            {
                return Result<TokenClaim>.Fail("invalid_token");
            }

            return Result<TokenClaim>.Ok(new TokenClaim(
                id,
                validationResult.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value!));
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return Result<TokenClaim>.Fail("invalid_token");
        }
    }
}

