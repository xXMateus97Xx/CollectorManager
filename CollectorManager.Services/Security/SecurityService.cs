using System.Security.Cryptography;
using System.Text;

namespace CollectorManager.Services.Security;

internal class SecurityService : ISecurityService
{
    private readonly SecuritySettings _settings;

    public SecurityService(SecuritySettings settings)
    {
        _settings = settings;
    }

    public string CreatePasswordSalt()
    {
        using var rng = RandomNumberGenerator.Create();

        var size = _settings.PasswordSaltSize;
        var bytes = size <= 64 ? stackalloc byte[64] : new byte[64];
        bytes = bytes.Slice(0, _settings.PasswordSaltSize);

        rng.GetBytes(bytes);

        return Convert.ToBase64String(bytes);
    }

    public string HashPassword(string password, string salt)
    {
        var size = Encoding.UTF8.GetByteCount(password) + Encoding.UTF8.GetByteCount(salt);
        var bytes = size <= 64 ? stackalloc byte[64] : new byte[64];

        bytes = bytes.Slice(0, size);

        var written = Encoding.UTF8.GetBytes(password, bytes);
        Encoding.UTF8.GetBytes(salt, bytes.Slice(written));

        return HashBytes(bytes);
    }

    public string HashBytes(ReadOnlySpan<byte> bytes)
    {
        using var hash = HashAlgorithm.Create(_settings.HashAlgorithm);
        if (hash == null)
            throw new CryptographicException($"{_settings.HashAlgorithm} is not a valid hash algorithm");

        var hashSize = hash.HashSize / 8;
        var result = hashSize <= 64 ? stackalloc byte[64] : new byte[64];

        result = result.Slice(0, hashSize);

        hash.TryComputeHash(bytes, result, out _);

        return Convert.ToHexString(result);
    }
}
