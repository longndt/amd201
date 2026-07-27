using AuthService.Services;
using Xunit;

namespace AuthService.Tests;

public class PasswordHasherTests
{
    [Fact]
    public void Hash_DoesNotReturnThePlainPassword()
    {
        var hash = PasswordHasher.Hash("secret123");
        Assert.NotEqual("secret123", hash);
        Assert.Contains(".", hash); // format: iterations.salt.hash
    }

    [Fact]
    public void Verify_ReturnsTrue_ForCorrectPassword()
    {
        var hash = PasswordHasher.Hash("secret123");
        Assert.True(PasswordHasher.Verify("secret123", hash));
    }

    [Fact]
    public void Verify_ReturnsFalse_ForWrongPassword()
    {
        var hash = PasswordHasher.Hash("secret123");
        Assert.False(PasswordHasher.Verify("wrong-password", hash));
    }

    [Fact]
    public void Hash_UsesRandomSalt_SoSamePasswordHashesDiffer()
    {
        var a = PasswordHasher.Hash("secret123");
        var b = PasswordHasher.Hash("secret123");
        Assert.NotEqual(a, b);                       // different salts -> different strings
        Assert.True(PasswordHasher.Verify("secret123", a));
        Assert.True(PasswordHasher.Verify("secret123", b)); // …but both still verify
    }

    [Fact]
    public void Verify_ReturnsFalse_ForMalformedHash()
    {
        Assert.False(PasswordHasher.Verify("secret123", "not-a-valid-hash"));
    }
}
