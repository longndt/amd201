using System.IdentityModel.Tokens.Jwt;
using AuthService.Models;
using AuthService.Services;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace AuthService.Tests;

public class TokenServiceTests
{
    private static TokenService BuildService()
    {
        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["Jwt:Key"] = "unit_test_super_secret_key_at_least_32_characters",
                ["Jwt:Issuer"] = "MicroserviceLab",
                ["Jwt:Audience"] = "MicroserviceLabClients",
            })
            .Build();

        return new TokenService(config);
    }

    [Fact]
    public void CreateToken_ProducesAReadableJwt_WithUserClaims()
    {
        var service = BuildService();
        var user = new AppUser { Id = 42, Username = "student", Email = "student@lab.com" };

        var (token, expiresAt) = service.CreateToken(user);

        Assert.False(string.IsNullOrWhiteSpace(token));
        Assert.True(expiresAt > DateTime.UtcNow);

        var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
        Assert.Equal("MicroserviceLab", jwt.Issuer);
        Assert.Contains(jwt.Claims, c => c.Type == JwtRegisteredClaimNames.UniqueName && c.Value == "student");
        Assert.Contains(jwt.Claims, c => c.Type == JwtRegisteredClaimNames.Sub && c.Value == "42");
    }
}
