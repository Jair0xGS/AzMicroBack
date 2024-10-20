using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Config;
using Domain.DTOs;
using Domain.Errors;
using Domain.Repository.Interfaces;
using ErrorOr;
using Microsoft.IdentityModel.Tokens;

namespace Domain.UseCases;

public class AuthUc(IUserRepo userRepo, JwtConfig config)
{
    public async Task<ErrorOr<AuthResponse>> Login(LoginCommand dto)
    {
        //flow for basic auth
        var user = await userRepo.GetByEmail(dto.Access);
        if (user.IsError) return user.FirstError;
        if (!user.Value.CheckPassword(dto.Secret)) return GErrors.User.InvalidCredentials;
        return GenerateTokens(user.Value.Id.ToString(), user.Value.Email);
        //TODO implement alternative auth solutions (Google auth,Microsoft auth, facebook auth,apple auth,etc)
    }
    
    public async Task<ErrorOr<string>> Logout(LogoutCommand dto)
    {
        var claims = ValidateToken(dto.Access,config.SecretKey);
        if (claims.IsError) return claims.FirstError;
        return "Session cerrada correctamente!";
    }

    public async Task<ErrorOr<AuthResponse>> RefreshToken(RefreshCommand dto)
    {
        var claims = ValidateToken(dto.Refresh, config.RefreshSecretKey);
        if (claims.IsError) return claims.FirstError;
        var userId = claims.Value.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var email = claims.Value.FindFirst(ClaimTypes.Email)?.Value;
        if (userId is null || email is null) return GErrors.Auth.CouldNotDecodeClaims;
        return GenerateTokens(userId,email);
    }

    private AuthResponse GenerateTokens(string userId, string email)
    {
        var claims = new List<Claim>(){
            new (JwtRegisteredClaimNames.Sub, userId),
            new (JwtRegisteredClaimNames.Email, email)
        };
        var accessToken = GenerateJwtToken(
            config.SecretKey,
            config.AccessTokenExpirationMinutes,
            claims
        );
        if(!config.UseRefresh)return new AuthResponse(
            accessToken,
            UnixTime(DateTime.UtcNow.AddMinutes(config.AccessTokenExpirationMinutes)),
            "",
            0
        );
        var refreshToken = GenerateJwtToken(
            config.RefreshSecretKey,
            config.RefreshTokenExpirationMinutes,
            claims
        );
        return new AuthResponse(
            accessToken,
            UnixTime(DateTime.UtcNow.AddMinutes(config.AccessTokenExpirationMinutes)),
            refreshToken,
            UnixTime(DateTime.UtcNow.AddMinutes(config.RefreshTokenExpirationMinutes))
        );
    }

    private string GenerateJwtToken( string secret,long expiration,List<Claim> sclaims)
    {
        var claims = new List<Claim>()
        {
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) 
        };
        claims.AddRange(sclaims);

        var token = new JwtSecurityToken(
            issuer: config.Issuer,
            audience: config.Audience,
            claims: claims.ToArray(),
            expires: DateTime.UtcNow.AddMinutes(expiration),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secret)),
                SecurityAlgorithms.HmacSha256
            )
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    public ErrorOr<ClaimsPrincipal> ValidateToken(string token,string secret)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(secret);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true, 
                ValidateIssuerSigningKey = true,
                ValidIssuer =config.Issuer,
                ValidAudience =config.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ClockSkew = TimeSpan.Zero 
            };
            var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
            if (validatedToken is not JwtSecurityToken jwtToken) return GErrors.Auth.InvalidToken;
            if (jwtToken.Header.Alg != SecurityAlgorithms.HmacSha256)
            {
                return GErrors.Auth.InvalidToken;
            }
            return principal;
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception.Message);
            return GErrors.Auth.InvalidToken;
        }
    }

    public long UnixTime(DateTime date)
    {
        DateTime epochStart=new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return (date- epochStart).Ticks/10000000;
    }
}